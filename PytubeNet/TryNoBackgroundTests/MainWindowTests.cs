using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;
using Python.Included;
using Python.Runtime;

namespace TryNoBackground.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void MainWindowTest()
        {
            MainWindow.startPy();
            bool resInit = MainWindow.pyInit;
            Assert.IsTrue(resInit);
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                MainWindow.pyVersion();

                var resVer = sw.ToString().Trim();
                StringAssert.Contains(resVer, "3.7.3");
            }
        }

        [TestMethod()]
        public void libraryTest()
        {
            MainWindow.startPy();
            Assert.IsNotNull(MainWindow.pytube);
        }

        [TestMethod()]
        public void videoTest()
        {
            MainWindow.startPy();
            string filepath = $"{MainWindow.recordpath}/周杰倫 Jay Chou【稻香 Rice Field】-Official Music Video.mp4";
            Assert.IsTrue(File.Exists(filepath), "The video have not been downloaded yet!");
        }
    }
}