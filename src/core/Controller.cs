namespace Looper.Core
{
    public abstract class Controller : IController
    {
        protected readonly Router _router;

        protected Controller(Router route)
        {
            _router = route;
        }

        public abstract void RegisterRoutes();
    }
}