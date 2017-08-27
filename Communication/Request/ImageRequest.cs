using System;
using System.Net;
using ModernHttpClient;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Communication
{
    public class ImageRequest : IRequest
    {
        public string ImageId { get; set; }

        public async Task<object> Send()
        {
            object image = null;

            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler());
                image = await httpClient.GetStreamAsync(new Uri("http://79.176.58.22/api/image/" + ImageId));
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("No photo was found. ex: " + ex.ToString());
            }

            return image;
        }
         
    } 
}