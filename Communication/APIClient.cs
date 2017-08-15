using System.Collections.Generic;

namespace Communication
{
    public class APIClient
    {
        
        public static Response CreateRequest(Request request)
        {
            // Deserialize Json string into KeyValuePairCollection
            Dictionary<string, string> dicResp = new Dictionary<string, string>();

            // Creating the response object
            var response = new Response
            {

            };

            return response;
        }
    }
}
