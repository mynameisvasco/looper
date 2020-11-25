using System.Threading.Tasks;
using Looper.Core;
using WebWindows;

namespace Looper.Sample
{
    public class LoginCommandPayload
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }

    public class LoginCommand : ICommand
    {
        private readonly WebWindow _window;

        public LoginCommand(WebWindow window)
        {
            _window = window;
        }

        public void Validate(Request request)
        {
            var payload = request.GetPayload<LoginCommandPayload>();
            if (payload.Name == null || payload.Password == null)
            {
                var ex = new System.Exception("Name and password field are mandatory.");
                ex.Data.Add("code", 400);
                throw ex;
            }
        }

        public async Task<Response> Handle(Request request)
        {
            this.Validate(request);
            var payload = request.GetPayload<LoginCommandPayload>();
            if (payload.Name != "Vasco" || payload.Password != "teste")
            {
                var ex = new System.Exception("Incorrect credentials provided");
                ex.Data.Add("code", 400);
                throw ex;
            }
            await Task.Delay(200);
            _window.ShowNotification("You are in!", "Provided credentials are correct!");
            return request.RespondWithCode(200, "You are in");
        }
    }
}