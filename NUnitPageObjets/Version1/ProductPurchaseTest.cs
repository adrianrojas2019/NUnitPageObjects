using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager.Helpers;

namespace NUnitPageObjets.Version1
{
    public class ProductPurchaseTests : IDisposable
    {
        private static IWebDriver? _driver;
        private static readonly MainPage? _mainPage;

        public ProductPurchaseTests()
        {
            new DriverManager().SetUpDriver(new ChromeConfig(), VersionResolveStrategy.MatchingBrowser);
            _driver = new ChromeDriver();
           
        }

        public void Dispose()
        {
            _driver.Quit();
        }

        [Test]
        public void PurchaseFalcon9()
        {
            _mainPage.GoTo();
            _mainPage.AddRocketToShoppingCart("Falcon 9");

        }

    }
}