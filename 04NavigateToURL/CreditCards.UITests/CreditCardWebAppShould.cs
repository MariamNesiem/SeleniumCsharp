using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace CreditCards.UITests
{
    public class CreditCardWebAppShould
    {
        public IWebDriver driver;
        public CreditCardWebAppShould()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("http://localhost:44108/");
        }

        ~CreditCardWebAppShould()
        {
           driver.Dispose();
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
                
        }

    }
}
