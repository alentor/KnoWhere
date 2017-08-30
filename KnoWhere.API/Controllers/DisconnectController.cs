using System;
using System.Diagnostics;
using System.Threading.Tasks;
using KnoWhere.API.Data;
using KnoWhere.API.Data.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KnoWhere.API.Controllers
{
    [Route("api/[controller]")]
    public class DisconnectController : Controller
    {
        // Receives the discontinued bucketId.
        [HttpGet("{id}")]
        public async Task<NoContentResult> Get(string id)
        {
            Debug.WriteLine(id);
            // TODO: Remove the discontinued bucketId lofic goes here.
            return NoContent();
        }
    }
}