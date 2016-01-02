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
            int day = (int)DateTime.Now.DayOfWeek;
            if (day == 0) // Sunday
            {
                WeekendFetch();
            }
            else if (day < 6) // Weekday
            {
                //WeekdayFetch();
                WeekendFetch();
            }
            else
            {
                //Console.WriteLine("Saturday, no fetch required");
                WeekendFetch();
            }
        }

        public static void WeekdayFetch()
        {
            Console.WriteLine("Weekday");
            using (var db = new DASLContext())
            {
                var endpoint = "GBService/buc/school";
                dynamic schools = FetchData(endpoint);
            }
        }

        public static void WeekendFetch()
        {
            using (var db = new DASLContext())
            {
                Console.WriteLine("Fetching Schools For District (buc)");
                dynamic schoolsInDistrict = FetchData("GBService/buc/school");
                    
                for (var i = 0; i < schoolsInDistrict.count.Value; i++)
                {
                    dynamic s = schoolsInDistrict.result[i];
                    School schoolObj = new School
                    {
                        SchoolId = s.SchoolID.Value.ToString(),
                        DistrictSchoolId = s.DistrictSchoolID.Value,
                        SchoolName = s.SchoolName.Value,
                        PrincipalName = s.PrincipalName.Value,
                        Address = s.Address.Value,
                        City = s.City.Value,
                        State = s.State.Value,
                        ZIP = s.ZIP.Value,
                        PhoneNumber = s.PhoneNumber.Value,
                        ExternalRefId = s.ExternalRefId.Value
                    };
                    School existingSchool = db.Schools.SingleOrDefault(sc => sc.SchoolId == schoolObj.SchoolId);
                    if (existingSchool == null)
                    {
                        // Add it
                        db.Schools.Add(schoolObj);
                    }
                    else
                    {
                        // Keep it up-to-date
                        existingSchool.SchoolId = schoolObj.SchoolId;
                        existingSchool.DistrictSchoolId = schoolObj.DistrictSchoolId;
                        existingSchool.SchoolName = schoolObj.SchoolName;
                        existingSchool.PrincipalName = schoolObj.PrincipalName;
                        existingSchool.PhoneNumber = schoolObj.PhoneNumber;
                        existingSchool.Address = schoolObj.Address;
                        existingSchool.City = schoolObj.City;
                        existingSchool.State = schoolObj.State;
                        existingSchool.ZIP = schoolObj.ZIP;
                        existingSchool.ExternalRefId = schoolObj.ExternalRefId;
                    }
                    db.SaveChanges();

                    Console.WriteLine("Fetching Classes For School (" + schoolObj.SchoolId + ")");
                    dynamic classesForSchool = FetchData("GBService/buc/class?SchoolID=" + schoolObj.SchoolId);
                    if (classesForSchool == null)
                    {
                        // This school likely no longer exists
                        Console.WriteLine("School no longer exists (" + schoolObj.SchoolId + ")");
                        continue;
                    }
                    Console.WriteLine(classesForSchool.count.Value + " Found");
                    for (var j = 0; j < classesForSchool.count.Value; j++)
                    {
                        dynamic c = classesForSchool.result[j];
                        Class classObj = new Class
                        {
                            DistrictCourseId = c.DistrictCourseID.Value,
                            CourseName = c.CourseName.Value,
                            SchoolId = c.SchoolID.Value.ToString(),
                            ClassId = c.ClassID.Value.ToString(),
                            CourseId = c.CourseID.Value.ToString(),
                            ExternalRefId = c.ExternalRefId.Value
                        };
                        Class existingClass = db.Classes.SingleOrDefault(cl => cl.ClassId == classObj.ClassId);
                        if (existingClass == null)
                        {
                            // Add it
                            db.Classes.Add(classObj);
                        }
                        else
                        {
                            // Keep it up-to-date
                            existingClass.DistrictCourseId = classObj.DistrictCourseId;
                            existingClass.CourseName = classObj.CourseName;
                            existingClass.SchoolId = classObj.SchoolId;
                            existingClass.ClassId = classObj.ClassId;
                            existingClass.CourseId = classObj.CourseId;
                            existingClass.ExternalRefId = classObj.ExternalRefId;
                        }
                        db.SaveChanges();

                        Console.WriteLine("Fetching Enrollment for Class (" + existingClass.ClassId + ")");
                        dynamic enrollmentForClass = FetchData("GBService/buc/classenrollment?ClassID=" + existingClass.ClassId);
                        if (enrollmentForClass == null)
                        {
                            // This school likely no longer exists
                            continue;
                        }
                        Console.WriteLine(enrollmentForClass.count.Value + " Found");
                        for (var k = 0; k < enrollmentForClass.count.Value; k++)
                        {
                            dynamic en = enrollmentForClass.result[k];
                            if (en["Students"] == null || en.Students.Count == 0)
                            {
                                Console.WriteLine("No students, skipping...");
                                continue;
                            }
                            for(var l = 0; l < en.Students.Count; l++)
                            {
                                dynamic st = en.Students[l];
                                Student studentObj = new Student
                                {
                                    StudentId = st.StudentID.Value.ToString(),
                                    SchoolId = st.SchoolID.Value.ToString(),
                                    FirstName = st.FirstName.Value,
                                    MiddleName = st.MidName.Value,
                                    LastName = st.LastName.Value,
                                    Gender = st.Gender.Value,
                                    DOB = st.DateOfBirth.Value.ToString(),
                                    GradeLevel = st.GradeLevelID.Value.ToString(),
                                    ExternalRefId = st.ExternalRefId.Value
                                };

                                Student existingStudent = db.Students.SingleOrDefault(sta => sta.StudentId == studentObj.StudentId);
                                if (existingStudent == null)
                                {
                                    // Add it
                                    db.Students.Add(studentObj);
                                }
                                else
                                {
                                    // Keep it up-to-date
                                    existingStudent.SchoolId = studentObj.SchoolId;
                                    existingStudent.FirstName = studentObj.FirstName;
                                    existingStudent.MiddleName = studentObj.MiddleName;
                                    existingStudent.LastName = studentObj.LastName;
                                    existingStudent.GradeLevel = studentObj.GradeLevel;
                                    existingStudent.Gender = studentObj.Gender;
                                    existingStudent.DOB = studentObj.DOB;
                                    existingStudent.ExternalRefId = studentObj.ExternalRefId;
                                }

                                StudentClass studentClassObj = new StudentClass
                                {
                                    StudentId = studentObj.StudentId,
                                    ClassId = classObj.ClassId
                                };

                                StudentClass existingStudentClass = db.StudentClasses.SingleOrDefault(stCl => stCl.StudentId == studentObj.StudentId && stCl.ClassId == classObj.ClassId);
                                if(existingStudentClass == null)
                                {
                                    db.StudentClasses.Add(studentClassObj);
                                }

                                db.SaveChanges();

                                //Console.WriteLine("Fetching Assignments for Student (" + existingStudent.StudentId + ")");
                                dynamic assignmentsForStudent = FetchData("GBService/buc/assignmentscore?StudentID=" + existingStudent.StudentId);
                                Console.WriteLine("GBService/buc/assignmentscore?StudentID=" + existingStudent.StudentId);
                                if (assignmentsForStudent != null)
                                {
                                    Console.WriteLine(assignmentsForStudent.ToString());
                                    return;
                                }
                                else
                                {
                                    return;
                                }
                            }
                        }
                    }

                    /*Console.WriteLine("Fetching Staff For School (" + schoolObj.RefId + ")");
                    dynamic staffForSchool = FetchData("SisService/Staff?leaOrSchoolInfoRefId=" + schoolObj.RefId);
                    if (staffForSchool == null)
                    {
                        // This school likely no longer exists
                        Console.WriteLine("School no longer exists (" + schoolObj.RefId + ")");
                        continue;
                    }
                    Console.WriteLine(staffForSchool.count.Value + " Found");
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
                    dynamic studentsForSchool = FetchData("SisService/StudentSnapshot?schoolInfoRefId=" + schoolObj.RefId);
                    if (studentsForSchool == null)
                    {
                        // This school likely no longer exists
                        continue;
                    }
                    Console.WriteLine(studentsForSchool.count.Value + " Found");
                    for (var k = 0; k < studentsForSchool.count.Value; k++)
                    {
                        dynamic st = studentsForSchool.result[k];
                        Student studentObj = new Student
                        {
                            RefId = st.StudentPersonalRefId.Value,
                            LocalId = st.LocalId.Value,
                            StateProvinceId = st.StateProvinceId.Value,
                            SchoolRefId = schoolObj.RefId,
                            FirstName = st.Name.FirstName.Value,
                            MiddleName = st.Name.MiddleName.Value,
                            LastName = st.Name.LastName.Value,
                            HomeroomNumber = st.HomeEnrollment.HomeroomNumber.Value,
                            GradeLevel = st.HomeEnrollment.GradeLevel.Code.Value
                        };

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
                            existingStudent.HomeroomNumber = studentObj.HomeroomNumber;
                            existingStudent.GradeLevel = studentObj.GradeLevel;
                        }
                        db.SaveChanges();
                    }

                    Console.WriteLine("Fetching Rooms For School (" + schoolObj.RefId + ")");
                    dynamic roomsForSchool = FetchData("SisService/Room?leaOrSchoolInfoRefId=" + schoolObj.RefId);
                    if (roomsForSchool == null)
                    {
                        // This school likely no longer exists
                        continue;
                    }
                    Console.WriteLine(roomsForSchool.count.Value + " Found");
                    for (var k = 0; k < roomsForSchool.count.Value; k++)
                    {
                        dynamic r = roomsForSchool.result[k];

                        Room roomObj = new Room
                        {
                            RefId = r.RefId.Value,
                            SchoolRefId = schoolObj.RefId,
                            RoomNumber = r.RoomNumber.Value,
                            Capacity = r.Capacity.Value.ToString()
                        };
                        if (r["StaffList"] != null && r.StaffList["StaffPersonalRefId"] != null && r.StaffList.StaffPersonalRefId.Count > 0)
                        {
                            roomObj.StaffRefId = r.StaffList.StaffPersonalRefId[0].Value;
                        }
                        Room existingRoom = db.Rooms.SingleOrDefault(ro => ro.RefId == roomObj.RefId);
                        if (existingRoom == null)
                        {
                            // Add it
                            db.Rooms.Add(roomObj);
                        }
                        else
                        {
                            // Keep it up-to-date
                            existingRoom.SchoolRefId = roomObj.SchoolRefId;
                            existingRoom.StaffRefId = roomObj.StaffRefId;
                            existingRoom.RoomNumber = roomObj.RoomNumber;
                            existingRoom.Capacity = roomObj.Capacity;
                        }
                        db.SaveChanges();
                    }*/
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
                Console.WriteLine(response);
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
