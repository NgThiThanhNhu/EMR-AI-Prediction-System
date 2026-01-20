namespace EMR_AIPredictionSystem.Model.Response
{
    public class BaseResponse <T>
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public T? data { get; set; }
    }
}
