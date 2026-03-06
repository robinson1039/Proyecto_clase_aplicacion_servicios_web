namespace WebApplicationApi.Core
{
    public class Response<T>
    {
        private string v = string.Empty;

        public Response(string v)
        {
            this.v = v;
        }

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public List<string> Errors { get; set; } = new();
        public T? Result { get; set; }

        public Response() { }

        public static Response<T> Failure(Exception ex, string message = "Ha ocurrido un error al generar la solicitud")
        {
            var errors = new List<string>
    {
        ex.Message
    };

            if (ex.InnerException != null)
            {
                errors.Add("InnerException: " + ex.InnerException.Message);
                errors.Add("InnerStackTrace: " + ex.InnerException.StackTrace);
            }

            errors.Add("StackTrace: " + ex.StackTrace);

            return new Response<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }


        public static Response<T> Failure(string message, List<string> errors = null)
        {
            return new Response<T>
            {
                IsSuccess = false,
                Message = message,
                Errors = errors
            };
        }

        public static Response<T> Success(T result, string message = "Tarea realizada con éxito")
        {
            return new Response<T>
            {
                IsSuccess = true,
                Message = message,
                Result = result,
            };
        }

        public static Response<T> Success(string message = "Tarea realizada con éxito")
        {
            return new Response<T>
            {
                IsSuccess = true,
                Message = message,
            };
        }
    }
}
