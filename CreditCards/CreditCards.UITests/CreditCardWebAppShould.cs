// This work is based on the Pluralsight course 'Creating Automated Browser Tests with Selenium in C#' by Jason Roberts

namespace CreditCards.UITests
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    // Debug -> Start without debugging before running tests
    [TestFixture]
    public class CreditCardWebAppShould
    {
        private IWebDriver _driver;
        private string _baseUrl = "http://localhost:44108/";

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _driver = new ChromeDriver();

            // If required, change URL in CreditCards Properties -> Debug
            GoTo(_baseUrl);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown() => _driver.Dispose();

        [Test]
        public void PageTitle_IsCorrect() => Assert.AreEqual("Home Page - Credit Cards", _driver.Title);

        [Test]
        public void ReloadHomePage_Succeeds()
        {
            _driver.Navigate().Refresh();
            PageTitle_IsCorrect();
        }

        // Stupid test - doesn't test functionality
        [Test]
        public void Back_FromAboutPage_GoesHome()
        {
            GoTo($"{_baseUrl}/Home/About");
            _driver.Navigate().Back();
            PageTitle_IsCorrect();

            // todo: check Guid
        }

        // [Test]
        // public void TimeGeneratedData_IsCorrect()
        // {
        // }
        private void GoTo(string url) => _driver.Navigate().GoToUrl(url);
    }
}
