using System;
using Newtonsoft.Json.Linq;

namespace Looper.Core
{
    public class Request
    {
        private readonly JObject _message;

        public Request(JObject message)
        {
            if (!message.ContainsKey("path") || !message.ContainsKey("payload"))
            {
                throw new Exception($@"
                    {message.ToString()} is not a valid request message.
                ");
            }
            _message = message;
        }

        public T GetPayload<T>()
        {
            return _message.GetValue("payload").ToObject<T>();
        }

        public JObject GetRawPayload()
        {
            return _message.GetValue("payload").ToObject<JObject>();
        }

        public string GetPath()
        {
            return _message["path"].ToObject<string>();
        }

        public Response Respond(JObject content)
        {
            var response = new Response();
            response.FromPath = _message["path"].ToObject<string>();
            response.Content = content;
            return response;
        }

        public Response RespondWithCode(int code, string message)
        {
            var response = new Response();
            var withCode = new JObject();
            withCode["message"] = message;
            withCode["code"] = code;
            response.FromPath = _message["path"].ToObject<string>();
            response.Content = withCode;
            return response;
        }
    }
}