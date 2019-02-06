namespace StringManipulation
{
    public class Trim
    {
        public string GetTrimmedString(string inputString, string stringToTrim)
        {
            var returnValue = !string.IsNullOrEmpty(stringToTrim) ? 
                inputString.Trim(stringToTrim.ToCharArray(0,1)) : 
                inputString.Trim();

            return returnValue;
        }
    }
}