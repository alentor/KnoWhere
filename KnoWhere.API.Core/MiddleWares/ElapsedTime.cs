using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace KnoWhere.API.Core.MiddleWares
{
    public class ElapsedTime
    {
        private readonly RequestDelegate _Next;

        public ElapsedTime(RequestDelegate next)
        {
            _Next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add("X-ElapsedTime", new[] { stopwatch.ElapsedMilliseconds.ToString() });
                Debug.WriteLine($"<!-- {stopwatch.ElapsedMilliseconds} ms -->");
                return Task.CompletedTask;
            });
            await _Next(context);
        }
    }
}