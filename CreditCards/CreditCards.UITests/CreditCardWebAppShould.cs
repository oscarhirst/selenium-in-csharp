// This work is based on the Pluralsight course 'Creating Automated Browser Tests with Selenium in C#' by Jason Roberts

namespace CreditCards.UITests
{
    using NUnit.Framework;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;

    [TestFixture]
    public class CreditCardWebAppShould
    {
        [Test]
        public void LoadApplicationPage()
        {
            using IWebDriver driver = new ChromeDriver();
        }
    }
}
