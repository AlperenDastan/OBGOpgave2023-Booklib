using Booklib;
using Newtonsoft.Json.Bson;

namespace BooklibTests
{
    [TestClass]
    public class BooklibUnitTests
    {
        private readonly Book _book = new Book() { Id = 1, Title = "Six Of Crows", Price = 600 };
        private readonly Book _nulltitle = new Book() { Id = 2, Title = null, Price = 600 };
        private readonly Book _titleoutofrange = new Book() { Id = 3, Title = "Bo", Price = 600 };
        private readonly Book _pricehigh = new Book() { Id = 4, Title = "Shadow & Bone", Price = 1300 }; 
        private readonly Book _pricelow = new Book() { Id = 5, Title = "The Silmarillion", Price = -1 }; 

        [TestMethod]
        public void ToStringTest()
        {
            Assert.AreEqual("1 Six Of Crows 600", _book.ToString());
        }

        [TestMethod]
        public void ValidateTitleTest()
        {
            _book.ValidateTitle();
            Assert.ThrowsException<ArgumentNullException>(() => _nulltitle.ValidateTitle());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _titleoutofrange.ValidateTitle());
        }

        [TestMethod]
        public void ValidatePriceTest()
        {
            _book.ValidatePrice();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _pricelow.ValidatePrice());
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _pricehigh.ValidatePrice());
        }

        [TestMethod]
        public void ValidateTest()
        {
            _book.Validate();
        }
    }
}