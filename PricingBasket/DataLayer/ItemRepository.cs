using System.Collections.Generic;
using System.Linq;

namespace PricingBasket.DataLayer
{
         /// <summary>
         /// This repository provides an object oriented view
         /// of the Items persistence layer
         /// </summary>
          class ItemRepository
          {
                    List<Item> _items;
                    public ItemRepository()
                    {
                              _items = new List<Item>()
                              {
                                        new Item("BEANS", 0.65, "can"),
                                        new Item("BREAD", 0.8, "loaf"),
                                        new Item("MILK", 1.3, "bottle"),
                                        new Item("APPLES", 1, "bag")
                              };
                    }

                    public List<Item> GetAllItems()
                    {
                              return _items;
                    }
          }
}
