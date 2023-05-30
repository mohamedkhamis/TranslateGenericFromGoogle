using System.Net.Http.Headers;
using System.Text;
using System.Xml;
using Newtonsoft.Json;
using TranslateGenericFromGoogle.DataModel;
using TranslateGenericFromGoogle.DataSample;

namespace TranslateGenericFromGoogle;

internal class TranslateGeneric
{
    public TranslateGeneric()
    {
        Result = string.Empty;
        DataObject = new JsonDataModelAngular();
        SourceLanguage = "ar";
        DestinationLanguage = "es";
        Client = new HttpClient();
        const string url = "https://translate.googleapis.com/translate_a/single";
        Client.BaseAddress = new Uri(url);
        Client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));
    }
    public string SourceLanguage { get; set; }
    public string DestinationLanguage { get; set; }
    public HttpClient Client { get; set; }
    public string Result { get; set; }
    public int Counter { get; set; }
    public JsonDataModelAngular DataObject { get; set; }

    public string? TranslateWord(string? word)
    {
        if (string.IsNullOrWhiteSpace(word))
            return word;
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
                    var translatedValue = firstElement?[0].ToString();
                    if (translatedValue != null)
                    {
                        valueTranslated = JsonConvert.DeserializeObject<List<object>>(translatedValue)?[0].ToString();
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
        var spanishDict = DictionaryLanguage.dict;
        var text = string.Empty;
        var counter = 0;
        foreach (var item in spanishDict)
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
        await File.WriteAllTextAsync("D:\\TranslateDictionarySpanishDict.txt", text);
    }
    public async Task TranslateFromXml()
    {
        var result = string.Empty;
        const string filename = "C:\\Users\\mohamed.khamis\\source\\repos\\mohamedkhamis\\TranslateGenericFromGoogle\\TranslateGenericFromGoogle\\DataSample\\ResourceXMLWords.xml";

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
                Console.WriteLine("XML counter is : " + counter + "   Date Time Is  "+ DateTime.Now);
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

        const string jsonFilePath = @"C:\Users\mohamed.khamis\Source\Repos\mohamedkhamis\TranslateGenericFromGoogle\TranslateGenericFromGoogle\DataSample\JsonKeyValue.json";
        var json = await File.ReadAllTextAsync(jsonFilePath, Encoding.UTF8);

        var mainRoot = JsonConvert.DeserializeObject<JsonDataModel>(json);
        var counter = 0;
        foreach (var manualScreen in mainRoot!.ManualScreens)
        {
            result += $"    {{\r\n      \"screenNo\": \"{manualScreen.ScreenNo}\",\r\n      \"screenName\": \"{TranslateWord(manualScreen.ScreenName)}\",\r\n      \"description\": \"{TranslateWord(manualScreen.Description)}\"\r\n    }},";
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
        await File.WriteAllTextAsync("D:\\TranslateFromJsonFR.txt", result);
    }
    public async Task TranslateFromJson(bool isAngularTranslate)
    {
        if (isAngularTranslate)
        {
            const string jsonFilePath = @"C:\Users\mohamed.khamis\Source\Repos\mohamedkhamis\TranslateGenericFromGoogle\TranslateGenericFromGoogle\DataSample\JsonKeyValueAngular.json";
            var json = await File.ReadAllTextAsync(jsonFilePath, Encoding.UTF8);
            var dynamicObject = JsonConvert.DeserializeObject<dynamic>(json)!;
            FillJsonModel(dynamicObject.login, DataObject.Login);
            FillJsonModel(dynamicObject.customers, DataObject.Customers);
            FillJsonModel(dynamicObject.newContract, DataObject.NewContract);
            FillJsonModel(dynamicObject.receipt, DataObject.Receipt);
            FillJsonModel(dynamicObject.menu, DataObject.Menu);
            FillJsonModel(dynamicObject.report, DataObject.Report);
            FillJsonModel(dynamicObject.basicDefinitions, DataObject.BasicDefinitions);
            FillJsonModel(dynamicObject.settings, DataObject.Settings);
            FillJsonModel(dynamicObject.configuration, DataObject.Configuration);
            FillJsonModel(dynamicObject.operations, DataObject.Operations);
            FillJsonModel(dynamicObject.userManagement, DataObject.UserManagement);
            FillJsonModel(dynamicObject.readings, DataObject.Readings);
            FillJsonModel(dynamicObject.share, DataObject.Share);
            FillJsonModel(dynamicObject.firmwareUpgrade, DataObject.FirmwareUpgrade);
            FillJsonModel(dynamicObject.dashbard, DataObject.Dashbard);
            FillJsonModel(dynamicObject.task, DataObject.Task);
            FillJsonModel(dynamicObject.schedule, DataObject.Schedule);
            FillJsonModel(dynamicObject.applet, DataObject.Applet);
            FillJsonModel(dynamicObject.cardServiceError, DataObject.CardServiceError);

            Result = "{\r\n  \"login\": {";
            AddData(DataObject.Login, "customers");
            AddData(DataObject.Customers, "newContract");
            AddData(DataObject.NewContract, "receipt");
            AddData(DataObject.Receipt, "menu");
            AddData(DataObject.Menu, "report");
            AddData(DataObject.Report, "basicDefinitions");
            AddData(DataObject.BasicDefinitions, "settings");
            AddData(DataObject.Settings, "configuration");
            AddData(DataObject.Configuration, "operations");
            AddData(DataObject.Operations, "userManagement");
            AddData(DataObject.UserManagement, "readings");
            AddData(DataObject.Readings, "share");
            AddData(DataObject.Share, "firmware-upgrade");
            AddData(DataObject.FirmwareUpgrade, "dashbard");
            AddData(DataObject.Dashbard, "task");
            AddData(DataObject.Task, "schedule");
            AddData(DataObject.Schedule, "applet");
            AddData(DataObject.Applet, "cardServiceError");
            AddData(DataObject.CardServiceError, "schedule");
            AddData(DataObject.Task, "Final");
            Result += "  }\r\n}";


            await File.WriteAllTextAsync("D:\\TranslateFromJson-ES-Angular.txt", Result);



        }

    }
    public void FillJsonModel(dynamic dynamicObject, List<KeyValueClass> dataEntity)
    {
        foreach (var item in dynamicObject)
        {
            dataEntity?.Add(new KeyValueClass() { Key = item.First.Parent.Name, Value = item.First.Value });
        }
    }
    public void AddData(List<KeyValueClass>? dataList,string nextGroupName)
    {
        var itemIndex = 0;
        if (dataList != null)
            foreach (var item in dataList)
            {
                itemIndex++;
                var lastComma = itemIndex == dataList.Count ?  string.Empty : ",";
                Result += $"  \"{item.Key}\": \"{TranslateWord(item.Value)}\"{lastComma}";
                Counter++;
                Console.WriteLine("json counter is : " + Counter + "   Date Time Is  " + DateTime.Now);

            }
        Result += "\r\n  },\r\n  \"" + $"{nextGroupName}" + "\": {";
    }
    public async Task FixPhoneNumbers()
    {
        StringBuilder result =new StringBuilder();

        const string PhoneNumberFiles = @"C:\Users\mohamed.khamis\Source\Repos\mohamedkhamis\TranslateGenericFromGoogle\TranslateGenericFromGoogle\DataSample\PhoneNumbersData.txt";
        using (var reader = new StreamReader(PhoneNumberFiles))
        {
            const char character = '+';

            while (await reader.ReadLineAsync() is { } line)
            {
                var NewLine = string.Empty;
                var LastChar = ' ';

                foreach (var c in line)
                {
                    if (c == character && !char.IsWhiteSpace(LastChar))
                    {
                        NewLine += Environment.NewLine;
                    }
                    LastChar = c;
                    NewLine += c;
                }

                result.Append(NewLine);
                result.Append(Environment.NewLine);

                //result += NewLine;
                Console.WriteLine(NewLine);
            }
        }
        await File.WriteAllTextAsync("D:\\NewPhoneNumber.txt", result.ToString());
    }

}