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
            Console.WriteLine("Starting DASL Import");
            using (var db = new DASLContext())
            {
                Console.WriteLine("Fetching Districts");
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
                        // Add it
                        db.Districts.Add(districtObj);
                    }
                    else
                    {
                        // Keep it up-to-date
                        existingDistrict.LocalId = districtObj.LocalId;
                        existingDistrict.StateProvinceId = districtObj.StateProvinceId;
                        existingDistrict.LeaName = districtObj.LeaName;
                        existingDistrict.LeaUrl = districtObj.LeaUrl;
                        existingDistrict.PhoneNumber = districtObj.PhoneNumber;
                        existingDistrict.Address = districtObj.Address;
                        existingDistrict.City = districtObj.City;
                        existingDistrict.State = districtObj.State;
                        existingDistrict.Country = districtObj.Country;
                        existingDistrict.PostalCode = districtObj.PostalCode;
                    }
                    db.SaveChanges();

                    Console.WriteLine("Fetching Schools For District (" + districtObj.RefId + ")");
                    dynamic schoolsInDistrict = FetchData("SisService/SchoolInfo?leaOrSchoolInfoRefId=" + districtObj.RefId);
                    for (var j = 0; j < schoolsInDistrict.count.Value; j++)
                    {
                        dynamic s = schoolsInDistrict.result[j];
                        School schoolObj = new School
                        {
                            RefId = s.RefId.Value,
                            LocalId = s.LocalId.Value,
                            StateProvinceId = s.StateProvinceId.Value,
                            SchoolName = s.SchoolName.Value,
                            DistrictRefId = s.LeaInfoRefId.Value,
                            SchoolUrl = s.SchoolUrl.Value,
                            PrincipalName = s.PrincipalInfo.ContactName.Value
                        };
                        if (s.PhoneNumberList != null && s.PhoneNumberList.Count > 0)
                        {
                            schoolObj.PhoneNumber = s.PhoneNumberList[0].Number.Value;
                        }
                        if (s.Address.Count > 0)
                        {
                            schoolObj.Address = s.Address[0].Street.Line1.Value;
                            schoolObj.City = s.Address[0].City.Value;
                            schoolObj.State = s.Address[0].StateProvince.Value;
                            schoolObj.Country = s.Address[0].Country.Value;
                            schoolObj.PostalCode = s.Address[0].PostalCode.Value;
                        }
                        School existingSchool = db.Schools.SingleOrDefault(sc => sc.RefId == schoolObj.RefId);
                        if (existingSchool == null)
                        {
                            // Add it
                            db.Schools.Add(schoolObj);
                        }
                        else
                        {
                            // Keep it up-to-date
                            existingSchool.LocalId = schoolObj.LocalId;
                            existingSchool.StateProvinceId = schoolObj.StateProvinceId;
                            existingSchool.SchoolName = schoolObj.SchoolName;
                            existingSchool.DistrictRefId = schoolObj.DistrictRefId;
                            existingSchool.SchoolUrl = schoolObj.SchoolUrl;
                            existingSchool.PhoneNumber = schoolObj.PhoneNumber;
                            existingSchool.Address = schoolObj.Address;
                            existingSchool.City = schoolObj.City;
                            existingSchool.State = schoolObj.State;
                            existingSchool.Country = schoolObj.Country;
                            existingSchool.PostalCode = schoolObj.PostalCode;
                        }
                        db.SaveChanges();

                        Console.WriteLine("Fetching Staff For School (" + schoolObj.RefId + ")");
                        dynamic staffForSchool = FetchData("SisService/Staff?leaOrSchoolInfoRefId=" + schoolObj.RefId);
                        if (staffForSchool == null)
                        {
                            // This school likely no longer exists
                            Console.WriteLine("School no longer exists (" + schoolObj.RefId + ")");
                            continue;
                        }
                        for (var k = 0; k < staffForSchool.count.Value; k++)
                        {
                            dynamic st = staffForSchool.result[k];
                            Staff staffObj = new Staff
                            {
                                RefId = st.RefId.Value,
                                LocalId = st.LocalId.Value,
                                StateProvinceId = st.StateProvinceId.Value,
                                SchoolRefId = schoolObj.RefId,
                                FirstName = st.Name.FirstName.Value,
                                LastName = st.Name.LastName.Value
                            };
                            if (st["EmailList"] != null && st.EmailList.Count > 0)
                            {
                                staffObj.Email = st.EmailList[0].Value;
                            }
                            Staff existingStaff = db.Staffs.SingleOrDefault(sta => sta.RefId == staffObj.RefId);
                            if (existingStaff == null)
                            {
                                // Add it
                                db.Staffs.Add(staffObj);
                            }
                            else
                            {
                                // Keep it up-to-date
                                existingStaff.LocalId = staffObj.LocalId;
                                existingStaff.StateProvinceId = staffObj.StateProvinceId;
                                existingStaff.SchoolRefId = staffObj.SchoolRefId;
                                existingStaff.FirstName = staffObj.FirstName;
                                existingStaff.LastName = staffObj.LastName;
                                existingStaff.Email = staffObj.Email;
                            }
                            db.SaveChanges();
                        }

                        Console.WriteLine("Fetching Students For School (" + schoolObj.RefId + ")");
                        dynamic studentsForSchool = FetchData("SisService/StudentPersonal?leaOrSchoolInfoRefId=" + schoolObj.RefId);
                        if (studentsForSchool == null)
                        {
                            // This school likely no longer exists
                            continue;
                        }
                        for (var k = 0; k < studentsForSchool.count.Value; k++)
                        {
                            dynamic st = studentsForSchool.result[k];
                            Student studentObj = new Student
                            {
                                RefId = st.RefId.Value,
                                LocalId = st.LocalId.Value,
                                StateProvinceId = st.StateProvinceId.Value,
                                SchoolRefId = schoolObj.RefId,
                                FirstName = st.Name.FirstName.Value,
                                MiddleName = st.Name.MiddleName.Value,
                                LastName = st.Name.LastName.Value,
                                HomeroomLocalId = st.MostRecent.HomeroomLocalId.Value,
                                GradeLevel = st.MostRecent.GradeLevel.Code.Value
                            };
                            if (st["PhoneNumberList"] != null && st.PhoneNumberList.Count > 0)
                            {
                                studentObj.PhoneNumber = st.PhoneNumberList[0].Number.Value;
                            }
                            Student existingStudent = db.Students.SingleOrDefault(sta => sta.RefId == studentObj.RefId);
                            if (existingStudent == null)
                            {
                                // Add it
                                db.Students.Add(studentObj);
                            }
                            else
                            {
                                // Keep it up-to-date
                                existingStudent.LocalId = studentObj.LocalId;
                                existingStudent.StateProvinceId = studentObj.StateProvinceId;
                                existingStudent.SchoolRefId = studentObj.SchoolRefId;
                                existingStudent.FirstName = studentObj.FirstName;
                                existingStudent.MiddleName = studentObj.MiddleName;
                                existingStudent.LastName = studentObj.LastName;
                                existingStudent.PhoneNumber = studentObj.PhoneNumber;
                                existingStudent.HomeroomLocalId = studentObj.HomeroomLocalId;
                                existingStudent.GradeLevel = studentObj.GradeLevel;
                            }
                            db.SaveChanges();
                        }
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

                dynamic json = null;
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
