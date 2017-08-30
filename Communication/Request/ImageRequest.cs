using System;
using ModernHttpClient;
using System.Net.Http;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Net;

namespace Communication
{
    public class ImageRequest : IRequest
    {
        public string ImageId { get; set; }

        // Use only this method as the sync method is not supported
        public async Task<object> SendAsync()
        {
            object image = null;

            try
            {
                var httpClient = new HttpClient(new NativeMessageHandler());

                if (!String.IsNullOrEmpty(ImageId))
                    image = await httpClient.GetStreamAsync(new Uri(AppSettings.Settings["ImageRequestApi"] + ImageId));
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("No photo was found. ex: " + ex.ToString());
            }

            return image;
        }

        public object Send()
        {
            object image = null;

            try
            { 
                var request = WebRequest.Create(AppSettings.Settings["ImageRequestApi"] + ImageId);

                if (!String.IsNullOrEmpty(ImageId))
                using (var response = request.GetResponse())
                {
                    image = response.GetResponseStream();
                } 
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("No photo was found. ex: " + ex.ToString());
            }

            return image;
        }
    }
}