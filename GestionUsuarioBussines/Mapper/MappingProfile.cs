using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Domain.DTO;
using Domain.Models;

namespace Domain.Mapper
{
    public class MappingProfile : Profile
    {

        public MappingProfile()
        {
            // Mapeo de DTO a Entidad
            CreateMap<UsuarioDTO, Usuario>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.Mail))
                .ForMember(dest => dest.Domicilio, opt =>
                {
                    opt.MapFrom(src => src.Domicilio);
                    opt.Condition((src, dest, destMember) => destMember != null); // Verifica que el Domicilio no sea nulo
                });

            CreateMap<DomicilioDTO, Domicilio>()
                .ForMember(dest => dest.Id, opt => opt.Ignore()); // Ignorar el Id al mapear Domicilio

            // Mapeo de Entidad a DTO
            CreateMap<Usuario, UsuarioDTO>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.Mail, opt => opt.MapFrom(src => src.Mail))
                .ForMember(dest => dest.Domicilio, opt => opt.MapFrom(src => src.Domicilio));

            CreateMap<Domicilio, DomicilioDTO>();
        }
    }
}
