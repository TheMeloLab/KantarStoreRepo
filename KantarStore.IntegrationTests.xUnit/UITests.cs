using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace KantarStore.IntegrationTests.xUnit
{
    public class UITests
    {
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private const string BaseUrl = "https://kantarstoreapp-akhmf3aac2apekgs.uksouth-01.azurewebsites.net/";

        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Fact]
        public void HeartBeatTest()
        {
            Setup();

            _driver.Navigate().GoToUrl(BaseUrl);

            // Wait for main page to load and product grid to be visible
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[class*='product']")));

            // Example: find the first product’s “Add to Cart” button
            var addButton = _driver.FindElement(By.XPath("//button[contains(., 'Add to Cart') or contains(., 'Add')]"));

            addButton.Click();

            Assert.True(true);
          
        }
    }
}