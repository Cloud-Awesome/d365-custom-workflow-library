using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.Configuration;

using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;

namespace CustomWorkflowLibrary.HelperClasses
{
    public class DynamicsWrapper
    {
        private static IOrganizationService _service;

        /// <summary>
        /// Default constructor to connect to CRM and instantiate org service
        /// </summary>
        public DynamicsWrapper()
        {
            
            var userName = ConfigurationManager.AppSettings["crm.user.name"];
            var password = ConfigurationManager.AppSettings["crm.user.password"];
            var orgServiceUri = ConfigurationManager.AppSettings["crm.orgService.uri"];

            try
            {
                ClientCredentials credentials = new ClientCredentials();
                credentials.UserName.UserName = userName;
                credentials.UserName.Password = password;

                Uri serviceUri = new Uri(orgServiceUri);
                OrganizationServiceProxy proxy = new OrganizationServiceProxy(serviceUri, null, credentials, null);

                proxy.EnableProxyTypes();
                _service = (IOrganizationService)proxy;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while connecting to CRM " + ex.Message);
            }
        }

        public WhoAmIResponse WhoAmI()
        {
            var req = new WhoAmIRequest();
            var response = _service.Execute(req);
            return (WhoAmIResponse) response;
        }

        public OrganizationResponse Execute(OrganizationRequest Request)
        {
            return _service.Execute(Request);
        }

        /// <summary>
        /// Get Entity Collection via QE
        /// </summary>
        /// <param name="entityType">Schema name for entity</param>
        /// <param name="columnSet">Semi-solon delimited list of schema names</param>
        /// <param name="queryString">Semi-solon delimited list of key-value pairs, separated by '='</param>
        /// <returns>String. Either NULL if no results returned or serialised JSON object of results</returns>
        public string GetCrmRecords(string entityType, string columnSet, string queryString)
        {
            var columns = columnSet.Split(';');
            var queryCriteria = queryString.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries)
                        .Select(part => part.Split(':'))
                        .ToDictionary(split => split[0].Trim(), split => split[1].Trim());

            var query = new QueryExpression(entityType)
            {
                ColumnSet = new ColumnSet(columns),
                Criteria = new FilterExpression()
            };

            foreach (var queryCriterion in queryCriteria)
            {
                query.Criteria.AddCondition(queryCriterion.Key, ConditionOperator.Like, "%" + queryCriterion.Value + "%");
            }

            EntityCollection queryResults;
            try
            {
                queryResults = _service.RetrieveMultiple(query);
            }
            catch (Exception exception)
            {
                //_t.TrackEvent("Exception thrown in GetCrmRecords method");
                //_t.TrackException(exception);
                throw;
            }

            if (queryResults.Entities.Count == 0)
            {
                return null;
            }

            var serialisedResults = JsonConvert.SerializeObject(queryResults);
            return serialisedResults;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="recordId"></param>
        /// <param name="attributesList"></param>
        /// <returns></returns>
        public Entity GetCrmRecord(string entityType, Guid recordId, string attributesList)
        {
            Entity entity;
            if (attributesList == null)
            {
                entity = _service.Retrieve(entityType, recordId, new ColumnSet(true));
            }
            else
            {
                entity = _service.Retrieve(entityType, recordId, new ColumnSet(attributesList));
            }
            
            return entity;
        }

        /// <summary>
        /// Creates a new record in CRM. Currently only supports String input values (not dates, lookups or optionsets) except customerid which is hard-coded to Contact (DH)
        /// TODO - Future enhancement to support other record types
        /// </summary>
        /// <param name="entityType"></param>
        /// <param name="attributesDictionary"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Condition.</exception>
        public Guid CreateCrmRecord(string entityType, Dictionary<string, object> attributesDictionary)
        {
            Entity record = new Entity(entityType);
            foreach (var key in attributesDictionary.Keys)
            {
                switch (key)
                {
                    case "customerid":
                        var customerId = Guid.Parse(attributesDictionary[key].ToString());
                        var customEntityReference = new EntityReference("contact", customerId);

                        record["customerid"] = customEntityReference;

                        break;
                    default:
                        if (key.ToLower().Contains("date"))
                        {
                            record.Attributes.Add(key, DateTime.Parse(attributesDictionary[key].ToString()));
                        }
                        else
                        {
                            record.Attributes.Add(key, attributesDictionary[key]);
                        }

                        break;
                }
            }

            Guid recordGuid;
            try
            {
                recordGuid = _service.Create(record);
            }
            catch (Exception exception)
            {
                var telemetryDictionary = new Dictionary<string, string>
                {
                    {"Calling Method", "CrmWrapper.CreateCrmRecord"},
                    {"entityType", entityType},
                    {"Health", "-1"}
                };

                //_t.TrackEvent("CRM record creation failed", telemetryDictionary);
                //_t.TrackException(exception, telemetryDictionary);

                throw;
            }

            return recordGuid;
        }

