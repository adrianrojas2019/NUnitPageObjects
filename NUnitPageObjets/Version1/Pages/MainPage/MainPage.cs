using NUnitPageObjets.Version1.Main;
using OpenQA.Selenium;

namespace NUnitPageObjets.Version1
{
    public class MainPage : WebPage

    { 
        public MainPage(IWebDriver driver) : base(driver)
        {
            Map = new Map(driver);
            //Assertios = new Assertions(Map);
        }
        public Map Map { get; private set; }

        //public Assertions Assertions { get; }

        protected override string Url => "https://demos.bellatrix.solutions/";
        public void AddRocketToShoppingCart(string rocketName)
        {
            GoTo();
            Map.GetProductBoxByName(rocketName).Click();
            Map.viewCartButton.Click();
        }

        protected override void WaitForPageToLoad()
        {
            WaitAndFindElement(By.CssSelector("[data-product_id*='28']"));
        }
    }
}
