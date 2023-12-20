using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using SeleniumExtras.WaitHelpers;

namespace NavtorAssignment.PageObjects
{
    class PrimengObjects
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        Boolean res;
        string dropdown = "(//span[@role='combobox' and contains(text(), 'City')])[1]";

        public PrimengObjects(IWebDriver driver)
        {
            this.driver = driver;
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            PageFactory.InitElements(driver, this);

        }


        #region
        /*------------------------------------------------*/
        //Private WebElements-----------------------------//
        /*------------------------------------------------*/
        #endregion
        [FindsBy(How = How.XPath, Using = "//span[contains(text(),'Get Started')]")]
        [CacheLookup]
        private IWebElement get_started_button;

        [FindsBy(How = How.XPath, Using = "//button[@type='button' and span[text()='Components']]")]
        [CacheLookup]
        private IWebElement components_dropdown_button;

        [FindsBy(How = How.XPath, Using = "//a[@href='/dropdown' and contains(text(),'Dropdown')]")]
        [CacheLookup]
        private IWebElement dropdown_link;

        [FindsBy(How = How.XPath, Using = "(//*[@placeholder='Select a City'])[1]")]
        [CacheLookup]
        private IWebElement selected_city_list_value;

        [FindsBy(How = How.XPath, Using = "(//span[@role='combobox' and contains(text(), 'City')])[1]")]
        [CacheLookup]
        private IWebElement select_city;



        #region
        /*------------------------------------------------*/
        //Public Methods - Reusable Actions
        /*------------------------------------------------*/
        #endregion

        public void ClickOnGetStarted()
        {
            get_started_button.Click();
            WaitHelper();
            Console.WriteLine("Clicked on Get Started button..");
        }
        public void ClickComponentsDropdownButton()
        {
            components_dropdown_button.Click();
            WaitHelper();
        }

        public Boolean IsDropdownLinkVisible()
        {
            res = dropdown_link.Displayed;
            return res;
        }

        public void ClickDropdownLink1()
        { 
            dropdown_link.Click();
            WaitHelper();
        }

        public void ClickDropdownLink2()
        {
            
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(dropdown)));
            select_city.Click();

        }


        public void ClickDropdownLink()
        {
            res = IsDropdownLinkVisible();
            if (res == true) 
            {
                dropdown_link.Click();
            }
            else {
                ClickComponentsDropdownButton();
                dropdown_link.Click();
            }
            WaitHelper();
        }

        public void SelectACity(string city)
        {
            //Actions act = new Actions(driver);
            //String[] cityArr = city.Split("");
            //string key = city[0].ToString();
            //act.SendKeys(key);      //Send Keypress with initials R for "Rome"
            //Thread.Sleep(200);
            //Parameterized xpath with city name
            string cityName = "//span[@class='ng-star-inserted' and contains(text(),'"+city+"')]";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(cityName)));
            driver.FindElement(By.XPath(cityName)).Click();
            Thread.Sleep(20000);
            WaitHelper();
        }

        public void VerifySelectedCityInDropdown(string expectedCity)
        {
            Thread.Sleep(500);
            string selectedCity = selected_city_list_value.GetAttribute("textContent");
            Console.Write("Here it is-");
            Console.Write(selectedCity);
            ClassicAssert.AreEqual(expectedCity, selectedCity);
        }

        public void WaitHelper() 
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

    }
}


