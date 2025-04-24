namespace GBMO.Teach.Core.Configurations.Logging
{
    public class InformationLogFormat : MainLogFormat
    {
        public int ResponseStatusCode { get; set; }
        public string ResponseBody { get; set; }
    }
}
