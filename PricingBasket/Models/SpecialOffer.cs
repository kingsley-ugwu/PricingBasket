using System;

namespace PricingBasket
{
          // This class is used to provide a list of special offers that apply to items
          class SpecialOffer
          {
                    public string ItemOnSpecialOffer { get; set; }
                    public double SpecialOfferDiscount { get; set; }
                    public string DependentItem { get; set; }
                    public int DependentItemQuantity { get; set; }
                    public DateTime? OfferStartDate { get; set;  }
                    public DateTime? OfferEndtDate { get; set; }

                    public SpecialOffer(string itemOnSpecialOffer, double specialOfferDiscount, string dependentItem, int dependentItemQuantity, DateTime? offerStartDate, DateTime? offerEndDate)
                    {
                              ItemOnSpecialOffer = itemOnSpecialOffer;
                              SpecialOfferDiscount = specialOfferDiscount;
                              DependentItem = dependentItem;
                              DependentItemQuantity = dependentItemQuantity;
                              OfferStartDate = OfferStartDate;
                              OfferEndtDate = OfferEndtDate;
                    }
          }
}
