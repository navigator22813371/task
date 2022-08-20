using System.Collections.Generic;

namespace Rick_and_Morty.Application.Responses
{
    public class Response<T>
    {
        public Response()
        {

        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
        }

        public Response(T data, string message = null)
        {
            Data = data;
            Message = message;
            Succeeded = true;
        }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Error { get; set; }
        public T Data { get; set; }
    }
}
