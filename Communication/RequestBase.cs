namespace Communication
{
    public abstract class RequestBase
    {
        protected internal string ApiUri { get; set; }
        protected internal object ResponseData { get; set; }
        protected internal string ApiKey { get; set; }
    }
}
