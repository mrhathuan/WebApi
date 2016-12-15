using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace ClientWeb
{
    public class StringHelper
    {
        private static readonly string[] VietnameseSigns = new string[] { "aAeEoOuUiIdDyY", "áàạảãâấầậẩẫăắằặẳẵ", "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", "éèẹẻẽêếềệểễ", "ÉÈẸẺẼÊẾỀỆỂỄ", "óòọỏõôốồộổỗơớờợởỡ", "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", "úùụủũưứừựửữ", "ÚÙỤỦŨƯỨỪỰỬỮ", "íìịỉĩ", "ÍÌỊỈĨ", "đ", "Đ", "ýỳỵỷỹ", "ÝỲỴỶỸ" };

        public static string RemoveSign4VietnameseString(string str)
        {
            for (int i = 1; i < VietnameseSigns.Length; i++)
                for (int j = 0; j < VietnameseSigns[i].Length; j++)
                    str = str.Replace(VietnameseSigns[i][j], VietnameseSigns[0][i - 1]);
            return str;
        }

        public static string RemoveSpecialCharacters(string input)
        {
            Regex r = new Regex(@"[^a-zA-Z0-9\-\\/(),_\s]+", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant | RegexOptions.Compiled);
            return r.Replace(input, String.Empty);
        }

        public static string RemoveVietSignAndSpecialChar(string input)
        {
            return RemoveSpecialCharacters(RemoveSign4VietnameseString(input));
        }

        public static string ReadVietFromNumber(long value)
        {
            string result = string.Empty;
            Dictionary<string, string> charNormal = new Dictionary<string, string> { { "0", "không" }, { "1", "một" }, { "2", "hai" }, { "3", "ba" }, { "4", "bốn" }, { "5", "năm" }, { "6", "sáu" }, { "7", "bảy" }, { "8", "tám" }, { "9", "chín" } };
            Dictionary<string, string> charLessHundred = new Dictionary<string, string> { { "0", "lẻ" }, { "1", "mốt" }, { "2", "hai" }, { "3", "ba" }, { "4", "bốn" }, { "5", "lăm" }, { "6", "sáu" }, { "7", "bảy" }, { "8", "tám" }, { "9", "chín" } };
            string[] charLevel = new string[] { "mươi", "trăm", "ngàn", "mươi", "trăm", "triệu", "mươi", "trăm", "tỉ" };
            string charLevelTen = "mười";

            if (value == 0)
                result = charNormal["0"];
            else
            {
                List<string> lstStr = new List<string>();
                int i = value.ToString().Length / 9;
                while (i >= 0)
                {
                    int f = i > 0 ? value.ToString().Length - (i * 9) : 0;
                    int t = f == 0 ? value.ToString().Length > 9 ? value.ToString().Length - (i * 9 + 9) : value.ToString().Length : 9;
                    lstStr.Add(value.ToString().Substring(f, t));

                    i--;
                }
                if (lstStr.Count > 0)
                {
                    foreach (var str in lstStr)
                    {
                        string strLevel = string.Empty;
                        for (int j = 0; j < str.Length; j++)
                        {
                            string s = str[str.Length - j - 1].ToString();
                            string n = j + 1 < str.Length ? str[str.Length - j - 2].ToString() : "";
                            switch (s)
                            {
                                case "0":
                                    if (j == 0 || j == 3 || j == 6)
                                        s = "";
                                    else if (j == 1 || j == 4 || j == 7)
                                    {
                                        if (strLevel.Trim() == "")
                                            s = "";
                                        else
                                            s = charLessHundred[s];
                                    }
                                    else
                                        s = charNormal[s];
                                    break;
                                case "1":
                                    if (j == 1 || j == 4 || j == 7)
                                        s = "";
                                    else if ((j == 0 || j == 3 || j == 6) && n.Length > 0 && n == "1")
                                        s = charNormal[s];
                                    else if ((j == 0 || j == 3 || j == 6) && n.Length > 0 && n != "0")
                                        s = charLessHundred[s];
                                    else
                                        s = charNormal[s];
                                    break;
                                default:
                                    if (j == 0 || j == 3 || j == 6)
                                        s = charLessHundred[s];
                                    else
                                        s = charNormal[s];
                                    break;
                            }
                            if (n != "")
                            {
                                if ((j == 0 || j == 3 || j == 6) && n == "0")
                                    strLevel = s != "" ? s + " " + strLevel : strLevel;
                                else
                                {
                                    if ((j == 0 || j == 3 || j == 6) && n.Length > 0 && n == "1")
                                        strLevel = s != "" ? charLevelTen + " " + s + " " + strLevel : charLevelTen + " " + strLevel;
                                    else
                                        strLevel = s != "" ? charLevel[j] + " " + s + " " + strLevel : charLevel[j] + " " + strLevel;
                                }
                            }
                            else
                                strLevel = s != "" ? s + " " + strLevel : strLevel;
                        }
                        result = charLevel[8] + " " + strLevel + result;
                    }
                    if (result != string.Empty)
                        result = result.Substring((charLevel[8] + " ").Length).Trim();
                }
            }
            return result;
        }

        //Create Unique Key
        public static string RNGCharacterMask()
        {
            int maxSize = 8;
            int minSize = 5;
            char[] chars = new char[62];
            string a = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            { result.Append(chars[b % (chars.Length - 1)]); }
            return result.ToString();
        }

        public static bool IsEmail(string email)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(email.Trim(),
                      @"^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                      @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                      System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        public static bool IsValidCode(string str)
        {
            //32: Space 127: Blank
            return str.All(c => c > 32 && c <= 127);
        }
    }
}