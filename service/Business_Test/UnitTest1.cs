using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;
using System.Collections;
using System.Collections.Generic;
using DTO;
namespace Business_Test
{
    [TestClass]
    public class UnitTest1 : BaseTest
    {
        [TestMethod]
        public void Test()
        {
            using (var bl = new BLFinance())
            {
            }
        }
    }
}
