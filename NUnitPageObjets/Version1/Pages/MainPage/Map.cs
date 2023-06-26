using NUnitPageObjets.Version1.Pages;
using OpenQA.Selenium;

namespace NUnitPageObjets.Version1.Main
{
    public class Map : BaseMap
    {
        public Map(IWebDriver driver) : base(driver) 
        { 
        }

        public IWebElement AddToCartFalcon9 => WaitAndFindElement(By.CssSelector("[data-product_id*='28']"));
       
        //Similar to below approach
        //public IWebElement AddToCartFalcon9
        //{
        //    get
        //    {
        //        return WaitAndFindElement(By.CssSelector("[data-product_id*='28']"));
        //    }
        //}

{        public IWebElement viewCartButton => WaitAndFindElement(By.CssSelector("[class*='added_to_cart wc-forward']"));

        public IWebElement GetProductBoxByName(string name)
        {
            return WaitAndFindElement(By.XPath($"//h2[text()='{name}']/parent::a/following-sibling::a"));
        }
    }
}
