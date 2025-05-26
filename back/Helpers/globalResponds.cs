namespace backapi.Helpers
{
    public class globalResponds
    {

        public string Code { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }

        public globalResponds(string code, string message, object data)
        {
            this.Code = code;
            this.Message = message;
            this.Data = data;
        }

    }
}
