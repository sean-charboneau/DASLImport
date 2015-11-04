using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using System.Security.Cryptography;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using DASL_Import_App.Models;
using DASL_Import_App.DAL;

namespace DASL_Import
{
    // To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
    class DASLImport
    {
        // Please set the following connection strings in app.config for this WebJob to run:
        // AzureWebJobsDashboard and AzureWebJobsStorage
        static void Main(string[] args)
        {
            Console.WriteLine("Starting DASL Import...");
            Console.WriteLine(ConfigurationManager.ConnectionStrings["dasl_db"].ConnectionString);
            using (var db = new DASLContext())
            {
                dynamic districts = FetchData("SisService/District");
                for (var i = 0; i < districts.count.Value; i++)
                {
                    dynamic d = districts.result[i];
                    District districtObj = new District
                    {
                        RefId = d.RefId.Value,
                        LocalId = d.LocalId.Value,
                        StateProvinceId = d.StateProvinceId.Value,
                        LeaName = d.LeaName.Value,
                        LeaUrl = d.LeaUrl.Value
                    };
                    if (d.PhoneNumber.Count > 0)
                    {
                        districtObj.PhoneNumber = d.PhoneNumber[0].Number.Value;
                    }
                    if (d.Address.Count > 0)
                    {
                        districtObj.Address = d.Address[0].Street.Line1.Value;
                        districtObj.City = d.Address[0].City.Value;
                        districtObj.State = d.Address[0].StateProvince.Value;
                        districtObj.Country = d.Address[0].Country.Value;
                        districtObj.PostalCode = d.Address[0].PostalCode.Value;
                    }
                    District existingDistrict = db.Districts.SingleOrDefault(di => di.RefId == districtObj.RefId);
                    if(existingDistrict == null)
                    {
                        db.Districts.Add(districtObj);
                        db.SaveChanges();
                    }
                }
            }
        }

        public static dynamic FetchData(string endpoint)
        {
            String vid = "360SafeSolutions";
            String vk = ConfigurationManager.AppSettings["DASLAccessKey"];
            String mydate = DateTime.Now.ToUniversalTime().ToString("R");
            String sid = "";
            String username = string.Format("{0}|{1}|{2}", vid, sid, mydate);
            String t = string.Format("{0}{1}{2}{3}", vid, sid, mydate, vk);
            
            byte[] bytearray = Encoding.ASCII.GetBytes(t);
            byte[] bytearrayhashed = new SHA1CryptoServiceProvider().ComputeHash(bytearray);
            String pw = Convert.ToBase64String(bytearrayhashed);

            String vlauth = vid + "||" + pw;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://vendorlink.omeresa.net/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("VL-Authorization", vlauth);
                client.DefaultRequestHeaders.Add("Date", mydate);

                HttpResponseMessage response = client.GetAsync(endpoint).Result;

                dynamic json = "";
                if(response.IsSuccessStatusCode)
                {
                    var ttt = response.Content.ReadAsStringAsync().Result;
                    json = JsonConvert.DeserializeObject(ttt);
                }

                return json;
            }
        }
    }
}
