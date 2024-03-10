using Asp.Versioning;
using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Entities;
using CityInfo.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Microsoft.AspNetCore.JsonPatch;

namespace CityInfo.API.Controllers
{

    /// <summary>
    /// Represents a controller for managing invoices.
    /// </summary>
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/invoices")]
    [ApiVersion(1)]
    [ApiVersion(2)]
    [Produces("application/json", "application/xml")]
    public class InvoicesController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMapper _mapper;

        const int maxPageSize = 20;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvoicesController"/> class.
        /// </summary>
        /// <param name="invoiceRepository">The invoice repository.</param>
        /// <param name="mapper">The mapper.</param>
        public InvoicesController(IInvoiceRepository invoiceRepository,
                    IMapper mapper)
        {
            _invoiceRepository = invoiceRepository ??
                throw new ArgumentNullException(nameof(invoiceRepository));
            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }


        /// <summary>
        /// Retrieves a list of invoices.
        /// </summary>
        /// <param name="name">The name of the invoice.</param>
        /// <param name="searchQuery">The search query.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>A list of invoices.</returns>
        [HttpGet]
        [SwaggerHeaderAttribute(SwaggerHeaderAttribute.HeaderNameEnum.Pagination)]
        public async Task<ActionResult<IEnumerable<InvoiceDto>>> GetInvoices(
            string? name, string? searchQuery, int pageNumber = 1, int pageSize = 10)
        {
            if (pageSize > maxPageSize)
            {
                pageSize = maxPageSize;
            }

            var (entities, paginationMetadata) = await _invoiceRepository
                .GetInvoicesAsync(name, searchQuery, pageNumber, pageSize);

            Response.Headers.Append("X-Pagination",
                JsonSerializer.Serialize(paginationMetadata));

            return Ok(_mapper.Map<IEnumerable<InvoiceDto>>(entities));
        }


        /// <summary>
        /// Retrieves an invoice by ID.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice.</param>
        /// <returns>The invoice.</returns>
        [HttpGet("{invoiceId}", Name = "GetInvoice")]
        public async Task<ActionResult<InvoiceDto>> GetInvoice(int invoiceId)
        {
            var invoice = await _invoiceRepository.GetInvoiceAsync(invoiceId);

            if (invoice == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<InvoiceDto>(invoice));
        }

        /// <summary>
        /// Creates a new invoice.
        /// </summary>
        /// <param name="invoice">The invoice to create.</param>
        /// <returns>The created invoice.</returns>
        [HttpPost]
        public async Task<ActionResult<InvoiceDto>> CreateInvoice(
            InvoiceForCreationDto invoice)
        {
            var invoiceEntity = _mapper.Map<Invoice>(invoice);
            _invoiceRepository.AddInvoice(invoiceEntity);
            await _invoiceRepository.SaveChangesAsync();

            return CreatedAtRoute("GetInvoice",
                new { invoiceId = invoiceEntity.Id },
                _mapper.Map<InvoiceDto>(invoiceEntity));
        }

        /// <summary>
        /// Updates an invoice.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice.</param>
        /// <param name="invoice">The updated invoice.</param>
        /// <returns>The updated invoice.</returns>
        [HttpPut("{invoiceId}")]
        public async Task<ActionResult> UpdateInvoice(int invoiceId,
            InvoiceForUpdateDto invoice)
        {
            var invoiceEntity = await _invoiceRepository.GetInvoiceAsync(invoiceId);

            if (invoiceEntity == null)
            {
                return NotFound();
            }

            _mapper.Map(invoice, invoiceEntity);

            await _invoiceRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Partially updates an invoice.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice.</param>
        /// <param name="patchDocument">The JSON patch document containing the updates.</param>
        /// <returns>No content.</returns>
        [HttpPatch("{invoiceId}")]
        public async Task<ActionResult> PartiallyUpdateInvoice(int invoiceId,
            JsonPatchDocument<InvoiceForUpdateDto> patchDocument)
        {
            var invoiceEntity = await _invoiceRepository.GetInvoiceAsync(invoiceId);

            if (invoiceEntity == null)
            {
                return NotFound();
            }

            var invoiceToPatch = _mapper.Map<InvoiceForUpdateDto>(invoiceEntity);
            patchDocument.ApplyTo(invoiceToPatch, ModelState);

            if (!TryValidateModel(invoiceToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(invoiceToPatch, invoiceEntity);
            await _invoiceRepository.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Deletes an invoice.
        /// </summary>
        /// <param name="invoiceId">The ID of the invoice to delete.</param>
        /// <returns>No content.</returns>
        [HttpDelete("{invoiceId}")]
        public async Task<ActionResult> DeleteInvoice(int invoiceId)
        {
            var invoiceEntity = await _invoiceRepository.GetInvoiceAsync(invoiceId);

            if (invoiceEntity == null)
            {
                return NotFound();
            }

            _invoiceRepository.DeleteInvoice(invoiceEntity);
            await _invoiceRepository.SaveChangesAsync();

            return NoContent();
        }

    }
}