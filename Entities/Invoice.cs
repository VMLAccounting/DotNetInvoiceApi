using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CityInfo.Api.Entities;

namespace CityInfo.API.Entities
{

    /// <summary>
    /// Represents the available payment methods for an invoice.
    /// </summary>
    public enum PaymentMethod
    {
        /// <summary>
        /// Represents payment in cash.
        /// </summary>
        Cash,

        /// <summary>
        /// Represents payment using a credit card.
        /// </summary>
        CreditCard,

        /// <summary>
        /// Represents payment via bank transfer.
        /// </summary>
        BankTransfer,

        /// <summary>
        /// Represents payment via PayPal.
        /// </summary>
        PayPal
    }

    /// <summary>
    /// Represents the status of an invoice.
    /// </summary>
    public enum InvoiceStatus
    {
        /// <summary>
        /// The invoice is in draft status.
        /// </summary>
        Draft,

        /// <summary>
        /// The invoice has been sent.
        /// </summary>
        Sent,

        /// <summary>
        /// The invoice has been paid.
        /// </summary>
        Paid,

        /// <summary>
        /// The invoice is closed.
        /// </summary>
        Closed,

        /// <summary>
        /// The invoice has been canceled.
        /// </summary>
        Canceled,

        /// <summary>
        /// The invoice has been refunded.
        /// </summary>
        Refunded
    }

    /// <summary>
    /// Represents the available payment terms for an invoice.
    /// </summary>
    public enum PaymentTerm
    {
        /// <summary>
        /// The invoice is due on receipt.
        /// </summary>
        DueOnReceipt,

        /// <summary>
        /// The invoice is due in 15 days.
        /// </summary>
        DueIn15Days,

        /// <summary>
        /// The invoice is due in 30 days.
        /// </summary>
        DueIn30Days,

        /// <summary>
        /// The invoice is due in 45 days.
        /// </summary>
        DueIn45Days,

        /// <summary>
        /// The invoice is due in 60 days.
        /// </summary>
        DueIn60Days,

        /// <summary>
        /// The invoice is due in 90 days.
        /// </summary>
        Custom
    }

    /// <summary>
    /// Represents an invoice.
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// Gets or sets the ID of the invoice.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the invoice.
        /// </summary>
        [Required]
        [MinLength(10)]
        [MaxLength(50)]
        public string InvoiceNumber { get; set; }

        /// <summary>
        /// Gets or sets the description of the invoice.
        /// </summary>
        [MaxLength(200)]
        public string Note { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the date of the invoice.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the due date of the invoice.
        /// </summary>
        public DateTime DueDate { get; set; }

        /// <summary>
        /// Gets or sets the payment terms of the invoice.
        /// </summary>
        public PaymentTerm PaymentTerms { get; set; }

        /// <summary>
        /// Gets or sets the name of the client.
        ///         ///        ///  </summary>
        public string ClientName { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the address of the client.
        /// </summary>
        public string ClientAddress { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the items of the invoice.
        /// </summary>
        public List<InvoiceItem> Items { get; set; }

        /// <summary>
        /// Gets or sets the total amount of the invoice.
        /// </summary>
        public decimal TotalAmount { get; set; } = 0;

        /// <summary>
        /// Gets or sets a value indicating whether the invoice is paid.
        /// </summary>
        public bool Paid { get; set; } = false;

        /// <summary>
        /// Gets or sets the payment method of the invoice.
        /// </summary>
        public PaymentMethod PaymentMethod { get; set; } = PaymentMethod.Cash;

        /// <summary>
        /// Gets or sets the currency of the invoice.
        /// </summary>
        /// <remarks>
        /// This is the currency in which the invoice is charged.
        /// </remarks>
        public string InvoiceCurrency { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets the price currency of the invoice.
        /// </summary>
        /// <remarks>
        /// This is the currency in which the items are charged.
        /// </remarks>
        public string ItemCurrency { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the currency rate of the invoice.
        /// </summary>
        /// <remarks>
        /// This is the rate between the invoice currency and the item currency.
        /// It is used to convert the item currency to the invoice currency.
        /// eg. 
        /// InvoiceCurrency = "USD"
        /// ItemCurrency = "EUR"
        /// CurrencyRate = "1.2"
        /// InvoiceCurrency = ItemCurrency / CurrencyRate
        /// </remarks>
        public string CurrencyRate { get; set; } = string.Empty;


        /// <summary>
        /// Gets or sets the status of the invoice.
        /// </summary>
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Draft;

        private Invoice()
        {
            InvoiceNumber = string.Empty;
            Items = new List<InvoiceItem>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Invoice"/> class with the specified name.
        /// </summary>
        /// <param name="invoiceNumber">The name of the invoice.</param>
        /// <param name="items">The items of the invoice.</param>
        public Invoice(string invoiceNumber, List<InvoiceItem> items)
        {
            InvoiceNumber = invoiceNumber;
            Items = items;
        }
    }
}
