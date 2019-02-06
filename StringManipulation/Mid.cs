namespace StringManipulation
{
    public class Mid
    {
        public string GetMidString(string inputString, int numberOfCharactersToExtract, int startingIndex)
        {
            return inputString.Substring(startingIndex, numberOfCharactersToExtract);
        }
    }
}