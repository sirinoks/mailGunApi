
using System;
using System.Web.Http;
using c_gun;

namespace WebApplication7.Controllers
{
    public class EventController : ApiController
    {
        // GET: api/Event
        public string Get()
        {
            var pr = new SendSimpleMessageChunk();

            var l = pr.EventsDateTimeRecipient("yes", "shinigamoo@gmail.com", 100, "Fri, 3 May 2013 09:00:00 -0000").Content;
            return l;
        }

        // GET: api/Event/5
        public string Get(string link)
        {
            return new SendSimpleMessageChunk().HttpPost(link);
        }


        // POST: api/Event
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Event/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Event/5
        public void Delete(int id)
        {
        }
    }
}
