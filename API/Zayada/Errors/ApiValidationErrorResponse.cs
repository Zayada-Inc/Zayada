namespace ZayadaAPI.Errors
{
    public class ApiValidationErrorResponse: ApiResponse
    {
        public ApiValidationErrorResponse() : base(400)
        {
        }

        public ApiValidationErrorResponse(string message) : base(400)
        {
            Errors = new[] { message };
        }

        public IEnumerable<string> Errors { get; set; }
    }
}
