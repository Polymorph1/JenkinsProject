using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace JenkinsProject
{

    //Enum for browserType
    public enum BrowerType
    {
        Chrome,
        Firefox,
        IE
    }

    public class Hooks : Base
    {
        private BrowerType _browserType;

        [SetUp]
        public void InitializeTest()
        {
            //Get the value from NUnit-console --params 
            //e.g. nunit3-console.exe --params:Browser=Firefox \SeleniumNUnitParam.dll
            //If nothing specified, test will run in Chrome browser
            // var browserType = TestContext.Parameters.Get("Browser", "Chrome");
            var browserType = "Firefox"; //Environment.GetEnvironmentVariable("browser");
            //Parse the browser Type, since its Enum
            _browserType = (BrowerType)Enum.Parse(typeof(BrowerType), browserType);
            //Pass it to browser
            ChooseDriverInstance(_browserType);
        }

        private void ChooseDriverInstance(BrowerType browserType)
        {
            if (browserType == BrowerType.Chrome)
            {
                //Driver = new ChromeDriver();
                ChromeOptions cap = new ChromeOptions();
                Driver = new RemoteWebDriver(new Uri("http://localhost:4444//wd/hub"), cap);
            }

            else if (browserType == BrowerType.Firefox)
            {
                //FirefoxDriverService service = FirefoxDriverService.CreateDefaultService(@"C:\Users\840\Desktop\drivers");
                //service.FirefoxBinaryPath = @"C:\Program Files\Mozilla Firefox";
               // Driver = new FirefoxDriver();
                FirefoxOptions cap = new FirefoxOptions();
                Driver = new RemoteWebDriver(new Uri("http://localhost:4444//wd/hub"), cap);

            }
            else if (browserType == BrowerType.IE)
            {
                Driver = new InternetExplorerDriver();
            }
        }

        [TearDown]
        public void CloseBrowser()
        {
            Driver.Quit();
        }
    }
}

