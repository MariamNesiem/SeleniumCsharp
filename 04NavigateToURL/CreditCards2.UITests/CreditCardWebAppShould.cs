using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace CreditCards2.UITests
{
    public class CreditCardWebAppShould
    {
        [Fact]
        [Trait("category","smoke")]
        public void LoadApplicationPage()
        {
            using (IWebDriver driver = new ChromeDriver(@"C:\Users\mariam.nesiem\Documents\Studies\SeleniumCsharp\SeleniumCsharp\04NavigateToURL\packages\Selenium.WebDriver.ChromeDriver.86.0.4240.2200\driver\win32"))
            {
                driver.Navigate().GoToUrl("http://localhost:44108/");
            }

        }

    }
}
