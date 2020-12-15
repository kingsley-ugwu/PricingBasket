using NUnit.Framework;
using System;
using System.IO;

namespace PricingBasket.Tests
{
          [TestFixture]
          public class PricingBasketFixtures
          {
                    [SetUp]
                    public void Init()
                    {
                              System.Configuration.ConfigurationManager.AppSettings["INPUTPREFIX"] = ("PriceCalculator");
                    }

                    [Test]
                    public void Main_InvalidPrefix_ArgumentNullExceptionThrown()
                    {
                              //arrange - structure of what to test
                              Console.SetOut(new StringWriter());
                              var input = new StringReader(@"Test1 
                                                                                          Test2");
                              Console.SetIn(input);

                              //act - make the call to the operations we are trying to assert
                              var ex = Assert.Throws<ArgumentNullException>(() => Program.Main(new string[] { }));

                              //assert - assert the results
                              Assert.That(ex.Message, Is.EqualTo("Value cannot be null."));
                    }

                    [Test]
                    public void Main_ValidInputValidItems_ResponseReturnedWithNoOffer()
                    {
                              //arrange - structure of what to test
                              var output = new StringWriter();
                              Console.SetOut(output);
                              Console.SetIn(new StringReader("PriceCalculator Milk"));
                              var expectedOutput = "Subtotal: £1.30\r\n(No offers available)\r\nTotal: £1.30\r\n";

                              //act - make the call to the operations we are trying to assert
                              Program.Main(new string[] { });

                              //assert - assert the results
                              Assert.AreEqual(expectedOutput, output.ToString());

                    }

                    [Test]
                    public void Main_ValidInputValidItemsCapitalPrefix_ResponseReturnedWithNoOffer()
                    {
                              //arrange - structure of what to test
                              var output = new StringWriter();
                              Console.SetOut(output);
                              Console.SetIn(new StringReader("PRICECALCULATOR Milk"));
                              var expectedOutput = "Subtotal: £1.30\r\n(No offers available)\r\nTotal: £1.30\r\n";

                              //act - make the call to the operations we are trying to assert
                              Program.Main(new string[] { });

                              //assert - assert the results
                              Assert.AreEqual(expectedOutput, output.ToString());

                    }

                    [Test]
                    public void Main_ValidInputValidItems_ResponseReturnedWithOffer()
                    {
                              //arrange - structure of what to test
                              var output = new StringWriter();
                              Console.SetOut(output);
                              Console.SetIn(new StringReader("PriceCalculator Apples Milk Bread"));
                              var expectedOutput = "Subtotal: £3.10\r\nApples 10% off: -10p\r\nTotal: £3.00\r\n";

                              //act - make the call to the operations we are trying to assert
                              Program.Main(new string[] { });

                              //assert - assert the results
                              Assert.AreEqual(expectedOutput, output.ToString());

                    }

                    [Test]
                    public void Main_ValidInputValidAndInvalidItems_ResponseReturnedWithOffer()
                    {
                              //arrange - structure of what to test
                              var output = new StringWriter();
                              Console.SetOut(output);
                              Console.SetIn(new StringReader("PriceCalculator Apples Milk Bread Apples1"));
                              var expectedOutput = "Subtotal: £3.10\r\nApples 10% off: -10p\r\nTotal: £3.00\r\n";

                              //act - make the call to the operations we are trying to assert
                              Program.Main(new string[] { });

                              //assert - assert the results
                              Assert.AreEqual(expectedOutput, output.ToString());

                    }

                    [Test]
                    public void CalculateSubTotal_1Apple1Milk1Bread_ReturnSubTotal()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Apples Milk Bread";
                              double expected = 3.1;

                              //act - make the call to the operations we are trying to assert
                              double actual = ShoppingBasket.CalculateSubTotal(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual, 0.1);
                    }

                    [Test]
                    public void CalculateTotal_1Apple1Milk1Bread_ReturnTotal()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Apples Milk Bread";
                              double expected = 3;

                              //act - make the call to the operations we are trying to assert
                              double actual = ShoppingBasket.CalculateTotal(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual);
                    }

                    [Test]
                    public void ListActiveOffers_ItemOnOffer_ReturnsOffersDetails()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Apples Milk Bread";
                              string expected = "Apples 10% off: -10p";

                              //act - make the call to the operations we are trying to assert
                              string actual = ShoppingBasket.ListActiveOffers(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual);
                    }

                    [Test]
                    public void ListActiveOffers_ItemNotOnOffer_ReturnNoOffersAvailable()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Milk";
                              string expected = "(No offers available)";

                              //act - make the call to the operations we are trying to assert
                              string actual = ShoppingBasket.ListActiveOffers(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual);
                    }

                    [Test]
                    public void ValidateUserInput_InvalidPrefix_ReturnFalse()
                    {
                              //arrange - structure of what to test
                              var input = "NotPriceCalculator Milk";
                              bool expected = false;

                              //act - make the call to the operations we are trying to assert
                              bool actual = ShoppingBasket.ValidateUserInput(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual);
                    }

                    [Test]
                    public void ValidateUserInput_ValidPrefix_ReturnTrue()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Milk";
                              bool expected = true;

                              //act - make the call to the operations we are trying to assert
                              bool actual = ShoppingBasket.ValidateUserInput(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual);
                    }
                    
                    [Test]
                    public void CalculateTotal_2Beans1Bread_OfferApplied()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Beans Beans Bread";
                              double expected = 1.7;

                              //act - make the call to the operations we are trying to assert
                              double actual = ShoppingBasket.CalculateTotal(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual, 0.1);
                    }

                    [Test]
                    public void CalculateTotal_1Beans1Bread_NoOfferApplied()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Beans Bread";
                              double expected = 1.45;

                              //act - make the call to the operations we are trying to assert
                              double actual = ShoppingBasket.CalculateTotal(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual, 0.1);
                    }

                    [Test]
                    public void CalculateTotal_2Beans2Bread_OfferAppliedOnce()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Beans Beans Bread Bread";
                              double expected = 2.5;

                              //act - make the call to the operations we are trying to assert
                              double actual = ShoppingBasket.CalculateTotal(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual, 0.1);
                    }

                    [Test]
                    public void CalculateTotal_4Beans2Bread_OfferAppliedTwice()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Beans Beans Beans Beans Bread Bread";
                              double expected = 3.4;

                              //act - make the call to the operations we are trying to assert
                              double actual = ShoppingBasket.CalculateTotal(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual, 0.1);
                    }

                    [Test]
                    public void CalculateTotal_InvalidItem_ReturnZero()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Beans1";
                              double expected =0;

                              //act - make the call to the operations we are trying to assert
                              double actual = ShoppingBasket.CalculateTotal(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual, 0.1);
                    }

                    [Test]
                    public void ListActiveOffers_InvalidItem_ReturnNoOffersAvailable()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Beans1";
                              string expected = "(No offers available)";

                              //act - make the call to the operations we are trying to assert
                              string actual = ShoppingBasket.ListActiveOffers(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual);
                    }

                    [Test]
                    public void CalculateSubTotal_InvalidItem_ReturnZero()
                    {
                              //arrange - structure of what to test
                              var input = "PriceCalculator Beans1";
                              double expected = 0;

                              //act - make the call to the operations we are trying to assert
                              double actual = ShoppingBasket.CalculateSubTotal(input);

                              //assert - assert the results
                              Assert.AreEqual(expected, actual);
                    }
          }
}
