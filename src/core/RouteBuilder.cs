using System;
using System.Collections.Generic;

namespace Looper.Core
{
    public class RouteBuilder
    {
        public readonly List<Action<Route>> actions = new List<Action<Route>>();

        public RouteBuilder WithPath(string path)
        {
            actions.Add((r) => r.Path = path);
            return this;
        }

        public RouteBuilder WithBehaviour(IBehaviour behaviour)
        {
            actions.Add((r) => r.Behaviours.Add(behaviour));
            return this;
        }

        public RouteBuilder To(RouteHandler handler)
        {
            actions.Add((r) => r.Handler = handler);
            return this;
        }

        public Route Build()
        {
            var route = new Route();
            actions.ForEach((a) => a(route));
            actions.Clear();
            return route;
        }
    }
}