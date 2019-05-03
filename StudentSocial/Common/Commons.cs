using StudentSocial.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace StudentSocial.Common
{
    public class Commons
    {
        public static string token = "";
        public static string semesterNow = "";
        public static List<Lich> lstLichHoc = new List<Lich>();
        public static List<Ky> lstKyHoc = new List<Ky>();
        public static List<Diem> lstDiem = new List<Diem>();
        public static Profile profile = new Profile();
        public static Chung chung = new Chung();
        public static List<Mon> lstMonHoc = new List<Mon>();
        public static List<Mon> lstMonDiem = new List<Mon>();
        public static Dictionary<String, String> dicMonHoc = new Dictionary<string, string>();
        //Đọc dữ liệu
        public static void readDataToFile()
        {
            readtoken();
            readSemester();
            readTimeExam();
            readMark();
        }
        private static void readtoken()
        {
            token = File.ReadAllText(Paths.token);
            var dataProfile = File.ReadAllText(Paths.profile);
            var ojProfile = JObject.Parse(dataProfile);
            profile.MaSinhVien = ojProfile.GetValue("MaSinhVien").ToString();
            profile.Truong = ojProfile.GetValue("Truong").ToString();
            profile.NienKhoa = ojProfile.GetValue("NienKhoa").ToString();
            profile.Nganh = ojProfile.GetValue("Nganh").ToString();
            profile.Lop = ojProfile.GetValue("Lop").ToString();
            profile.HoTen = ojProfile.GetValue("HoTen").ToString();
            profile.HeDaoTao = ojProfile.GetValue("HeDaoTao").ToString();
        }
        public static void readSemester()
        {
            var dataSeme = File.ReadAllText(Paths.semester);
            var jaSeme = JArray.Parse(dataSeme);
            for (int i = 0; i < jaSeme.Count; i++)
            {
                var joSeme = JObject.Parse(jaSeme[i].ToString());
                lstKyHoc.Add(new Ky() {
                    TenKy = joSeme.GetValue("TenKy").ToString(),
                    MaKy = joSeme.GetValue("MaKy").ToString(),
                    KyHienTai = Boolean.Parse(joSeme.GetValue("KyHienTai").ToString())
                });
                if (joSeme.GetValue("KyHienTai").ToString() == "True")
                {
                    File.WriteAllText(Paths.hocky, joSeme.GetValue("MaKy").ToString());
                    semesterNow = joSeme.GetValue("MaKy").ToString();
                }
            }
        }
        private static void readTimeExam()
        {
            var dataTime = JObject.Parse(File.ReadAllText(Paths.lichhoc));
            //Lấy lịch học theo value Entries
            var jaTime = JArray.Parse(dataTime.GetValue("Entries").ToString());
            //Lưu vào list lịch
            for (int i = 0; i < jaTime.Count; i++)
            {
                var joTime = JObject.Parse(jaTime[i].ToString());
                lstLichHoc.Add(new Lich() {
                    MaMon = joTime.GetValue("MaMon").ToString(),
                    DiaDiem = joTime.GetValue("DiaDiem").ToString(),
                    GiaoVien = joTime.GetValue("GiaoVien").ToString(),
                    HinhThuc = joTime.GetValue("HinhThuc").ToString(),
                    LoaiLich = joTime.GetValue("LoaiLich").ToString(),
                    Ngay = joTime.GetValue("Ngay").ToString(),
                    SoBaoDanh = joTime.GetValue("SoBaoDanh").ToString(),
                    ThoiGian = joTime.GetValue("ThoiGian").ToString(),
                });
            }
            //Lấy danh sách môn học thep value Subjects
            var jaSubject = JArray.Parse(dataTime.GetValue("Subjects").ToString());
            for (int i = 0; i < jaSubject.Count; i++)
            {
                var joSubject = JObject.Parse(jaSubject[i].ToString());
                lstMonHoc.Add(new Mon() {
                    HocPhan = joSubject.GetValue("HocPhan").ToString(),
                    MaMon = joSubject.GetValue("MaMon").ToString(),
                    SoTinChi = joSubject.GetValue("SoTinChi").ToString(),
                    TenMon = joSubject.GetValue("TenMon").ToString()
                });
                try
                {
                    dicMonHoc.Add(joSubject.GetValue("MaMon").ToString(), joSubject.GetValue("TenMon").ToString());
                }
                catch (Exception)
                {
                }
            }

            var dataExam = JObject.Parse(File.ReadAllText(Paths.lichthi));
            //Lấy lịch học theo value Entries
            var jaExam = JArray.Parse(dataExam.GetValue("Entries").ToString());
            //Lưu vào list lịch
            for (int i = 0; i < jaExam.Count; i++)
            {
                var joExam = JObject.Parse(jaExam[i].ToString());
                lstLichHoc.Add(new Lich()
                {
                    MaMon = joExam.GetValue("MaMon").ToString(),
                    DiaDiem = joExam.GetValue("DiaDiem").ToString(),
                    GiaoVien = joExam.GetValue("GiaoVien").ToString(),
                    HinhThuc = joExam.GetValue("HinhThuc").ToString(),
                    LoaiLich = joExam.GetValue("LoaiLich").ToString(),
                    Ngay = joExam.GetValue("Ngay").ToString(),
                    SoBaoDanh = joExam.GetValue("SoBaoDanh").ToString(),
                    ThoiGian = joExam.GetValue("ThoiGian").ToString(),
                });
            }
            //Lấy danh sách môn học thep value Subjects
            jaSubject = JArray.Parse(dataExam.GetValue("Subjects").ToString());
            for (int i = 0; i < jaSubject.Count; i++)
            {
                var joSubject = JObject.Parse(jaSubject[i].ToString());
                lstMonHoc.Add(new Mon()
                {
                    HocPhan = joSubject.GetValue("HocPhan").ToString(),
                    MaMon = joSubject.GetValue("MaMon").ToString(),
                    SoTinChi = joSubject.GetValue("SoTinChi").ToString(),
                    TenMon = joSubject.GetValue("TenMon").ToString()
                });
                try
                {
                    dicMonHoc.Add(joSubject.GetValue("MaMon").ToString(), joSubject.GetValue("TenMon").ToString());
                }
                catch (Exception)
                {
                }
            }
        }
        private static void readMark()
        {
            var dataMark = JObject.Parse(File.ReadAllText(Paths.diem));
            chung = new Chung()
            {
                DTBC = dataMark.GetValue("DTBC").ToString(),
                DTBCQD = dataMark.GetValue("DTBCQD").ToString(),
                SoMonKhongDat = dataMark.GetValue("SoMonKhongDat").ToString(),
                SoTCKhongDat = dataMark.GetValue("SoTCKhongDat").ToString(),
                STCTD = dataMark.GetValue("STCTD").ToString(),
                STCTLN = dataMark.GetValue("STCTLN").ToString(), 
                TongTC = dataMark.GetValue("TongTC").ToString()
            };

            var dataDiem = JArray.Parse(dataMark.GetValue("Entries").ToString());
            for (int i = 0; i < dataDiem.Count; i++)
            {
                var joDiem = JObject.Parse(dataDiem[i].ToString());
                lstDiem.Add(new Diem() {
                    CC = joDiem.GetValue("CC").ToString(),
                    DiemChu = joDiem.GetValue("DiemChu").ToString(),
                    KT = joDiem.GetValue("KT").ToString(),
                    MaMon = joDiem.GetValue("MaMon").ToString(),
                    THI = joDiem.GetValue("THI").ToString(),
                    TKHP = joDiem.GetValue("TKHP").ToString(),
                });
            }

            //Lấy danh sách môn học thep value Subjects
            var jaSubject = JArray.Parse(dataMark.GetValue("Subjects").ToString());
            for (int i = 0; i < jaSubject.Count; i++)
            {
                var joSubject = JObject.Parse(jaSubject[i].ToString());
                lstMonDiem.Add(new Mon()
                {
                    HocPhan = joSubject.GetValue("HocPhan").ToString(),
                    MaMon = joSubject.GetValue("MaMon").ToString(),
                    SoTinChi = joSubject.GetValue("SoTinChi").ToString(),
                    TenMon = joSubject.GetValue("TenMon").ToString()
                });
            }
        }
    }
}
