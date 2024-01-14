using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => _lines;

        private List<CartLine> _lines = new List<CartLine>();

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            // if the product is already in the cart, increment its quantity
            if (_lines.Any(l => l.Product.Id == product.Id))
            {
                _lines.Where(l => l.Product.Id == product.Id).First().Quantity += quantity;
            }
            else // add the product to the cart
            {
                _lines.Add(new CartLine { Product = product, Quantity = quantity });
            }
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            _lines.RemoveAll(l => l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue() =>
            _lines.Sum(l => l.Product.Price * l.Quantity);

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue() =>
            GetTotalValue() / _lines.Sum(l => l.Quantity);

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            if (_lines.Any(l => l.Product.Id == productId))
                return _lines.Where(l => l.Product.Id == productId).First().Product;
            else return null;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index) =>
            _lines.ToArray()[index];

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear() => _lines.Clear();
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
