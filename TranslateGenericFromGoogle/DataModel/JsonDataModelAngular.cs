namespace TranslateGenericFromGoogle.DataModel
{
    public class JsonDataModelAngular
    {
        public List<KeyValueClass>? Login { get; set; } = new();
        public List<KeyValueClass>? Customers { get; set; } = new();
        public List<KeyValueClass>? NewContract { get; set; } = new();
        public List<KeyValueClass>? Receipt { get; set; } = new();
        public List<KeyValueClass>? Menu { get; set; } = new();
        public List<KeyValueClass>? Report { get; set; } = new();
        public List<KeyValueClass>? BasicDefinitions { get; set; } = new();
        public List<KeyValueClass>? Settings { get; set; } = new();
        public List<KeyValueClass>? Configuration { get; set; } = new();
        public List<KeyValueClass>? Operations { get; set; } = new();
        public List<KeyValueClass>? UserManagement { get; set; } = new();
        public List<KeyValueClass>? Readings { get; set; } = new();
        public List<KeyValueClass>? Share { get; set; } = new();
        public List<KeyValueClass>? FirmwareUpgrade { get; set; } = new();
        public List<KeyValueClass>? Dashbard { get; set; } = new();
        public List<KeyValueClass>? Task { get; set; } = new();
        public List<KeyValueClass>? Schedule { get; set; } = new();
        public List<KeyValueClass>? Applet { get; set; } = new();
        public List<KeyValueClass>? CardServiceError { get; set; } = new();
    }


    public class KeyValueClass
    {
        public string? Key { get; set; }
        public string? Value { get; set; }

    }
}
