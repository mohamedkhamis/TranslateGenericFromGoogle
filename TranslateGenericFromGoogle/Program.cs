namespace TranslateGenericFromGoogle
{
    internal class Program
    {

        private static async Task Main()
        {
            var translate = new TranslateGeneric();

            Console.WriteLine("start Translate From XML");
            await translate.TranslateFromXml()!;

            Console.WriteLine("start Translate From Json Form Angular Json Key Value");
            await translate.TranslateFromJson(true);

            Console.WriteLine("start Translate From Json Form Custom Json Key Value");
            await translate.TranslateFromJson();

            Console.WriteLine("start Translate From Dictionary");
            await translate.TranslateFromDictionary();
        }
    }
}