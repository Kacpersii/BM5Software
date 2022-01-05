using AutoMapper;
using BM5Software.DTOs;
using BM5Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BM5Software
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Doctor, DoctorDto>()
                .ForMember(d => d.SpecializationName, d => d.MapFrom(s => s.Specialization.Name));

            CreateMap<WorkplaceDto, Workplace>()
                .ForMember(w => w.Address, d => d.MapFrom(dto => 
                    new Address() 
                    { 
                        Id = dto.AddressId,
                        City = dto.City, 
                        Street = dto.Street, 
                        BuildingNumber = dto.BuildingNumber,
                        ApartmentNumber = dto.ApartmentNumber, 
                        PostalCode = dto.PostalCode,
                        Province = dto.Province 
                    })
                );

        }
    }
}
