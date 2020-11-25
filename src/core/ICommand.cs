using System.Threading.Tasks;

namespace Looper.Core
{
    public interface ICommand
    {
        Task<Response> Handle(Request request);
        void Validate(Request request);
    }
}