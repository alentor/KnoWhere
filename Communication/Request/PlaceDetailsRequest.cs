using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Communication
{
    public class PlacesDetailsRequest : IRequest
    { 

        public string PlaceId { get; set; } 

        public async Task<object> Send()
        {
            try
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.ToString());
            }
        }
         
    } 
}