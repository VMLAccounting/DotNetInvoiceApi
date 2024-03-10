using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    /// <summary>
    /// Represents an invoice for creation.
    /// </summary>
    public class InvoiceForCreationDto
    {
        /// <summary>
        /// Gets or sets the name of the invoice.
        /// </summary>
        [Required]
        [MinLength(1)]
        [MaxLength(50)]
        public string InvoiceNumber { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the description of the invoice.
        /// </summary>
        [MaxLength(200)]
        public string? Note { get; set; }
    }
}