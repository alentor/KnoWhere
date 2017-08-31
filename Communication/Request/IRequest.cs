using System.Threading.Tasks;

namespace Communication
{
    public interface IRequest
    {
        Task<object> SendAsync(string HttpAddress);

        object Send(string HttpAddress);
    }
}