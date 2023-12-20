using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using WebDriverManager.DriverConfigs.Impl;
using OpenQA.Selenium.Interactions;

namespace NavtorAssignment.Utility
{
    public class BrowserSelector
    {
        private WebDriverWait wait;


        public IWebDriver FindDriver(string browser, string url)
        {

            if (browser == "chrome")
            {
                var settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                ChromeOptions options = new ChromeOptions();
                options.AddArgument("pageLoadStrategy=normal");
                //Get Latest Webdriver and match with installed chrome version to avoid version conflict
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                //Inject the Headless option to the chromedriver if required
                IWebDriver driver = new ChromeDriver(options);
                wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                if (url == null || url == "")
                {
                    driver.Url = settings["url"];
                }
                else
                {
                    driver.Url = url;
                }
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
                driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(30);
                Console.WriteLine("Launched" + settings["url"]);
                return driver;
            }
            if (browser == "headless")
            {
                var settings = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
                ChromeOptions options = new ChromeOptions();
                // ChromeDriver is just AWFUL because every version or two it breaks unless you pass cryptic arguments
                //AGRESSIVE: options.setPageLoadStrategy(PageLoadStrategy.NONE); // https://www.skptricks.com/2018/08/timed-out-receiving-message-from-renderer-selenium.html
                options.AddArguments("start-maximized"); // https://stackoverflow.com/a/26283818/1689770
                options.AddArguments("enable-automation"); // https://stackoverflow.com/a/43840128/1689770
                options.AddArguments("--headless"); // only if you are ACTUALLY running headless
                options.AddArguments("--no-sandbox"); //https://stackoverflow.com/a/50725918/1689770
                options.AddArguments("--disable-dev-shm-usage"); //https://stackoverflow.com/a/50725918/1689770
                options.AddArguments("--disable-browser-side-navigation"); //https://stackoverflow.com/a/49123152/1689770
                options.AddArguments("--disable-gpu"); //https://stackoverflow.com/questions/51959986/how-to-solve-selenium-chromedriver-timed-out-receiving-message-from-renderer-exc
                options.AddArguments("--window-size=1920,1080");
                options.AddArguments("--force-device-scale-factor=1");
                options.AddArguments("--disable-features=NetworkService");
                options.AddArguments("--log-level=ALL");
                String userAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/101.0.4951.54 Safari/537.362";
                options.AddArguments(String.Format("user-agent=%s", userAgent));
                //Inject the Headless options to the chromedriver
                IWebDriver driver = new ChromeDriver(options);
                if (url == null || url == "")
                {
                    driver.Url = settings["url"];
                }
                else
                {
                    driver.Url = url;
                }
                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(20);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(20);
                driver.Manage().Timeouts().AsynchronousJavaScript = TimeSpan.FromSeconds(20);
                Console.WriteLine("Launched" + settings["url"]);
                return driver;
                //This option was deprecated, see https://sqa.stackexchange.com/questions/32444/how-to-disable-infobar-from-chrome
                //options.addArguments("--disable-infobars"); //https://stackoverflow.com/a/43840128/1689770
            }
            else
            {
                IWebDriver driver = new ChromeDriver();
                Console.WriteLine("no run");
                return driver;
            }
        }
    }
}
