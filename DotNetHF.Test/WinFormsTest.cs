using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DotNetHF.WinForms;

namespace DotNetHF.Test
{
    [TestClass]
    public class WinFormsTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            string responseFromServer = "<WrongXml>valami</WrongXml>";
            MemoryStream s = new MemoryStream();
            StreamWriter writer = new StreamWriter(s);
            writer.Write(responseFromServer);
            writer.Flush();
            s.Position = 0;
            //ellenőrzés
            ValidateXml(s);
            Assert.AreEqual(expected, actual, 0.001, "Account not debited correctly");
        }
    }
}
