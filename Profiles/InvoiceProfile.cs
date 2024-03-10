using AutoMapper;

namespace CityInfo.API.Profiles
{
    /// <summary>
    /// Represents the profile for mapping entities to InvoiceDto models.
    /// </summary>
    public class CityProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CityProfile"/> class.
        /// </summary>
        public CityProfile()
        {
            CreateMap<Entities.Invoice, Models.InvoiceDto>();
            CreateMap<Models.InvoiceForCreationDto, Entities.Invoice>();
            CreateMap<Models.InvoiceForUpdateDto, Entities.Invoice>();
            CreateMap<Entities.Invoice, Models.InvoiceForUpdateDto>();
        }
    }
}
