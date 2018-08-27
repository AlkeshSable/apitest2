using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using RestSharp.Authenticators;

namespace apitest2
{
    public class Prime
    {
        public RestClient restClient;
        public RestRequest restRequest;
        public IRestResponse restResponse;
        public string Get (string url, string uri)
        {
            restClient = new RestClient(url);
            restRequest = new RestRequest(uri, Method.GET);
            restResponse = restClient.Execute(restRequest);
            return restResponse.Content;
            throw new NotImplementedException(restResponse.StatusCode.ToString());
        }
        public string Post(string url, string uri,string Name, string Job)
        {
            restClient = new RestClient(url);
            restRequest = new RestRequest(uri, Method.POST);
            restRequest.AddParameter("Name", Name);
            restRequest.AddParameter("Job", Job);
            restResponse = restClient.Execute(restRequest);
            Console.WriteLine(restResponse.StatusCode);
            return restResponse.Content;
            throw new NotImplementedException(restResponse.StatusCode.ToString());
        }
        public string Put(string url, string uri, string Name, string Job)
        {
            restClient = new RestClient(url);
            restRequest = new RestRequest(uri, Method.PUT);
            restRequest.AddParameter("Name", Name);
            restRequest.AddParameter("Job", Job);
            restResponse = restClient.Execute(restRequest);
            Console.WriteLine(restResponse.StatusCode);
            return restResponse.Content;
            throw new NotImplementedException(restResponse.StatusCode.ToString());
        }
    }
    [TestFixture]
    public class Class1
    {
        public Prime test1;
        public Class1()
        {
            test1 = new Prime();
        }
        [Test]
        public void RunTest1()
        {
            //Console.WriteLine("input first name");
            //string fn = Console.ReadLine();
            //Console.WriteLine("input last name");
            //string ln = Console.ReadLine();
            
          
            string response = test1.Get("https://reqres.in/", "api/users?page=2");         
            

                     
            dynamic jresp = JsonConvert.DeserializeObject<dynamic>(response);

            //converts the response to json (string to objects in json format)

            JArray aray = jresp.data;

            //lists the json objects to array

            JToken tkn = aray;

            //creates tokens to array

            foreach (JObject itemf in tkn)
            {
                string trf = itemf.GetValue("first_name").ToString();
                if ("Eve" == trf)
                {
                    string trl = itemf.GetValue("last_name").ToString();
                    if ("Holt" == trl)
                    {
                        Console.WriteLine(itemf.GetValue("last_name"));
                        string tra = itemf.GetValue("avatar").ToString();
                        Console.WriteLine(tra);
                        Assert.AreNotEqual(tra, "");
                        break;
                    }
                }
            }            
        }
        [Test]
        public void RunTest2()
        {
            string name = "Prakhar";
            string job = "QA associate";
            
            string response = test1.Put("https://reqres.in/", "api/users/2",name, job);
            Console.WriteLine(response);
        }

        [TestCase("Prakhar","QA associate")]
        [TestCase("sanjeev","QA dev") ,Ignore("sanjeev")]
        public void RunTest3(string name, string job)
        {
            //string name = ;
            //string job = ;
            string response = test1.Post("https://reqres.in/", "/api/users", name, job);
            Console.WriteLine(response);
        }
    }
}
