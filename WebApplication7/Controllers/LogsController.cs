using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Http;
using c_gun;
using Newtonsoft.Json;

namespace WebApplication7.Controllers
{
    public class Data
    {
        public string Id { get; set; }
    }

    public class LogsController : ApiController
    {
        private readonly List<LogClass> _pages = new List<LogClass>();

        private void InitializeLogs()
        {
            _pages.Clear();

            var app = new SendSimpleMessageChunk();


            if (Directory.Exists(app.dateDir))
                foreach (var file in Directory.GetFiles(app.dateDir))
                {
                    var acc = JsonConvert.DeserializeObject<List<LogClass>>(File.ReadAllText(file));
                    _pages.Add(new LogClass
                    {
                        Body = acc[0].Body,
                        Title = acc[0].Title,
                        Date = acc[0].Date,
                        From = acc[0].From,
                        Id = acc[0].Id,
                        To = acc[0].To,
                        Status = acc[0].Status
                    });
                }
        }

        // GET api/Logs
        public IEnumerable<LogClass> Get()
        {
            InitializeLogs();
            return _pages;
        }

        // GET api/Logs/5
        public LogClass Get(int id)
        {
            InitializeLogs();
            return _pages[id];
        }

        // POST api/Logs
        public void Post([FromBody] string value)
        {
        }

        // PUT api/Logs/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/Logs/5
        public void Delete(int id)
        {
        }

        // POST api/resend
        [Route("api/resend")]
        [HttpPost]
        public void Resend(Data data)
        {
            Console.WriteLine($"boole = {data.Id}");
            var app = new SendSimpleMessageChunk();


            if (Directory.Exists(app.dateDir))
                foreach (var file in Directory.GetFiles(app.dateDir))
                {
                    var path = $"{app.dateDir}" + "\\" + data.Id + ".json";
                    if (file != path) continue;
                    var acc = JsonConvert.DeserializeObject<List<LogClass>>(File.ReadAllText(file));

                    app.from = acc[0].From;
                    app.msgTo = acc[0].To;
                    app.body = acc[0].Body;
                    app.title = acc[0].Title;

                    //send message and create log
                    app.Process("noTagsYet");
                }
        }

        // GET api/resendGet
        [Route("api/resendGet")]
        [HttpGet]
        public string ResendGet()
        {
            return "boole";
        }
    }
}