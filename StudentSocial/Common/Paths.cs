using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentSocial.Common
{
    public class Paths
    {
        public static String audio = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86) + @"\VSond Soft\Student Social WPF\audio");
        public static String baseFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        public static String folder = Path.Combine(baseFolder, "StudentSocialWPF");
        public static String token = Path.Combine(folder, "token.txt");
        public static String semester = Path.Combine(folder, "semester.txt");
        public static String hocky = Path.Combine(folder, "hocky.txt");
        public static String lichthi = Path.Combine(folder, "lichthi.txt");
        public static String lichhoc = Path.Combine(folder, "lichhoc.txt");
        public static String diem = Path.Combine(folder, "diem.txt");
        public static String code = Path.Combine(folder, "code.txt");
        public static String profile = Path.Combine(folder, "profile.txt");
        public static String show = Path.Combine(folder, "show.txt");
        public static String noti = Path.Combine(folder, "noti.txt");
        public static String khoidong = Path.Combine(folder, "khoidong.txt");
        public static String thongbao = Path.Combine(folder, "thongbao.txt");
        public static String amthanh = Path.Combine(folder, "amthanh.txt");

        public static bool checkIsNull()
        {
            if (
                File.ReadAllText(hocky) == "" ||
                File.ReadAllText(diem) == "" ||
                File.ReadAllText(hocky) == "false" ||
                File.ReadAllText(diem) == "false"

                )
            {
                return true;
            }
            return false;
        }
        public static bool checkToken()
        {
            if (!File.Exists(Paths.token))
            {
                return false;
            }
            else
            {
                String token = File.ReadAllText(Paths.token);
                if (token == "")
                {
                    return false;
                }
            }
            return true;
        }
        public static void createdPaths()
        {
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            var tk = File.Create(token);
            var se = File.Create(semester);
            var hk = File.Create(hocky);
            var lt = File.Create(lichthi);
            var lh = File.Create(lichhoc);
            var d = File.Create(diem);
            var c = File.Create(code);
            var p = File.Create(profile);
            var s = File.Create(show);
            var n = File.Create(noti);
            var kd = File.Create(khoidong);
            var tb = File.Create(thongbao);
            var at = File.Create(amthanh);
            n.Close();
            s.Close();
            tk.Close();
            se.Close();
            hk.Close();
            lt.Close();
            lh.Close();
            d.Close();
            c.Close();
            p.Close();
            kd.Close();
            tb.Close();
            at.Close();
            File.WriteAllText(show, "login");
            File.WriteAllText(khoidong, "false");
            File.WriteAllText(thongbao, "true");
            File.WriteAllText(amthanh, "default");
        }
        public static void clearData()
        {
            File.WriteAllText(token, "");
            File.WriteAllText(semester, "");
            File.WriteAllText(hocky, "");
            File.WriteAllText(lichthi, "");
            File.WriteAllText(lichhoc, "");
            File.WriteAllText(diem, "");
            File.WriteAllText(code, "");
            File.WriteAllText(profile, "");
            File.WriteAllText(show, "login");
        }
        public static void clearUpdate()
        {
            File.WriteAllText(lichthi, "");
            File.WriteAllText(lichhoc, "");
        }
    }
}
