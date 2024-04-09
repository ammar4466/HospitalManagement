namespace Data
{
    public class DapperMultiResponse<TResult, TResult2>
    {
        public IEnumerable<TResult> Result { get; set; }
        public IEnumerable<TResult2> Result2 { get; set; }
    }
    public class ConnectionStrings
    {
        public string DBCon { get; set; }
        public string CurrentMonth { get; set; }
        public string PreviousMonth { get; set; }
        public string All_Plan_Sync { get; set; }
    }
    public class ConnectionHelper
    {
        public ConnectionStringType Type { get; set; }
        public string MM_YYYY { get; set; }
    }
    public enum ConnectionStringType
    {
        Default = 0,
        CurrentMonth = 1,
        PreviousMonth = 2,
        AllPlanAPI = 3,
    }
}