using System;
using System.Collections.Generic;
using ChClient.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ChClientUnitTests
{
    [TestClass]
    public class ProductUnitTests
    {
        //mock-ok
        //feltételezhetjük, hogy nem null listákat kapunk adatelérési oldalról
        //(akkor se, ha 0 darab értéket kapunk DB oldalról)
        
        [TestMethod]
        public void EmptyProductTest()
        {
            Product item = new Product();

            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void StringMWInputProductTest()
        {
            Product item = new Product();
            item.MWString = "szöveges érték";
            item.RatioString="42";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void StringRatioInputProductTest()
        {
            Product item = new Product();
            item.MWString = "";
            item.RatioString = "szöveges érték";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void NumberMWInputProductTest()
        {
            Product item = new Product();
            item.MWString = "12";
            item.RatioString = "";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void NumberRatioInputProductTest()
        {
            Product item = new Product();
            item.MWString = "";
            item.RatioString = "56";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreNotEqual(0, errors.Count);
        }

        [TestMethod]
        public void BothInputGivenProductTest()
        {
            Product item = new Product();
            item.MWString = "24";
            item.RatioString = "63";
            List<OutputMessage> errors = new List<OutputMessage>();

            errors = item.Validate();

            Assert.AreEqual(0, errors.Count);
        }

    }
}
