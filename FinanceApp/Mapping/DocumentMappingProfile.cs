using AutoMapper;
using FinanceApp.Models;
using System.Reflection.Metadata;

namespace FinanceApp.Mapping
{
    public class DocumentMappingProfile:Profile
    {
        public DocumentMappingProfile()
        {
            CreateMap<Document, DocumentDTO>();
            CreateMap<Document, DocPosDto>();    
        }
    }
}
