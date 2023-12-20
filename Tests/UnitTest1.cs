using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using Microsoft.Extensions.Configuration;
using NavtorAssignment.Utility;
using NavtorAssignment.PageObjects;
using NUnit.Framework.Legacy;

namespace NavtorAssignment.Tests
{
    public class Tests
    {
        private WebDriverWait wait;
        private string Url = null;
        private string Browser = null;
        private string WebTitle = string.Empty;
        private string City = null;
        public IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            //Build json object files
            var settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            var testDt = new ConfigurationBuilder().AddJsonFile("TestData\\testData.json").Build();
            
            //Env Setup & Launch Url
            this.Browser = settings["browser"];
            this.Url = settings["url"];
            BrowserSelector br = new BrowserSelector();
            driver = br.FindDriver(Browser, Url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);
            Console.WriteLine("Launched" + Url);

            //------------------------ Test Data Setup ----------------------//
            this.WebTitle = testDt["webTitle"];
            this.City = testDt["city"];
        }

        [Test, Order(1), Category("smoke")]
        public void LandingPageTest()
        {
            PrimengObjects primeObj = new PrimengObjects(driver);
            primeObj.WaitHelper();

            string title = driver.Title; //Fetch Title
            ClassicAssert.AreEqual(title, WebTitle);
            Console.WriteLine("Landing Title Verified");
        }

        [Test, Order(2), Category("smoke")]
        public void SelectTheCity()
        {
            PrimengObjects primeObj = new PrimengObjects(driver);
            primeObj.ClickOnGetStarted();
            primeObj.ClickComponentsDropdownButton();
            primeObj.ClickDropdownLink();
            Thread.Sleep(1000);
            primeObj.OpenDropdownList();
            primeObj.SelectACity(City);
            primeObj.VerifySelectedCityInDropdown(City);
        }


        [TearDown]
        public void Cleanup()
        {
            driver.Close();
            if (driver != null)
            {
                driver.Quit();
            }
        }

    }
}