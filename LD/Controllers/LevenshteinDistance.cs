using System;
using LD.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LevenshteinDistanceController : Controller
    {
        [Authorize, HttpGet("{string1}/{string2}")]
        public IActionResult Get(string string1, string string2)
        {
            try
            {
                if (string.IsNullOrEmpty(string1) || string.IsNullOrEmpty(string2))
                    return BadRequest("Please provide both the strings");

                return Ok(JsonConvert.SerializeObject(ResourceExtension.GetLevenshteinDistance(string1.ToLower(), string2.ToLower())));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }

        }
    }
}
