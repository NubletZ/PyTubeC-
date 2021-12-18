using Microsoft.VisualStudio.TestTools.UnitTesting;
using TryNoBackground;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TryNoBackground.Tests
{
    [TestClass()]
    public class MainWindowTests
    {
        [TestMethod()]
        public void MainWindowTest()
        {
            MainWindow.startPy();
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);
                MainWindow.pyVersion();

                var resVer = sw.ToString().Trim();
                StringAssert.Contains(resVer, "3.7.3");
            }

            bool resInit = MainWindow.pyInit;
            Assert.IsTrue(resInit);
        }
    }
}