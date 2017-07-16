using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Logic.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        #region Auxiliary data

        private readonly Customer customer =
            new Customer("Jeffrey Richter", "+1 (425) 555-0100", 1000000);

        #endregion


        #region IFormattable implementation ToString method tests

        [TestCase("G", ExpectedResult = "Jeffrey Richter, 1,000,000.00, +1 (425) 555-0100")]
        [TestCase("NR", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("nr", ExpectedResult = "Jeffrey Richter, 1,000,000.00")]
        [TestCase("RP", ExpectedResult = "1,000,000.00, +1 (425) 555-0100")]
        [TestCase("R", ExpectedResult = "1,000,000.00")]
        [TestCase("P", ExpectedResult = "+1 (425) 555-0100")]
        [Category("ToString tests")]
        public string IFormattableToString_AllOk_FormatedString(string format)
        {
            return customer.ToString(format);
        }


        #endregion


        #region Object's ToString tests

        [Test]
        [Category("ToString tests")]
        public void ToString_AllOk_FormatedString()
        {
            Assert.AreEqual("Jeffrey Richter, 1,000,000.00, +1 (425) 555-0100", customer.ToString());
        }

        #endregion


        [Test]
        [Category("ToString tests")]
        [Ignore("")]
        public void CustomToString_AllOk_FormatedString()
        {
            Assert.AreEqual("Name: Jeffrey Richter, Revenue: 1,000,000.00, Phone: +1 (425) 555-0100", customer.ToString("A", new CustomProvider()));
        }

        #region Exceptions tests

        [TestCase("QW")]
        [TestCase("NAME")]
        [TestCase("Ph")]
        [TestCase("NNR")]
        [Category("ToString tests")]
        public void ToString_IncorrectArguments_ThrowsArgumentExceptions(string format)
        {
            Assert.Catch<ArgumentException>(() => customer.ToString(format));
        }

        #endregion

    }
}
