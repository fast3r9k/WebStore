using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.ServiceHosting.Controllers
{
    [Route(WebApi.Values)]
    [ApiController]
    public class ValuesController : ControllerBase
    {

        private static readonly List<string> __Values = Enumerable
           .Range(1, 10)
           .Select(i => $"Value{i:00}")
           .ToList();

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get() => __Values; 

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __Values.Count)
                return NotFound();
            return __Values[id];
        }

        // POST api/<ValuesController>
        [HttpPost]
        [HttpPost("add")]
        public ActionResult Post([FromBody] string value)
        {
            __Values.Add(value);
            return Ok();
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        [HttpPut("edit/{id}")]
        public ActionResult Put(int id, [FromBody] string value)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __Values.Count)
                return NotFound();
            __Values[id] = value;
            return Ok();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            if (id < 0)
                return BadRequest();
            if (id >= __Values.Count)
                return NotFound();
            __Values.RemoveAt(id);
            return Ok();    
        }
    }
}
