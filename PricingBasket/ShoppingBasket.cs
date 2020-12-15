using PricingBasket.DataLayer;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace PricingBasket
{
          public class ShoppingBasket
          {
                    private static List<Item> allItems = new ItemRepository().GetAllItems();

                    public static bool ValidateUserInput(string userInput)
                    {
                              if (userInput == null)
                                        throw new ArgumentNullException();

                              //check if first word of user input is valid
                              return userInput.Split(' ')[0].ToUpper() != System.Configuration.ConfigurationManager.AppSettings["INPUTPREFIX"].ToUpper() ? false : true;
                    }

                    public static double CalculateSubTotal(string userInput)
                    {
                              var userInputAsArray = userInput.ToUpper().Split(' ');

                              //iterate over items and compute sub total
                              double subTotal = 0;
                              for (int i = 1; i < userInputAsArray.Length; i++)
                              {
                                        if (allItems.Find(x => x.ItemName == userInputAsArray[i])?.ItemName != null)
                                                  subTotal += allItems.Find(x => x.ItemName == userInputAsArray[i]).ItemPrice;
                              }
                              return subTotal;
                    }

                    public static string ListActiveOffers(string userInput)
                    {
                              var allOffers = new SpecialOfferRepository().GetAllOffers();
                              var userInputAsArray = userInput.ToUpper().Split(' ');
                              double discount = 0;
                              string message = null;

                              for (int i = 1; i < userInputAsArray.Length; i++)
                              {
                                        if (allItems.Find(x => x.ItemName == userInputAsArray[i]) != null)
                                        {
                                                  if (allOffers.Find(x => x.ItemOnSpecialOffer == userInputAsArray[i]) != null)
                                                  {
                                                            var currentOffer = allOffers.Find(x => x.ItemOnSpecialOffer == userInputAsArray[i]);
                                                            if (currentOffer.DependentItem == null)
                                                            {
                                                                      discount = currentOffer.SpecialOfferDiscount;
                                                                      TextInfo myTI = new CultureInfo("en-GB", false).TextInfo;
                                                                      message += myTI.ToTitleCase(currentOffer.ItemOnSpecialOffer.ToLower()) +
                                                                                          " " + (discount * 100) + "% off: -" + (discount * 100) + "p";
                                                            }
                                                  }
                                        }
                              }
                              return message ?? "(No offers available)";
                    }

                    public static double CalculateTotal(string userInput)
                    {
                              var allOffers = new SpecialOfferRepository().GetAllOffers();
                              var userInputAsArray = userInput.ToUpper().Split(' ');
                              double itemCost, discount, subTotal = 0;
                              int maxNumberOfTimesToAppplyDiscount = 0;
                              int discountApplied = 0;

                              for (int i = 1; i < userInputAsArray.Length; i++)
                              {
                                        var currentItem = allItems.Find(x => x.ItemName == userInputAsArray[i]);
                                        if (currentItem != null)
                                        {
                                                  itemCost = currentItem.ItemPrice;

                                                  if (allOffers.Find(x => x.ItemOnSpecialOffer == userInputAsArray[i]) != null)
                                                  {
                                                            var currentOffer = allOffers.Find(x => x.ItemOnSpecialOffer == userInputAsArray[i]);

                                                            if (currentOffer.DependentItem == null)
                                                            {
                                                                      discount = currentOffer.SpecialOfferDiscount;
                                                                      subTotal += itemCost - (itemCost * discount);
                                                            }
                                                            else
                                                            {
                                                                      var depItem = currentOffer.DependentItem;
                                                                      string[] source = userInput.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                                                                      var itemCount = (from word in source
                                                                                       where word.ToLowerInvariant() == depItem.ToLowerInvariant()
                                                                                       select word).Count();
                                                                      maxNumberOfTimesToAppplyDiscount = itemCount / currentOffer.DependentItemQuantity;

                                                                      if (itemCount >= currentOffer.DependentItemQuantity && discountApplied < maxNumberOfTimesToAppplyDiscount)
                                                                      {
                                                                                discount = currentOffer.SpecialOfferDiscount;
                                                                                subTotal += itemCost - (itemCost * discount);
                                                                                discountApplied += 1;
                                                                      }
                                                                      else
                                                                                subTotal += itemCost;
                                                            }
                                                  }
                                                  else
                                                            subTotal += itemCost;
                                        }
                              }
                              return subTotal;
                    }
          }
}
