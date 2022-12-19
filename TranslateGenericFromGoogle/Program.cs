namespace TranslateGenericFromGoogle
{
    internal class Program
    {

        private static async Task Main()
        {
            var translateClass = new TranslateGeneric();

            Console.WriteLine("start Translate From XML");
            await translateClass.TranslateFromXml()!;

            Console.WriteLine("start Translate From Json Form Angular Json Key Value");
            await translateClass.TranslateFromJson(true);

            Console.WriteLine("start Translate From Json Form Custom Json Key Value");
            await translateClass.TranslateFromJson();

            Console.WriteLine("start Translate From Dictionary");
            await translateClass.TranslateFromDictionary();
        }
    }
}