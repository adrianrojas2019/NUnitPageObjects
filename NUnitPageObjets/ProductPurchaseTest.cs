using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace NUnitPageObjets
{
    public class ProductPurchaseTests : IDisposable
    {
        private readonly IWebDriver _driver;

        public double WAIT_FOR_ELEMENT_TIMEOUT { get; }

        private readonly WebDriverWait _webDriverWait;

        public ProductPurchaseTests()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _driver = new ChromeDriver();
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));

        }

        public void Dispose()
        {
            _driver.Quit();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CompletePurchaseSuccessfully_WhenNewClient()
        {
            AddRocketToShoppingCart();
            ApplyCoupon();
            IncreaseProductQuantity();

            var proceedToCheckout = WaitAndFindElement(By.CssSelector("[class*='checkout-button button alt wc-forward']"));

            proceedToCheckout.Click();
            WaitUntilPageLoadsCompletely();
            var billingFirstName = WaitAndFindElement(By.Id("billing_first_name"));
            billingFirstName.SendKeys("Anton");
            var billingLastName = WaitAndFindElement(By.Id("billing_last_name"));
            billingLastName.SendKeys("Angelov");
            var billingCompany = WaitAndFindElement(By.Id("billing_company"));
            billingCompany.SendKeys("Space Flowers");
            var billingCountryWrapper = WaitAndFindElement(By.Id("select2-billing_country-container"));
            billingCountryWrapper.Click();
            var billingCountryFilter = WaitAndFindElement(By.ClassName("select2-search__field"));
            billingCountryFilter.SendKeys("Germany");

            var germanyOption = WaitAndFindElement(By.XPath("//*[contains(text(),'Germany')]"));
            germanyOption.Click();
            
            var billingAddress1 = WaitAndFindElement(By.Id("billing_address_1"));
            billingAddress1.SendKeys("1 Willi Brandt Avenue Tiergarten");

            var billingAddress2 = WaitAndFindElement(By.Id("billing_address_2"));
            billingAddress2.SendKeys("Lutzowplatz 17");

            var billingCity = WaitAndFindElement(By.Id("billing_city"));
            billingCity.SendKeys("Berlin");

            var billingZip = WaitAndFindElement(By.Id("billing_postcode"));
            billingZip.Clear();
            Thread.Sleep(1000);
            billingZip.SendKeys("10115");

            var billingPhone = WaitAndFindElement(By.Id("billing_phone"));
            billingPhone.SendKeys("+00498888999281");

            var billingEmail = WaitAndFindElement(By.Id("billing_email"));
            billingEmail.SendKeys("info@berlinspaceflowers.com");

            //WaitForAjax();

            var placeOrderButton = WaitAndFindElement(By.Id("place_order"));
            Thread.Sleep(3000);
            placeOrderButton.Click();

            //WaitForAjax();

            var receiveMessage = WaitAndFindElement(By.XPath("//h1[text() = 'Order received']"));
            Assert.That(receiveMessage.Text, Is.EqualTo("Order received"));
            Thread.Sleep(3000);
        }

        private void IncreaseProductQuantity()
        {
            Thread.Sleep(1000);
            var quantityBox = WaitAndFindElement(By.CssSelector("[class*='input-text qty text']"));
            Thread.Sleep(2000);
            quantityBox.Clear();
            Thread.Sleep(1000);
            quantityBox.SendKeys("2");
            Thread.Sleep(1000);
            //WaitForAjax();

            //WaitToBeClickable(By.CssSelector("[value*='Update cart']"));
            var updateCart = WaitAndFindElement(By.CssSelector("[value*='Update cart']"));

            Thread.Sleep(1000);
            updateCart.Click();
            Thread.Sleep(5000);
            //WaitForAjax();

            var totalSpan = WaitAndFindElement(By.XPath("//*[@class='order-total']//span"));
            Assert.That(totalSpan.Text, Is.EqualTo("114.00€"));

            Console.WriteLine("Worked?" + totalSpan.Text);
            Thread.Sleep(5000);
        }

        private void ApplyCoupon() 
        { 
            var couponCodeTextField = WaitAndFindElement(By.Id("coupon_code"));
            couponCodeTextField.Clear();
            couponCodeTextField.SendKeys("happybirthday");

            var couponButton = WaitAndFindElement(By.CssSelector("[value*='Apply coupon']"));
            couponButton.Click();
            //Console.WriteLine("Wait For Ajax");
            //WaitForAjax();
            //Console.WriteLine("Worked?");

            var messageAlert = WaitAndFindElement(By.CssSelector("[class*='woocommerce-message']"));
            Assert.That(messageAlert.Text, Is.EqualTo("Coupon code applied successfully."));
        }

        private void AddRocketToShoppingCart()
        {
            _driver.Navigate().GoToUrl("https://demos.bellatrix.solutions/");
            _driver.Manage().Window.Maximize();

            var addToCartFalcon9 = WaitAndFindElement(By.CssSelector("[data-product_id*='28']"));
            addToCartFalcon9.Click();
            var viewCartButton = WaitAndFindElement(By.CssSelector("[class*='added_to_cart wc-forward']"));
            viewCartButton.Click();

        }

        private void WaitToBeClickable(By by)
        {
            var webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            webDriverWait.Until(ExpectedConditions.ElementToBeClickable(by));
        }

        private IWebElement WaitAndFindElement(By by)
        {
            var webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(30));
            return webDriverWait.Until(ExpectedConditions.ElementExists(by));
        }

        private void WaitForAjax()
        {
            var js = (IJavaScriptExecutor)_driver;
            _webDriverWait.Until(wd => js.ExecuteScript("return jQuery.active").Equals("0"));
        }

        private void WaitUntilPageLoadsCompletely()
        {
            var js = (IJavaScriptExecutor)_driver;
            _webDriverWait.Until(wd => js.ExecuteScript("return document.readyState").Equals("complete"));

            //WebDriverWait wait = new(_driver, System.TimeSpan.FromSeconds(30));
            //webDriverWait.Until(ExpectedConditions.ElementExists(by));
            //wait.Until((wd => js.executeScript("return document.readyState").equals("complete"));
        }
    }
}