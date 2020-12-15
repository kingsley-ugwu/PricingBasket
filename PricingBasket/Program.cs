using static System.Console;
using static System.String;

namespace PricingBasket
{
          public class Program
          {
                    /// <summary>This is the entry point to the program.
                    /// <para>This program returns the  price of a given basket
                    /// of good taking into account any offers</para></summary>
                    public static void Main(string[] args)
                    {
                              var input = ReadLine(); //Get input from the user
                              while (!ShoppingBasket.ValidateUserInput(input)) // Validate user input is in the format expected e.g. PriceCalculator item1 item2 item3 …
                              {
                                        WriteLine("The first word must be PriceCalculator (this word is not case-sensitive), use Ctrl+C to close this program");
                                        input = ReadLine();
                              }

                              // Respond with desired output to the console
                              WriteLine("Subtotal: " + Format("{0:C}", ShoppingBasket.CalculateSubTotal(input)));
                              WriteLine(ShoppingBasket.ListActiveOffers(input));
                              WriteLine("Total: " + Format("{0:C}", ShoppingBasket.CalculateTotal(input)));

                    }
          }
}