        public void DeleteCrmRecord(string entityName, Guid recordId)
        {
            try
            {
                _service.Delete(entityName, recordId);
            }
            catch (Exception exception)
            {
                //

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fetchXml"></param>
        /// <returns></returns>
        public List<Dictionary<string, string>> RetrieveMultipleByFetchXml(string fetchXml)
        {
            List<Dictionary<string, string>> returnResults = null;

            var queryResults = _service.RetrieveMultiple(new FetchExpression(fetchXml));
            if (queryResults?.Entities != null && queryResults.Entities.Count > 0)
            {
                returnResults = new List<Dictionary<string, string>>();

                for (int i = 0; i < queryResults.Entities.Count; i++)
                {
                    Dictionary<string, string> listEl = new Dictionary<string, string>();
                    foreach (string key in queryResults.Entities[i].Attributes.Keys)
                    {
                        string val = GetAttributeStringValue(queryResults.Entities[i].Attributes[key]);
                        listEl.Add(key, val);
                    }
                    returnResults.Add(listEl);
                }
            }

            return returnResults;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="crmAttribute"></param>
        /// <returns></returns>
        public static string GetAttributeStringValue(object crmAttribute)
        {
            OptionSetValue osv = crmAttribute as OptionSetValue;
            if (null != osv)
            {
                return (osv.Value.ToString());
            }

            Money mnVal = crmAttribute as Money;
            if (null != mnVal)
            {
                return (mnVal.Value.ToString());
            }

            AliasedValue alVal = crmAttribute as AliasedValue;
            if (null != alVal)
            {
                return (alVal.Value.ToString());
            }

            EntityReference er = crmAttribute as EntityReference;
            if (null != er)
            {
                return (er.Id.ToString());
            }

            return (crmAttribute.ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parentEntityGuid"></param>
        /// <returns></returns>
        public string GetSharePointDocLocAbsoluteUrlFromParentRecord(string parentEntityGuid)
        {
            var sharePointDocLocQuery = new QueryExpression("sharepointdocumentlocation")
            {
                ColumnSet = new ColumnSet("sharepointdocumentlocationid")
            };
            sharePointDocLocQuery.Criteria.AddCondition("regardingobjectid", ConditionOperator.Equal, Guid.Parse(parentEntityGuid));
            sharePointDocLocQuery.Criteria.AddCondition("statecode", ConditionOperator.Equal, 0);

            var docLocs = _service.RetrieveMultiple(sharePointDocLocQuery);
            if (docLocs.Entities.Count == 0) { return null; }

            var docLoc = docLocs[0].ToEntityReference();

            var urlRequest = new RetrieveAbsoluteAndSiteCollectionUrlRequest
            {
                Target = docLoc
            };
            var urlResponse = (RetrieveAbsoluteAndSiteCollectionUrlResponse)_service.Execute(urlRequest);

            return urlResponse.AbsoluteUrl.ToString();
        }
        
        /// <summary>
        /// Wrapper for EntityReference to save on Crm.Sdk references
        /// </summary>
        public class CrmRecordSummary
        {
            /// <summary>
            /// Primary ID of the record
            /// </summary>
            public Guid Id { get; set; }
            /// <summary>
            /// Primary UI Name of the record
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// Often a hidden field on the record (e.g. Primary name without prefix)
            /// </summary>
            public string AlternateName { get; set; }
            /// <summary>
            /// Created On. Can be nullable in case it's not necessary
            /// </summary>
            public DateTime? CreatedOn { get; set; }
            /// <summary>
            /// GUID representing the user or team owning the record
            /// </summary>
            public Guid OwnerId { get; set; }
            /// <summary>
            /// UI Name for the owning user or team
            /// </summary>
            public string OwnerName { get; set; }
        }
    }
}
