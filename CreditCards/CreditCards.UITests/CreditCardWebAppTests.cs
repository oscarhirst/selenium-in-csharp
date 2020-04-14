// This work is based on the Pluralsight course 'Creating Automated Browser Tests with Selenium in C#' by Jason Roberts

namespace CreditCards.UITests
{
    using System;
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    [TestFixture]
    public class CreditCardWebAppTests
    {
        private IWebDriver _driver;
        private string _baseUrl = "http://localhost:44108/";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _driver = new ChromeDriver();
        }

        [SetUp]
        public void SetUp() => GoTo(_baseUrl); // If required, change URL in CreditCards Properties -> Debug

        [OneTimeTearDown]
        public void OneTimeTearDown() => _driver.Dispose();

        [Test]
        public void PageTitle_IsCorrectForHomePage() => Assert.AreEqual("Home Page - Credit Cards", _driver.Title);

        [Test]
        public void ReloadHomePage_Succeeds()
        {
            _driver.Navigate().Refresh();
            PageTitle_IsCorrectForHomePage();
        }

        [Test]
        public void HomesGeneratedVisitGuid_AfterNavigatingBackFromAboutPage_HasNewValue()
        {
            string initialVisitGuid = _driver.FindElement(By.Id("GenerationToken")).Text;

            GoTo($"{_baseUrl}/Home/About");
            _driver.Navigate().Back();

            PageTitle_IsCorrectForHomePage();
            Assert.AreNotEqual(initialVisitGuid, _driver.FindElement(By.Id("GenerationToken")).Text);
        }

        // [Test]
        // public void TimeGeneratedData_IsCorrect()
        // {
        // }
        public void AssertOnApplicationPage()
        {
            Assert.AreEqual("Credit Card Application - Credit Cards", _driver.Title);
            Assert.AreEqual(_driver.Url, "http://localhost:44108/Apply");
        }

        [Test]
        public void ApplyButtons__NewLowRateApplyButton_AfterClick_NavigatesToApplicationPage()
        {
            IWebElement lowRateApplyLink = _driver.FindElement(By.Name("ApplyLowRate"));
            lowRateApplyLink.Click();

            AssertOnApplicationPage();
        }

        [Test]
        public void ApplyButtons__EasyApplyButton_AfterClick_NavigatesToApplicationPage()
        {
            MoveCarouselAndClickApplyButton();

            AssertOnApplicationPage();
        }

        [Test]
        public void ApplyButtons__CustomerServiceApplyButton_AfterClick_NavigatesToApplicationPage()
        {
            MoveCarouselAndClickApplyButton(2);

            AssertOnApplicationPage();
        }

        [Test]
        public void ProductsAndRatesTable_IsDisplayedCorrectly()
        {
            var firstProduct = _driver.FindElement(By.TagName("td")).Text;
            Assert.AreEqual("Easy Credit Card", firstProduct);

            // todo
        }

        [Test]
        public void RandomGreetingApplyLink_AfterClicked_NavigatesToApplicationPage()
        {
            // _driver.FindElement(By.XPath("//a[text()[contains(.,'- Apply Now!')]]")).Click();
            _driver.FindElement(By.PartialLinkText("- Apply Now!")).Click();

            AssertOnApplicationPage();
        }

        private void GoTo(string url) => _driver.Navigate().GoToUrl(url);

        private void MoveCarouselAndClickApplyButton(int moves = 1)
        {
            for (var i = 0; i <= moves; i++)
            {
                _driver.FindElement(By.CssSelector("[data-slide='next']")).Click();
            }

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(1));

            // Waits until button is present on html (while carousel spins), up to a second
            wait.Until((d) => d.FindElement(By.PartialLinkText("Apply Now!"))).Click();
        }
    }
}
