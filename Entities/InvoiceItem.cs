using System;
using CityInfo.API.Entities;

namespace CityInfo.Api.Entities
{
    /// <summary>
    /// Represents an invoice item.
    /// </summary>
    public class InvoiceItem
    {
        /// <summary>
        /// Gets or sets the ID of the invoice item.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the description of the invoice item.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the unit type of the invoice item.
        /// </summary>
        public string UnitType { get; set; } = "Unit";

        /// <summary>
        /// Gets or sets the quantity of the invoice item.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Gets or sets the price per unit of the invoice item.
        /// </summary>
        public decimal PricePerUnit { get; set; }

        /// <summary>
        /// Gets or sets the total price of the invoice item.
        /// </summary>
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Gets or sets the invoice associated with the invoice item.
        /// </summary>
        public Invoice? Invoice { get; set; }

        private InvoiceItem()
        {
            Description = string.Empty;
            Quantity = 0;
            PricePerUnit = 0;
            TotalPrice = 0;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceItem"/> class.
        /// </summary>
        /// <param name="id">The ID of the invoice item.</param>
        /// <param name="description">The description of the invoice item.</param>
        /// <param name="quantity">The quantity of the invoice item.</param>
        /// <param name="pricePerUnit">The price per unit of the invoice item.</param>
        /// <param name="invoice">The invoice associated with the invoice item.</param>
        public InvoiceItem(int id, string description, int quantity, decimal pricePerUnit, Invoice invoice)
        {
            Id = id;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Quantity = quantity;
            PricePerUnit = pricePerUnit;
            TotalPrice = quantity * pricePerUnit;
            Invoice = invoice ?? throw new ArgumentNullException(nameof(invoice));
        }
    }
}
