using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace WebApi.Utilities.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BookDtoForUpdate, Book>().ReverseMap();  // ReverseMap two ways mapping
            CreateMap<Book, BookDto>(); //(source,destination)
            CreateMap<BookDtoForInsertion, Book>();
           
        }
    }
}
