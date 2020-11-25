using System.Threading.Tasks;
using System.Collections.Generic;

namespace Looper.Core
{
    public delegate Task<Response> RouteHandler(Request request);

    public class Route
    {
        public string Path { get; set; }

        public RouteHandler Handler { get; set; }

        public List<IBehaviour> Behaviours { get; } = new List<IBehaviour>();

        public Route()
        {

        }

        public Route(string path, RouteHandler handler)
        {
            Path = path;
            Handler = handler;
        }
    }
}