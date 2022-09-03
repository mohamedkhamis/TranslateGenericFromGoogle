namespace testingAnyThing
{
    internal class JsonDataModel
    {
        public JsonDataModel(List<Info> info, List<Screen> manualScreens, List<Screen> automaticScreens, List<Error> cardErrorCodes, List<Error> meterErrorCodes, List<ScreenLogos> screenLogos, List<Others> others)
        {
            Info = info;
            ManualScreens = manualScreens;
            this.AutomaticScreens = automaticScreens;
            this.CardErrorCodes = cardErrorCodes;
            this.MeterErrorCodes = meterErrorCodes;
            this.ScreenLogos = screenLogos;
            this.Others = others;
        }

        public List<Info> Info { get; set; }
        public List<Screen> ManualScreens { get; set; }
        public List<Screen> AutomaticScreens { get; set; }
        public List<Error> CardErrorCodes { get; set; }
        public List<Error> MeterErrorCodes { get; set; }
        public List<ScreenLogos> ScreenLogos { get; set; }
        public List<Others> Others { get; set; }

    }
    public class Info
    {
        public string? Info1 { get; set; }
        public string? Photo { get; set; }

    }
    public class Screen
    {
        public string? ScreenNo { get; set; }
        public string? ScreenName { get; set; }
        public string? Description { get; set; }

    }

    public class Error
    {
        public string? CodeNo { get; set; }
        public string? CodeName { get; set; }
        public string? Description { get; set; }

    }
    public class ScreenLogos
    {
        public string? Logo { get; set; }
        public string? Description { get; set; }

    }
    public class Others
    {
        public string? CodeNo { get; set; }
        public string? Data { get; set; }

    }
}
