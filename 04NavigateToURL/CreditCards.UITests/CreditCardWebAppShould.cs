using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
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
            Thread.Sleep(3000);
        }

        ~CreditCardWebAppShould()
        {

           driver.Dispose();
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void LoadApplicationPage()
        {
            driver.Navigate().Refresh();
            Assert.Equal("Home Page - Credit Cards",driver.Title);
            Assert.Equal("http://localhost:44108/", driver.Url);
        }

        [Fact]
        [Trait("Category", "Smoke")]
        public void ReloadHomePageOnBack()
        {
            driver.Navigate().GoToUrl("http://localhost:44108/Home/About");
            driver.Navigate().Back();
            Assert.Equal("Home Page - Credit Cards", driver.Title);
            Assert.Equal("http://localhost:44108/", driver.Url);
        }

    }
}
