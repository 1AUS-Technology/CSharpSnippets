using System.ComponentModel.DataAnnotations;

namespace SecureByDesign
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var qty = new OrderQuantity(-1);
            Console.WriteLine(qty);

        }



        private static void ValidateOrder(string customerName, int quantity)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(customerName);

            if (quantity < 1 || quantity > 1000_000)
            {
                string message = $"Quantity must be between {1} and {1000_000}";
                throw new ArgumentOutOfRangeException(nameof(quantity), message);
            }
        }

        private static void UpdateOrder(string customerName, OrderQuantity updatedQuantity)
        {
            Console.WriteLine("Order updated");

        }

        private static void DispatchOrder(string customerName, OrderQuantity quantity)
        {
            Console.WriteLine("Order dispatched");
        }
    }

    public record struct OrderQuantity
    {
        public OrderQuantity(int quantity)
        {
            if (quantity < 1 || quantity > 1000_000)
            {
                string message = $"Quantity must be between {1} and {1000_000}";
                throw new ArgumentOutOfRangeException(nameof(quantity), message);
            }
            Quantity = quantity;
        }

        public int Quantity { get; }
    }
}



