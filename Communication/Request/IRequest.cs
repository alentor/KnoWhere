using System.Threading.Tasks;

namespace Communication
{
    public interface IRequest
    {
        Task<object> SendAsync();

        object Send();
    }
}