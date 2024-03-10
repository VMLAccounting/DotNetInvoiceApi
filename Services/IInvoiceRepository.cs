using CityInfo.API.Entities;

namespace CityInfo.API.Services
{
    /// <summary>
    /// Represents a repository for managing invoices.
    /// </summary>
    public interface IInvoiceRepository
    {
        /// <summary>
        /// Retrieves all invoices asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of invoices.</returns>
        Task<IEnumerable<Invoice>> GetInvoicesAsync();

        /// <summary>
        /// Retrieves invoices asynchronously based on specified parameters.
        /// </summary>
        /// <param name="name">The name parameter.</param>
        /// <param name="searchQuery">The search query parameter.</param>
        /// <param name="pageNumber">The page number parameter.</param>
        /// <param name="pageSize">The page size parameter.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the collection of invoices and pagination metadata.</returns>
        Task<(IEnumerable<Invoice>, PaginationMetadata)> GetInvoicesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize);

        /// <summary>
        /// Retrieves an invoice asynchronously based on the specified invoice ID.
        /// </summary>
        /// <param name="invoiceId">The invoice ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the invoice.</returns>
        Task<Invoice?> GetInvoiceAsync(int invoiceId);

        /// <summary>
        /// Checks if an invoice exists asynchronously based on the specified city ID.
        /// </summary>
        /// <param name="cityId">The city ID.</param>
        /// <returns>A task that represents the asynchronous operation. The task result indicates whether the invoice exists.</returns>
        Task<bool> InvoiceExistsAsync(int cityId);

        /// <summary>
        /// Saves changes asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task<bool> SaveChangesAsync();

        /// <summary>
        /// Adds an invoice entity.
        /// </summary>
        /// <param name="invoiceEntity">The invoice entity to add.</param>
        void AddInvoice(Invoice invoiceEntity);

        /// <summary>
        /// Deletes an invoice entity.
        /// </summary>
        /// <param name="invoiceEntity">The invoice entity to delete.</param>
        void DeleteInvoice(Invoice invoiceEntity);
    }
}
