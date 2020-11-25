using System;
using Looper.Core;
using System.Threading.Tasks;

namespace Looper.Sample
{
    public class ExampleBehaviourPayload
    {
        public string Secret { get; set; }
    }

    public class ExampleBehaviour : IBehaviour
    {
        public async Task Handle(Request req)
        {
            await Task.Delay(200);
            var payload = req.GetPayload<ExampleBehaviourPayload>();
            if (payload.Secret != "Aveiro")
            {
                var ex = new Exception("Secret is incorrect");
                ex.Data.Add("code", 403);
                throw ex;
            }
        }
    }
}