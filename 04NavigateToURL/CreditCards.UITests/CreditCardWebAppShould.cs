using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.ObjectModel;
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

        [Fact]
        [Trait("Category", "Smoke")]
        public void FindTokenElment()
        {
            Assert.Equal("Home Page - Credit Cards", driver.Title);
            string initialToken = driver.FindElement(By.Id("GenerationToken")).Text;
            driver.Navigate().Refresh();
            string reloadedToken = driver.FindElement(By.Id("GenerationToken")).Text;
            Assert.NotEqual(initialToken, reloadedToken);

        }

        [Fact]
        public void BeInitiatedFromHomePage_NewLowRate()
        {
             IWebElement applyLink = driver.FindElement(By.Name("ApplyLowRate"));
            Thread.Sleep(3000);
            applyLink.Click();
            Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal("http://localhost:44108/Apply", driver.Url);
        }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication()
        {
            IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
            carouselNext.Click();
            Thread.Sleep(1000);
            IWebElement applyLink = driver.FindElement(By.LinkText("Easy: Apply Now!"));
            applyLink.Click();
                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal("http://localhost:44108/Apply", driver.Url); 
        }

        [Fact]
        public void BeInitiatedFromHomePage_CustomerService()
        {
                IWebElement carouselNext =
                    driver.FindElement(By.CssSelector("[data-slide='next']"));
                carouselNext.Click();
                Thread.Sleep(1000);
                carouselNext.Click();
                Thread.Sleep(1000);
                IWebElement applyLink =
                    driver.FindElement(By.ClassName("customer-service-apply-now"));
                applyLink.Click();
                Thread.Sleep(1000);
                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal("http://localhost:44108/Apply", driver.Url);
            
        }

        [Fact]
        public void DisplayProductsAndRates()
        {

            ReadOnlyCollection<IWebElement> tableCells =driver.FindElements(By.TagName("td"));

            Assert.Equal("Easy Credit Card", tableCells[0].Text);
            Assert.Equal("20% APR", tableCells[1].Text);

            Assert.Equal("Silver Credit Card", tableCells[2].Text);
            Assert.Equal("18% APR", tableCells[3].Text);

            Assert.Equal("Gold Credit Card", tableCells[4].Text);
            Assert.Equal("17% APR", tableCells[5].Text);
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting()
        {
                IWebElement randomGreetingApplyLink =
                    driver.FindElement(By.PartialLinkText("- Apply Now!"));
                randomGreetingApplyLink.Click();
            Thread.Sleep(1000);
            Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal("http://localhost:44108/Apply", driver.Url);
        }

        [Fact]
        public void BeInitiatedFromHomePage_RandomGreeting_Using_XPATH()
        {
                IWebElement randomGreetingApplyLink =
                    driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]"));
                randomGreetingApplyLink.Click();
            Thread.Sleep(1000);
                Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
                Assert.Equal("http://localhost:44108/Apply", driver.Url);
            }

        [Fact]
        public void BeInitiatedFromHomePage_EasyApplication_ExplicitWait()
        {
            IWebElement carouselNext = driver.FindElement(By.CssSelector("[data-slide='next']"));
            carouselNext.Click();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            //IWebElement applyLink = wait.Until((d) => d.FindElement(By.LinkText("Easy: Apply Now!")));
             IWebElement applyLink = wait.Until(ExpectedConditions.ElementToBeClickable(By.LinkText("Easy: Apply Now!")));
            applyLink.Click();
            Assert.Equal("Credit Card Application - Credit Cards", driver.Title);
            Assert.Equal("http://localhost:44108/Apply", driver.Url);
        }


    }
}
