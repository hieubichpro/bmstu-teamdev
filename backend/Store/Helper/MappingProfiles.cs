using AutoMapper;
using Dto;
using Store.Models;

namespace Store.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<ItemCart, ItemCartDto>();
            CreateMap<ItemOrder, ItemOrderDto>();
            CreateMap<Cart, CartDto>();
            CreateMap<Order, OrderDto>();
            CreateMap<User, UserDto>();
        }
    }
}
