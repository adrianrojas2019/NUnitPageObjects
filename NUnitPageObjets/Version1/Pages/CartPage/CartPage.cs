using OpenQA.Selenium;
using NUnitPageObjets.Version1.Cart;

namespace NUnitPageObjets.Version1
{
   public class CartPage : WebPage
    {
        public CartPage(IWebDriver driver) : base(driver) 
        {
            Map = new Map(driver);
            Assertions = new Assertions(Map);
        }  
        public Map Map { get; set; }
        public Assertions Assertions { get; set; }

        protected override string Url => "https://demos.bellatrix.solutions/";

        public void ApplyCoupon(string coupon)
        {
            Map.QuantityBox.Clear();
            Map.CouponCodeTextField.SendKeys(coupon);
            Map.ApplyCouponButton.Click();
            WaitForAjax();
        }

        public void IncreaceProductQuantity(int newQuantity)
        {
            Map.QuantityBox.Clear();
            Map.QuantityBox.SendKeys(newQuantity.ToString());
            Map.UpdateCart.Click();
            WaitForAjax();
        }

        public void ProceedToCheckout()
        {
            Map.ProceedToChechout.Click();
            WaitUntilPageLoadsCompletely();
        }

        public string GetTotal()
        {
            return Map.TotalSpan.Text;
        }

        public string GetMessageNotification()
        {
            return Map.MessageAlert.Text;
        }

        protected override void WaitForPageToLoad()
        {
            WaitAndFindElement(By.Id("coupon_code"));
        }

    }
}
