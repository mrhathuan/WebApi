using Business;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business_Test
{
    public class BaseTest
    {
        protected const int MaxSec = 3;

        public BaseTest()
        {
            var obj = new AccountItem();
            obj.UserID = 2;
            obj.UserName = "admin";
            obj.ListActionCode = new string[] { };
            obj.SYSCustomerID = 2;
            obj.IsAdmin = false;
            obj.GroupID = 1;
            Thread.SetData(Thread.GetNamedDataSlot("Account"), obj);
        }

        protected void ChangeAccount(AccountItem acc)
        {
            if (acc != null)
            {
                Thread.SetData(Thread.GetNamedDataSlot("Account"), acc);
            }
        }

        protected void LogResult(string fileName, List<string> lstContent)
        {
            string filePath = Directory.GetCurrentDirectory().Replace("bin\\Debug", "") + "TestResult\\" + fileName + ".txt";
            System.IO.File.WriteAllLines(filePath, lstContent);
        }
    }
}
