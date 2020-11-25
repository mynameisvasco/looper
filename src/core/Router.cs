using System;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using WebWindows;

namespace Looper.Core
{
    public class Router
    {
        private readonly WebWindow _webview;
        public readonly IDictionary<string, Route> _routes;

        public Router(WebWindow webview)
        {
            _webview = webview;
            _routes = new Dictionary<string, Route>();
        }

        public void AddRoute(Route route)
        {
            _routes.Add(route.Path, route);
        }

        public void AddRoutes(IEnumerable<Route> routes)
        {
            foreach (var route in routes)
            {
                _routes.Add(route.Path, route);
            }
        }

        public async void OnRequest(object sender, string message)
        {
            var request = new Request(JObject.Parse(message));
            var response = new Response();
            var isValidRoute = _routes.ContainsKey(request.GetPath());
            if (isValidRoute)
            {
                try
                {
                    var route = _routes[request.GetPath()];
                    foreach (var behaviour in route.Behaviours)
                    {
                        await behaviour.Handle(request);
                    }
                    response = await route.Handler(request);
                }
                catch (Exception e)
                {
                    int code = 502;
                    if (e.Data.Contains("code"))
                    {
                        code = (int)e.Data["code"];
                    }
                    response = request.RespondWithCode(code, e.Message);
                }
            }
            else
            {
                response = request.RespondWithCode(404, $"Path {request.GetPath()} not found.");
            }
            _webview.Invoke(() =>
            {
                _webview.SendMessage(response.ToString());
            });
        }
    }
}