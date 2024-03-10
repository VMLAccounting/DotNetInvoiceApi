using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Services
{
    /// <summary>
    /// Represents a repository for managing invoices.
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly InvoiceContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoiceRepository"/> class.
        /// </summary>
        /// <param name="context">The invoice context.</param>
        public InvoiceRepository(InvoiceContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        /// <inheritdoc/>
        public void AddInvoice(Invoice invoiceEntity)
        {
            _context.Invoices.Add(invoiceEntity);
        }

        /// <inheritdoc/>
        public void DeleteInvoice(Invoice invoiceEntity)
        {
            _context.Invoices.Remove(invoiceEntity);
        }

        /// <inheritdoc/>
        public async Task<Invoice?> GetInvoiceAsync(int invoiceId)
        {
            return await _context.Invoices.FirstOrDefaultAsync(c => c.Id == invoiceId);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<Invoice>> GetInvoicesAsync()
        {
            return await _context.Invoices.OrderBy(c => c.Id).ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<(IEnumerable<Invoice>, PaginationMetadata)> GetInvoicesAsync(
            string? name, string? searchQuery, int pageNumber, int pageSize)
        {
            // collection to start from
            var collection = _context.Invoices as IQueryable<Invoice>;

            if (!string.IsNullOrWhiteSpace(name))
            {
                name = name.Trim();
                collection = collection.Where(c => c.InvoiceNumber == name);
            }

            if (!string.IsNullOrWhiteSpace(searchQuery))
            {
                searchQuery = searchQuery.Trim();
                collection = collection.Where(a => a.InvoiceNumber.Contains(searchQuery)
                    || (a.Note != null && a.Note.Contains(searchQuery)));
            }

            var totalItemCount = await collection.CountAsync();

            var paginationMetadata = new PaginationMetadata(
                totalItemCount, pageSize, pageNumber);

            var collectionToReturn = await collection.OrderBy(c => c.InvoiceNumber)
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize)
                .ToListAsync();

            return (collectionToReturn, paginationMetadata);
        }

        /// <inheritdoc/>
        public async Task<bool> InvoiceExistsAsync(int cityId)
        {
            return await _context.Invoices.AnyAsync(c => c.Id == cityId);
        }

        /// <inheritdoc/>
        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }
    }
}
