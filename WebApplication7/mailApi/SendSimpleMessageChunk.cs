using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Web.Script.Serialization;

namespace c_gun
{

    public class LogClass
    {
        public string Id;
        public string Date;
        public string From;
        public string To;
        public string Title;
        public string Body;
        public int Status;
        public string Tags;
    }

    public class MessageResponse
    {
        public string id;
        public string message;
    }

    public class SendSimpleMessageChunk
    {
        private static string ApiPass = System.IO.File.ReadAllText(@"P:\projects\older\_js\amazingPrint\emailApi\v_0.020_final\WebApplication7\apiPass.txt");//put a path to your txt file with api pass
        private static string ApiDomain = System.IO.File.ReadAllText(@"P:\projects\older\_js\amazingPrint\emailApi\v_0.020_final\WebApplication7\apiDomain.txt");//and a domain
        public string from = "<mailgun@api.mailgun.net/v3/" + ApiDomain + ">";
        public string msgTo = "";
        public string body = "The story of a greedy dinosaur";
        public string title = "Happy day, happy message";

        public static string regPath = @"C:\Siri\amazingPrint\emailApi\v_0.004\WebApplication7\bin\logs"; //change this path for your own
        public string currentLogPath = "";
        public string dateDir = @regPath + "\\" + DateTime.Now.ToShortDateString();

        public int status = 4;


        public void Process(string tags)
        {
            var preResponse = SendSimpleMessage(tags).Content.ToString();
            var response = Newtonsoft.Json.JsonConvert.DeserializeObject<MessageResponse>(preResponse);

            if (response.message == "Queued. Thank you.")
            {
                status = 0;
            }
            string idCut = response.id.Substring(1, response.id.Length - 2);

            DateFileCheck(idCut);
            LogsGen(idCut, tags);
        }

        public IRestResponse SendSimpleMessage(string tags= null)
        {
            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator =
                new HttpBasicAuthenticator("api",
                    ApiPass);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", ApiDomain,
                ParameterType.UrlSegment);
            request.Resource = "{domain}/messages";
            request.AddParameter("from",
                from);
            request.AddParameter("to", msgTo);
            request.AddParameter("subject", title);
            if (!String.IsNullOrEmpty(tags))
            {
                request.AddParameter("o:tag", tags);
            }
            
            request.AddParameter("text", body);
            request.Method = Method.POST;
            return client.Execute(request);
        }

        public void LogsGen(string id_, string tags = null)
        {
            //date gen
            string today = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
            var jsonData = File.ReadAllText(currentLogPath);
            var dataList = JsonConvert.DeserializeObject<List<LogClass>>(jsonData)
                               ?? new List<LogClass>();

            var a = EventsDateTimeRecipient().Content;
            var evt = GetEvent(a);

            switch (evt) //event generation.
            {
                case "accepted":
                    status = 3;
                    break;

                case "rejected":
                    status = 4;
                    break;

                case "delivered":
                    status = 2;
                    break;

                case "failed":
                    status = 5;
                    break;

                case "opened":
                    status = 1;
                    break;
            }

            var newLogData = new LogClass //creating logs with class
            {
                Id = id_,
                Date = today,
                From = from,
                To = msgTo,
                Title = title,
                Body = body,
                Status = status,
                Tags = tags
            };

            dataList.Add(newLogData);

            jsonData = JsonConvert.SerializeObject(dataList);
            File.WriteAllText(currentLogPath, jsonData);
        }

        private  string GetEvent(string a)//some magic to get event data from a request
        {
            var eValue = "";

            JavaScriptSerializer conv = new JavaScriptSerializer();
            var aa = conv.Deserialize<Dictionary<string, Object>>(a);
            var items = aa["items"] as ArrayList;
            if (items != null && items.Count > 0)
            {
                var fItems = items[0] as Dictionary<string, Object>;
                if (fItems != null)
                {
                     eValue = fItems["event"] as string;
                }
            }

            return eValue;
        }



             /// <summary>
             /// checks if there is a path for logs. If there is no such path, function creates it.
             /// </summary>
             /// <param name="id">message id</param>
       public void DateFileCheck(string id) 
        {
            string datePath = regPath + "\\" + DateTime.Now.ToShortDateString() + "\\" + id + ".json";

            if (Directory.Exists(dateDir))
            {
                if (File.Exists(datePath))
                {
                    currentLogPath = datePath;
                }
                else
                {
                    var datePathTouch = File.Create(datePath);
                    datePathTouch.Close();
                    currentLogPath = datePath;
                }
            }
            else
            {
                Directory.CreateDirectory(dateDir);
                var datePathTouch = File.Create(datePath);
                datePathTouch.Close();
                currentLogPath = datePath;
            }
        }


        /// <summary>
        /// functionality of "Index" search
        /// </summary>
        public IRestResponse EventsDateTimeRecipient(string ascending = "yes", string recipient = null, int limit = 1, string start = "Fri, 3 May 2013 09:00:00 -0000", string size = null, string _event = null, string attachment = null, string _from = null, string message_id = null, string subject = null, string tags = null, string end = null)
        {

            RestClient client = new RestClient();
            client.BaseUrl = new Uri("https://api.mailgun.net/v3");
            client.Authenticator = new HttpBasicAuthenticator("api", ApiPass);
            RestRequest request = new RestRequest();
            request.AddParameter("domain", ApiDomain, ParameterType.UrlSegment);
            request.Resource = "{domain}/events";
            request.AddParameter("pretty", "yes");
            request.AddParameter("ascending", ascending);

            request.AddParameter("limit", limit);

            if (!String.IsNullOrEmpty(recipient))
            {
                request.AddParameter("recipient", recipient);
            }

            if (!String.IsNullOrEmpty(size))
            {
                request.AddParameter("size", size);
            }

            if (!String.IsNullOrEmpty(_event))
            {
                request.AddParameter("event", _event);
            }

            if (!String.IsNullOrEmpty(attachment))
            {
                request.AddParameter("attachment", attachment);
            }

            if (!String.IsNullOrEmpty(_from))
            {
                request.AddParameter("from", _from);
            }

            if (!String.IsNullOrEmpty(message_id))
            {
                request.AddParameter("message-id", message_id);
            }

            if (!String.IsNullOrEmpty(subject))
            {
                request.AddParameter("subject", subject);
            }

            if (!String.IsNullOrEmpty(tags))
            {
                request.AddParameter("tags", tags);
            }


            request.AddParameter("begin", start);
            if (!String.IsNullOrEmpty(tags))
            {
                request.AddParameter("end", end);
            }

            return client.Execute(request);
        }

        public string HttpPost(string url)
        {
            string response = "";
            HttpWebResponse webResponse = null;
            try
            {
                // Create the XML request object
                HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(url);
                webRequest.KeepAlive = false;
                webRequest.Timeout = 30 * 500;
                webRequest.ReadWriteTimeout = 120 * 500;
                NetworkCredential credential = new NetworkCredential("api", ApiPass);
                webRequest.Credentials = credential;
                webRequest.Method = "GET";
                webResponse = (HttpWebResponse)webRequest.GetResponse();
                Stream stream = webResponse.GetResponseStream();
                if (stream != null)
                {
                    StreamReader streamResponse = new StreamReader(stream);
                    response = streamResponse.ReadToEnd();
                    stream.Dispose();
                    streamResponse.Close();
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (webResponse != null)
                    webResponse.Close();
            }

            return (response);
        }
    }
}
