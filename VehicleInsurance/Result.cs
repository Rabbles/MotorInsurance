namespace VehicleInsurance
{
    /// <summary>
    /// TODO
    /// </summary>
    public class Result
    {
        public string Message { get; set; }
        public bool IsSuccessful { get; set; }

        public Result(string message, bool isSuccessful)
        {
            this.Message = message;
            this.IsSuccessful = isSuccessful;
        }
    }
}
