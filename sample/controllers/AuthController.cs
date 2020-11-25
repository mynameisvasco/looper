using Looper.Core;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Looper.Sample
{
    public class AuthController : Controller
    {
        private readonly ICommand _loginCommand;
        private readonly IBehaviour _exampleBehaviour;

        public AuthController(Router router, ICommand loginCommand, IBehaviour exampleBehaviour) : base(router)
        {
            this._loginCommand = loginCommand;
            this._exampleBehaviour = exampleBehaviour;
        }

        public async Task<Response> Login(Request request)
        {
            return await _loginCommand.Handle(request);
        }

        public override void RegisterRoutes()
        {
            var route = new RouteBuilder();
            var routes = new List<Route>() {
                route.WithPath("/auth/login")
                    .WithBehaviour(_exampleBehaviour)
                    .To(Login)
                    .Build(),
                //...
            };
            _router.AddRoutes(routes);
        }
    }
}