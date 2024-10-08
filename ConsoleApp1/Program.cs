using System;
using System.Text.Json;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics.CodeAnalysis;
using System.Security;
using System.Text;
using System.Runtime.InteropServices.JavaScript;
using System.Transactions;


namespace API
{

    public class RootObject
    {
        public Location location { get; set; }
        public Current current { get; set; }

        public class Current
        {
            public float temp_c { get; set; }
            public float temp_f { get; set; }
            public int is_day { get; set; }
            public float wind_mph { get; set; }
            public float wind_kph { get; set; }
            public int wind_degree { get; set; }
            public string wind_dir { get; set; }
            public float pressure_mb { get; set; }
            public float pressure_in { get; set; }
            public float precip_mm { get; set; }
            public float precip_in { get; set; }
            public int humidity { get; set; }
            public int cloud { get; set; }
            public float feelslike_c { get; set; }
            public float feelslike_f { get; set; }
            public float windchill_c { get; set; }
            public float windchill_f { get; set; }
            public float heatindex_c { get; set; }
            public float heatindex_f { get; set; }
            public float dewpoint_c { get; set; }
            public float dewpoint_f { get; set; }
            public float vis_km { get; set; }
            public float vis_miles { get; set; }
            public float uv { get; set; }
            public float gust_mph { get; set; }
            public float gust_kph { get; set; }
        }

        public class Location
        {
            public string name { get; set; }
            public string region { get; set; }
            public string country { get; set; }
            public float lat { get; set; }
            public float lon { get; set; }
            public string tz_id { get; set; }
            public int localtime_epoch { get; set; }
            public string localtime { get; set; }
        }
    }


    public class Request
    {
        static public HttpClient client = new HttpClient();
        static async Task Main()
        {

            //Проводим проверку для ислючений чтобы не было ошибок и вылетов


            //Это уже работает и выдает json файл но нужно думать с тем что не всегда дает ответ 

            while (true)
            {
                try
                {
                    System.Console.WriteLine("Write a lat, lon/ name city \n Example: 51.5171 -0.1063 \t London/St Petersburg \n\t If you want to close a window write <exit> ");
                    string plus = System.Console.ReadLine();
                    if (plus == "exit"){
                        break;
                    }
                    string Reserch = "http://api.weatherapi.com/v1/current.json?key=1a2abfabfd5445b588a200307242609&q=";
                    using HttpResponseMessage response = await client.GetAsync(Reserch + plus);
                    //using HttpResponseMessage response = await client.GetAsync("http://api.weatherapi.com/v1/current.json?key=1a2abfabfd5445b588a200307242609&q=St Petersburg");
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    //Console.WriteLine(responseBody);

                    /*   string path = @"c:\temp\settings.txt";
                       using (FileStream fs = File.Create(path))
                       {
                           AddText(fs, responseBody);
                       }
                       using FileStream openStream = File.OpenRead(path);*/


                    RootObject? root = JsonSerializer.Deserialize<RootObject>(responseBody);
                    System.Console.WriteLine("output:");


                    //WeatherResponse real_response = JsonConvert.DeserializeObject<WeatherResponse>(responseBody);
                    //System.Console.WriteLine(real_response.temp);
                    //Хз как надо конвертировать json в уже читабельный вид но это задача на затра сейчас уже 11:20PM

                    System.Console.WriteLine($"Name:{root.location.name}");
                    System.Console.WriteLine($"region:{root.location.region}");
                    System.Console.WriteLine($"lat:{root.location.lat}");
                    System.Console.WriteLine($"lon:{root.location.lon}");
                    System.Console.WriteLine($"Time:{root.location.localtime}\n");
                    System.Console.WriteLine($"Temp per C:{root.current.temp_c} ");
                    System.Console.WriteLine($"Day:{root.current.is_day}");
                    System.Console.WriteLine($"Precip mm:{root.current.precip_mm}");
                    System.Console.WriteLine($"Cloud:{root.current.cloud}");
                    System.Console.WriteLine($"Vis km:{root.current.vis_km}");
                    System.Console.WriteLine($"Gust kp/h:{root.current.gust_kph}");

                    System.Console.ReadKey();
                    Console.Clear();

                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine(ex.Message);
                    System.Console.WriteLine("Press ENTER to continue");
                    System.Console.ReadLine();
                    Console.Clear();
                    continue;
                }
            }
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}