using NUnit.Framework;

namespace NUnitPageObjets.Version1.Cart
{
    public class Assertions
    {
        private readonly Map _map;
        
        public Assertions(Map map) 
        { 
            _map = map;
        }

        public void AssertCouponAppliedSuccessfully()
        {
            //sert.AreEqual("Coupon code applied successfully.", _map.MessageAlert.Text);
            Assert.That(_map.MessageAlert.Text, Is.EqualTo("Coupon code applied successfully."));
        }

        public void AssertTotalPrice(string expectedPrice)
        {
            Assert.That(_map.TotalSpan.Text, Is.EqualTo(expectedPrice));
        }
    }

}
