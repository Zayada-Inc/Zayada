namespace ZayadaAPI.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultMessageForStatusCode(statusCode);

        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultMessageForStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "Ai facut o greseala fratele meu",
                401 => "N-ai voie, du-te de aici",
                403 => "Ce cauti aici?",
                404 => "N-am gasit fratele meu nimic",
                500 => "Sunt si erori care traiesc cat o capodopera.",
                _ => null
            };
        }
    }
}
