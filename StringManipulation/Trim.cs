namespace StringManipulation
{
    public class Trim
    {
        //TODO - WorkFlow Activity

        public string GetTrimmedString(string inputString, string stringToTrim)
        {
            var returnValue = !string.IsNullOrEmpty(stringToTrim) ? 
                inputString.Trim(stringToTrim.ToCharArray(0,1)) : 
                inputString.Trim();

            return returnValue;
        }
    }
}