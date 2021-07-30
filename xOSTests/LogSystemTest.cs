using Microsoft.VisualStudio.TestTools.UnitTesting;
using xOS.CLog;
using System.IO;
using System;
namespace xOSTests
{
    [TestClass]
    public static class LogSystemTest
    {
        [TestMethod]
        public static void LogSytemTest()
        {
            string FileName = "logtest.txt";
            string LogSystemTest = "test";
            string day= DateTime.Now.ToString("d");
            string month= DateTime.Now.ToString("M");
            string year= DateTime.Now.ToString("y");
            string hours = DateTime.Now.ToString("HH");
            string minutes = DateTime.Now.ToString("mm");
            string secomds = DateTime.Now.ToString("ss");
            string expected = $"[{day}-{month}-{year} {hours}:{minutes}] {LogSystemTest} \n";
            LogSystem.SystemLogAudit(FileName,LogSystemTest);
            string FileRead = File.ReadAllText(FileName);
            Assert.AreEqual(expected, FileRead);
        }
    }
}
