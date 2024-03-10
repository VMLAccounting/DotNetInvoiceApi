namespace CityInfo.API.Models
{
    /// <summary>
    /// Represents an invoice data transfer object.
    /// </summary>
    public class InvoiceDto
    {
        /// <summary>
        /// Gets or sets the ID of the invoice.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the invoice.
        /// </summary>
        public string InvoiceNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the invoice.
        /// </summary>
        public string? Note { get; set; }
    }
}