using System;
using NUnit.Framework; // or xUnit if you prefer
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace KantarStore.UiTests
{
    [TestFixture]
    public class AddToCartVisualTest
    {
        private ChromeDriver _driver;
        private WebDriverWait _wait;
        private const string BaseUrl = "https://kantarstoreapp-akhmf3aac2apekgs.uksouth-01.azurewebsites.net/";

        [SetUp]
        public void Setup()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            _driver = new ChromeDriver(options);
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void AddProductToCart)
        {
            _driver.Navigate().GoToUrl(BaseUrl);

            // Wait for main page to load and product grid to be visible
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("div[class*='product']")));

            // Example: find the first product’s “Add to Cart” button
            var addButton = _driver.FindElement(By.XPath("//button[contains(., 'Add to Cart') or contains(., 'Add')]"));
            addButton.Click();

            // Wait for cart icon or counter to update
            _wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("span[class*='cart-count'], div[class*='cart'], i[class*='cart']")));

            // Take a visual screenshot for comparison or manual review
            var screenshot = ((ITakesScreenshot)_driver).GetScreenshot();
            var fileName = $"CartVisual_{DateTime.Now:yyyyMMdd_HHmmss}.png";
            screenshot.SaveAsFile(fileName, ScreenshotImageFormat.Png);
            Console.WriteLine($"? Screenshot saved to {fileName}");

            // Example assertion (depends on site behavior)
            var cartBadge = _driver.FindElement(By.CssSelector("span[class*='cart-count']"));
            Assert.IsTrue(Convert.ToInt32(cartBadge.Text) > 0, "Cart count should increase after adding item.");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
