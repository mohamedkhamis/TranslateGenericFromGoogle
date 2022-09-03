namespace TranslateGenericFromGoogle
{
    internal class Program
    {
        private static async Task Main()
        {
            var translateClass = new TranslateGeneric();
            Console.WriteLine("start TranslateFromJson");
            await translateClass.TranslateFromJson();
            Console.WriteLine("start TranslateFromDictionary");
            await translateClass.TranslateFromDictionary();
            Console.WriteLine("start TranslateFromXML");
            await translateClass.TranslateFromXml();
        }
    }
}