using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SampleService.Kestrel.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        /// <summary>
        ///  POST api/values
        /// </summary>
        /// <param name="user"></param>
        [HttpPost]
        public IActionResult Post([FromBody]User user)
        {
            var data = System.Text.Json.JsonSerializer.Serialize(user);
            return Content(data);
        }
    }
    /// <summary>
    /// User
    /// </summary>
    public class User
    {
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Other
        /// </summary>
        public string Age { get; set; }
    }
}