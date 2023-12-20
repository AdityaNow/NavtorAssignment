using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
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

        public void ClickDropdownLink()
        { 
            dropdown_link.Click();
            WaitHelper();
        }

        public void OpenDropdownList()
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(dropdown)));
            select_city.Click();
        }

        public void ClickDropdownLinkWhenNotVisible() //Never used - as never encountered this behaviour
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
            string cityName = "//span[@class='ng-star-inserted' and contains(text(),'"+city+"')]";
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(cityName)));
            driver.FindElement(By.XPath(cityName)).Click();
            WaitHelper();
            Thread.Sleep(100);
        }

        public void VerifySelectedCityInDropdown(string expectedCity)
        {
            Thread.Sleep(50);
            string selectedCity = selected_city_list_value.GetAttribute("textContent");
            ClassicAssert.AreEqual(expectedCity, selectedCity);
        }

        public void WaitHelper() 
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        }

    }
}


