// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Linq;

namespace LinqExample;

public class Program
{
    class DummyApi
    {
        public async void fetchData(string baseurl)
        {
            Console.WriteLine("fetching data");
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (var res = client.GetAsync(baseurl).Result)
                    {
                      if(res != null) {
                            string data = await res.Content.ReadAsStringAsync();
                            JObject dataObj = JObject.Parse(data);
                            //Console.WriteLine("data ------------ {0}", dataObj["carts"]);
                            var output = from d in dataObj["carts"]
                              where d["total"]!.Value<Int32>() > 4000
                              select d;
                            foreach(var i in output) {
                              Console.WriteLine(i);
                            }

                      }
                      else {
                        Console.WriteLine("no data");
                      }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("error {0}", e);
            }
        }
    }
    public static void Main()
    {
        Console.WriteLine("hello");
        string baseurl = "https://dummyjson.com/carts";
        //string baseurl = String.Format("http://pokeapi.co/api/v2/pokemon/");
        DummyApi api = new DummyApi();
        api.fetchData(baseurl);
    }
}
