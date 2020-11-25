using System.Threading.Tasks;

namespace Looper.Core
{
    public interface IBehaviour
    {
        Task Handle(Request req);
    }
}