using System;
using ModernHttpClient;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;

namespace Communication
{
    public class ImageRequest : RequestBase, IRequest
    {
        public string ImageId { get; set; }
          
         
        // Use only this method as the sync method is not supported
        public async Task<object> SendAsync(string HttpAddress)
        { 
            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler());

                if (!String.IsNullOrEmpty(ImageId))
                    ResponseData = await httpClient.GetStreamAsync(new Uri(HttpAddress + ImageId));
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No image was found. ex: " + ex.ToString());
            }

            return ResponseData;
        }

        public object Send(string HttpAddress)
        { 
            try
            { 
                var request = WebRequest.Create(HttpAddress + ImageId);

                if (!String.IsNullOrEmpty(ImageId))
                using (var response = request.GetResponse())
                {
                    ResponseData = response.GetResponseStream();
                } 
            }
            catch (Exception ex)
            {
                Debug.WriteLine("No image was found. ex: " + ex.ToString());
            }

            return ResponseData;
        }
    }
}