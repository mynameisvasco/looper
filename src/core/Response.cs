using Newtonsoft.Json.Linq;

namespace Looper.Core
{
    public class Response
    {
        public string FromPath { get; set; }
        public JObject Content { get; set; }

        public override string ToString()
        {
            var json = new JObject();
            json["path"] = FromPath;
            json["content"] = Content;
            return json.ToString();
        }
    }
}