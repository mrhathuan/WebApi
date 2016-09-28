using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace Shop_Nhi.Common
{
    public class StringHelper
    {
        public static string RemoveSpecialChars(string str)
        {
            string[] chars = new string[] { "`", "~", "+", ",", ".", "/", "!", "@", "#", "$", "%", "^", "&", "*", "?", "'", "\"", ";", "_", "(", ")", ":", "|", "[", "]", "<", ">", "=", "'" };
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");            
            for (int i = 0; i < chars.Length; i++)
            {
                if (str.Contains(chars[i]))
                {
                    str = str.Replace(chars[i], "");
                }
            }
            string temp = str.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D').ToLower();
        }

        public static bool IsValiCode(string str)
        {
            //string[] chars = new string[] {"`","~","+", ",", "."," ", "/", "!", "@", "#","?", "$", "%", "^", "&", "'", "\"", ";", "_", ":", "|","đ","Đ" };
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = str.Normalize(NormalizationForm.FormD);
            if (regex.IsMatch(temp) || str.Contains("đ") || str.Contains("Đ"))
            {
                return false;
            }
            //for (int i = 0; i < chars.Length; i++)
            //{
            //    if (str.Contains(chars[i]))
            //    {
            //        return false;
            //    }
            //}
            return true;
        }

        public static string CreateLostPassword(int PasswordLength)
        {
            string _allowedChars = "abcdefghijk0123456789mnopqrstuvwxyz";
            Random randNum = new Random();
            char[] chars = new char[PasswordLength];
            int allowedCharCount = _allowedChars.Length;
            for (int i = 0; i < PasswordLength; i++)
            {
                chars[i] = _allowedChars[(int)((_allowedChars.Length) * randNum.NextDouble())];
            }
            return new string(chars);
        }
    }
}