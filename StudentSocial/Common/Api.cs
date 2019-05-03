using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSocial.Common
{
    public class Api
    {
        public static string api = "https://studentsocial.shipx.vn/api.php?action=";
        //"https://sqt-ictu.herokuapp.com/api.php?action=";
        //"https://trolyao.rao.vn/tnu/api.php?action=";
        public static string login = api+"login";
        public static string semester = api+"semester";
        public static string exam = api+"exam-table";
        public static string mark = api+"mark";
        public static string time = api+"time-table";
        public static string profile = api+"profile";
    }
}
