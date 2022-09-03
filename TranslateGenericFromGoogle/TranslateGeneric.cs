using System.Net.Http.Headers;
using System.Xml;
using Newtonsoft.Json;
using testingAnyThing;

namespace TranslateGenericFromGoogle
{
    internal class TranslateGeneric
    {
        public string SourceLanguage { get; set; }
        public string DestinationLanguage { get; set; }
        public HttpClient Client { get; set; }

        public TranslateGeneric()
        {
            SourceLanguage = "ar";
            DestinationLanguage = "fr";
            Client = new HttpClient();
            const string url = "https://translate.googleapis.com/translate_a/single";
            Client.BaseAddress = new Uri(url);
            Client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public string? TranslateWord(string? word)
        {
            var valueTranslated = string.Empty;
            var urlParameters = $"?client=gtx&sl={SourceLanguage}&tl={DestinationLanguage}&dt=t&q={word}";
            try
            {
                var response = Client.GetAsync(urlParameters).Result;
                {
                    var dataObjects = response.Content.ReadAsStringAsync().Result;
                    var mainRoot = JsonConvert.DeserializeObject<List<object>>(dataObjects);
                    var arrayList = mainRoot?[0].ToString();
                    if (arrayList != null)
                    {
                        var firstElement = JsonConvert.DeserializeObject<List<object>>(arrayList);
                        var frenchValue = firstElement?[0].ToString();
                        if (frenchValue != null)
                        {
                            valueTranslated = JsonConvert.DeserializeObject<List<object>>(frenchValue)?[0].ToString();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return valueTranslated;
        }
        public async Task TranslateFromDictionary()
        {
            DictionaryLanguage.FillArabicDictionary();
            var french = DictionaryLanguage.Dict;
            var text = string.Empty;
            var counter = 0;
            foreach (var item in french)
            {
                try
                {
                    var valueTranslated = TranslateWord(item.Value);
                    text += $"Dict.Add(\"{item.Key}\", \"{valueTranslated} \");\r\n";
                    counter++;
                    Console.WriteLine("Dictionary counter is : " + counter);

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
            await File.WriteAllTextAsync("D:\\TranslateDictionary.txt", text);
        }
        public async Task TranslateFromXml()
        {
            var result = string.Empty;
            const string filename = "C:\\Users\\mkhhe\\source\\repos\\TranslateGenericFromGoogle\\TranslateGenericFromGoogle\\ResourceXMLWords.xml";
            var xmlDoc = new XmlDocument();
            xmlDoc.Load(filename);
            var locationDetails = xmlDoc.SelectNodes("root");
            var counter = 0;

            foreach (XmlNode locationNode in locationDetails!)
            {
                foreach (XmlNode elementLoc in locationNode.ChildNodes)
                {
                    var key = elementLoc.Attributes?["name"]?.Value;
                    var value = TranslateWord(elementLoc.InnerText);
                    result += $"\t<data name=\"{key}\" xml:space=\"preserve\">\r\n    <value>{value}</value>\r\n  </data>";
                    counter++;
                    Console.WriteLine("XML counter is : " + counter);
                }
            }

            if (!string.IsNullOrWhiteSpace(result))
            {
                await File.WriteAllTextAsync("D:\\Translate-XML.txt", result);
            }
        }
        public async Task TranslateFromJson()
        {
            var result = "{  \"manualScreens\": [";
            const string jsonFilePath = @"C:\Users\mkhhe\source\repos\TranslateGenericFromGoogle\TranslateGenericFromGoogle\JsonKeyValue.json";
            var json = await File.ReadAllTextAsync(jsonFilePath, System.Text.Encoding.UTF8);

            var mainRoot = JsonConvert.DeserializeObject<JsonDataModel>(json);
            var counter = 0;
            foreach (var manualScreen in mainRoot!.ManualScreens)
            {
                result += $"    {{\r\n      \"screenNo\": \"0\",\r\n      \"screenName\": \"{TranslateWord(manualScreen.ScreenName)}\",\r\n      \"description\": \"{manualScreen.Description}\"\r\n    }},";
                counter++;
                Console.WriteLine("json counter is : " + counter);
            }
            result += "], \n{  \"automaticScreens\": [";

            foreach (var automaticScreen in mainRoot.AutomaticScreens)
            {
                result += $"    {{\r\n      \"screenNo\": \"0\",\r\n      \"screenName\": \"{TranslateWord(automaticScreen.ScreenName)}\",\r\n      \"description\": \"{automaticScreen.Description}\"\r\n    }},";
                counter++;
                Console.WriteLine("json counter is : " + counter);
            }
            result += " ], \n {  \"cardErrorCodes\": [";
            foreach (var cardErrorItem in mainRoot.CardErrorCodes.Select(cardError => $"    {{\r\n      \"codeNo\": \"{cardError.CodeNo}\",\r\n      \"codeName\": \"{cardError.CodeName}\",\r\n      \"description\": \"{cardError.Description}\"\r\n    }},"))
            {
                result += cardErrorItem;
                counter++;
                Console.WriteLine("json counter is : " + counter);
            }
            result += "  ],";
            foreach (var cardErrorItem in mainRoot.MeterErrorCodes.Select(meterError => $"    {{\r\n      \"codeNo\": \"{meterError.CodeNo}\",\r\n      \"codeName\": \"{meterError.CodeName}\",\r\n      \"description\": \"{meterError.Description}\"\r\n    }},"))
            {
                result += cardErrorItem;
                counter++;
                Console.WriteLine("json counter is : " + counter);
            }
            result += "  ]   \n }";
            await File.WriteAllTextAsync("D:\\TranslateFromJson.txt", result);
        }

    }
}
