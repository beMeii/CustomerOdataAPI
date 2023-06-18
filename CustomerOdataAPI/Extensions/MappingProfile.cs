using AutoMapper;
using CustomerOdataAPI.DTOs;
using CustomerOdataAPI.Models;

namespace CustomerOdataAPI.Extensions
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>();
        }
    }
}
