using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KadirApp.Controllers
{
    [Produces("application/json")]
    [Route("api/Food")]
    public class FoodController : Controller
    {
        // GET: api/Food
        [HttpGet]
        public string Get()
        {
            return "[RM2.50] chicken,[RM2.50] pie,[RM2.50] grape,[RM2.50] lemon,[RM2.50] fruit,[RM2.50] salad,[RM2.50] fish bone,[RM2.50] goodie";
        }

        // GET: api/Food/5
        [HttpGet("{command}", Name = "Get")]
        public string Get(string command)
        {
            switch (command)
            {
                case "FoodList":
                    return "chicken,pie,grape,lemon,fruit,salad,fish bone,goodie";
                case "DrinkList":
                    return "teh ais,lmao ais,coffee,ais kosong";
                case "FoodPrice":
                    return "RM1.50,RM2.50,RM3.50,RM4.50,RM5.50,RM6.50,RM7.50,RM8.50";
                case "DrinkPrice":
                    return "RM2.00,RM3.00,RM4.00,RM5.00";
            }
            return "Invalid Link";
        }
        
        // POST: api/Food
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Food/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
