namespace PricingBasket
{
          //This class is used to store items that will be added into a shopping basket
          class Item
          {
                    public string ItemName { get; set; }
                    public double ItemPrice { get; set; }
                    public string ItemUnit { get; set; }

                    public Item(string itemName, double itemPrice, string itemUnit)
                    {
                              ItemName = itemName;
                              ItemPrice = itemPrice;
                              ItemUnit = itemUnit;
                    }
          }
}
