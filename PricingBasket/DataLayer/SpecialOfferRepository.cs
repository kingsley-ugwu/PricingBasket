using System;
using System.Collections.Generic;

namespace PricingBasket.DataLayer
{
          /// <summary>
          /// This repository provides an object oriented view
          /// of all special offers
          /// </summary>
          class SpecialOfferRepository
          {
                    List<SpecialOffer> _specialOffers;
                    public SpecialOfferRepository()
                    {
                              _specialOffers = new List<SpecialOffer>()
                              {
                                        new SpecialOffer("APPLES", 0.1, null, 1, new DateTime(2020,12,2), new DateTime(2020, 12, 20)),
                                        new SpecialOffer("BREAD", 0.5, "BEANS", 2, null, null)
                              };
                    }

                    public List<SpecialOffer> GetAllOffers()
                    {
                              return _specialOffers;
                    }
                    
          }
}
