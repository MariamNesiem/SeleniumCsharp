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
            //driver.Manage().Window.Maximize();
            //Thread.Sleep(1000);
            //driver.Manage().Window.Minimize();
            //Thread.Sleep(1000);
            //driver.Manage().Window.Size = new System.Drawing.Size(300, 400);
            //Thread.Sleep(1000);
            //driver.Manage().Window.Position = new System.Drawing.Point(1, 1);
            //Thread.Sleep(1000);
            //driver.Manage().Window.Position = new System.Drawing.Point(50, 50);
            //Thread.Sleep(1000);
            //driver.Manage().Window.Position = new System.Drawing.Point(100, 100);
            //Thread.Sleep(1000);
            //driver.Manage().Window.FullScreen();
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


        [Fact]
        public void BeSubmittedWhenValid()
        {
                driver.Navigate().GoToUrl("http://localhost:44108/Apply");

                driver.FindElement(By.Id("FirstName")).SendKeys("Sarah");
                Thread.Sleep(1000);
                driver.FindElement(By.Id("LastName")).SendKeys("Smith");
                Thread.Sleep(1000);
                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("123456-A");
                Thread.Sleep(1000);
                driver.FindElement(By.Id("Age")).SendKeys("18");
                Thread.Sleep(1000);
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");
                Thread.Sleep(1000);
                driver.FindElement(By.Id("Single")).Click();
                Thread.Sleep(1000);
                IWebElement businessSourceSelectElement = driver.FindElement(By.Id("BusinessSource"));
                SelectElement businessSource = new SelectElement(businessSourceSelectElement);
                // Check default selected option is correct
                Assert.Equal("I'd Rather Not Say", businessSource.SelectedOption.Text);
                Assert.Equal(5, businessSource.Options.Count);
                // Select an option
                businessSource.SelectByValue("Email");
                Thread.Sleep(1000);
                businessSource.SelectByText("Internet Search");
                Thread.Sleep(1000);
                businessSource.SelectByIndex(4); 

                driver.FindElement(By.Id("TermsAccepted")).Click();

                //driver.FindElement(By.Id("SubmitApplication")).Click();
                driver.FindElement(By.Id("Single")).Submit();

                Assert.StartsWith("Application Complete", driver.Title);
                Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                Assert.Equal("Sarah Smith", driver.FindElement(By.Id("FullName")).Text);
                Assert.Equal("18", driver.FindElement(By.Id("Age")).Text);
                Assert.Equal("50000", driver.FindElement(By.Id("Income")).Text);
                Assert.Equal("Single", driver.FindElement(By.Id("RelationshipStatus")).Text);
                Assert.Equal("TV", driver.FindElement(By.Id("BusinessSource")).Text);
            
        }

        [Fact]
        public void BeSubmittedWhenValidationErrorsCorrected()
        {
            const string firstName = "Sarah";
            const string invalidAge = "17";
            const string validAge = "18";
            driver.Navigate().GoToUrl("http://localhost:44108/Apply");

                driver.FindElement(By.Id("FirstName")).SendKeys(firstName);
                // Don't enter lastname
                driver.FindElement(By.Id("FrequentFlyerNumber")).SendKeys("123456-A");
                driver.FindElement(By.Id("Age")).SendKeys(invalidAge);
                driver.FindElement(By.Id("GrossAnnualIncome")).SendKeys("50000");
                driver.FindElement(By.Id("Single")).Click();
                IWebElement businessSourceSelectElement =
                        driver.FindElement(By.Id("BusinessSource"));
                SelectElement businessSource = new SelectElement(businessSourceSelectElement);
                businessSource.SelectByValue("Email");
                driver.FindElement(By.Id("TermsAccepted")).Click();
                driver.FindElement(By.Id("SubmitApplication")).Click();

                // Assert that validation failed                
                var validationErrors =
                    driver.FindElements(By.CssSelector(".validation-summary-errors > ul > li"));
                Assert.Equal(2, validationErrors.Count);
                Assert.Equal("Please provide a last name", validationErrors[0].Text);
                Assert.Equal("You must be at least 18 years old", validationErrors[1].Text);

            Thread.Sleep(5000);
            // Fix errors
            driver.FindElement(By.Id("LastName")).SendKeys("Smith");
                driver.FindElement(By.Id("Age")).Clear();
                driver.FindElement(By.Id("Age")).SendKeys(validAge);

                // Resubmit form
                driver.FindElement(By.Id("SubmitApplication")).Click();

                // Check form submitted
                Assert.StartsWith("Application Complete", driver.Title);
                Assert.Equal("ReferredToHuman", driver.FindElement(By.Id("Decision")).Text);
                Assert.NotEmpty(driver.FindElement(By.Id("ReferenceNumber")).Text);
                Assert.Equal("Sarah Smith", driver.FindElement(By.Id("FullName")).Text);
                Assert.Equal("18", driver.FindElement(By.Id("Age")).Text);
                Assert.Equal("50000", driver.FindElement(By.Id("Income")).Text);
                Assert.Equal("Single", driver.FindElement(By.Id("RelationshipStatus")).Text);
                Assert.Equal("Email", driver.FindElement(By.Id("BusinessSource")).Text);
        }

        [Fact]
        public void OpenContactFooterLinkInNewTab()
        {
               driver.FindElement(By.Id("ContactFooter")).Click();

            Thread.Sleep(5000);

                ReadOnlyCollection<string> allTabs = driver.WindowHandles;
                string homePageTab = allTabs[0];
                string contactTab = allTabs[1];

                driver.SwitchTo().Window(contactTab);

            Thread.Sleep(5000);

            Assert.EndsWith("/Home/Contact", driver.Url);
          
        }

        [Fact]
        public void AlertIfLiveChatClosed()
        {
             driver.FindElement(By.Id("LiveChat")).Click();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                IAlert alert = wait.Until(ExpectedConditions.AlertIsPresent());

                Assert.Equal("Live chat is currently closed.", alert.Text);

            Thread.Sleep(5000);

            alert.Accept();

            Thread.Sleep(5000);
        }

        [Fact]
        public void RenderAboutPage()
        {
            
                driver.Navigate().GoToUrl("http://localhost:44108/Home/About");

                ITakesScreenshot screenShotDriver = (ITakesScreenshot)driver;

                Screenshot screenshot = screenShotDriver.GetScreenshot();

                screenshot.SaveAsFile("aboutpage.bmp", ScreenshotImageFormat.Bmp);
            }

        }
}
