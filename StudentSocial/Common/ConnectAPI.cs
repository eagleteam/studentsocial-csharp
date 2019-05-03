using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using xNet;

namespace StudentSocial.Common
{
    public class ConnectAPI
    {
        public static string Login(string username, string password)
        {
            String body = "username=" + username + "&password=" + password;
            var request = new HttpRequest();
            try
            {
                var result = request.Post(Api.login, body, "application/x-www-form-urlencoded").ToString();
                result = result.Replace("\"", "");
                if (result != "false")
                {
                    File.WriteAllText(Paths.token, result); //Lưu token
                }
                Commons.token = result;
                return result;
            }
            catch (Exception)
            {
                return "error";
            }
            
        }
        private static HttpRequest creatHttpRequest()
        {
            var request = new HttpRequest();
            request.AddHeader("access-token", Commons.token);
            return request;
        }
        private static string getResponse(HttpRequest request, String api)
        {
            var response = request.Post(api);
            if (response.IsOK)
            {
                return response.ToString();
            }
            return "fasle";
        }
        public static string getToken()
        {
            return File.ReadAllText(Paths.token);
        }
        public static void getSemester()
        {
            var request = creatHttpRequest();
            var result = getResponse(request, Api.semester);
            if (result != "false")
            {
                //Lưu kỳ học
                File.WriteAllText(Paths.semester, result);
            }
        }
        public static void getMark()
        {
            var request = creatHttpRequest();
            var result = getResponse(request, Api.mark);
            if (result != "false")
            {
                //Lưu điểm
                File.WriteAllText(Paths.diem, result);
            }
        }
        public static void getProfile()
        {
            var request = creatHttpRequest();
            var result = getResponse(request, Api.profile);
            if (result != "false")
            {
                //Lưu thông tin
                File.WriteAllText(Paths.profile, result);
            }
        }
        //ADD BODY
        public static void getTime()
        {
            String body = "semester="+ Commons.semesterNow;
            var request = new HttpRequest();
            request.AddHeader("access-token", Commons.token);
            var result = request.Post(Api.time,body, "application/x-www-form-urlencoded").ToString();
            if (result != "false")
            {
                //Lưu lịch học
                File.WriteAllText(Paths.lichhoc, result);
            }
        }
        public static void getExam()
        {
            String body = "semester=" + Commons.semesterNow;
            var request = new HttpRequest();
            request.AddHeader("access-token", Commons.token);
            var result = request.Post(Api.exam, body, "application/x-www-form-urlencoded").ToString();
            if (result != "false")
            {
                //Lưu lịch học
                File.WriteAllText(Paths.lichthi, result);
            }
        }
    }
}
