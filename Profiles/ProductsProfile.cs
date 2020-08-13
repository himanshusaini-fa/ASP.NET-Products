using System;
using AutoMapper;
using Products.Dtos;
using Products.Models;

namespace Products.Profiles
{
    public class ProductsProfile : Profile
    {
        public ProductsProfile()
        {
            CreateMap<Product, ProductReadDto>();
            CreateMap<ProductAddDto, Product>();
            CreateMap<ProductUpdateDto, Product>().ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
            {
                if (srcMember != null)
                {
                    if (typeof(int) == srcMember.GetType())
                    {
                        return Convert.ToInt32(srcMember) >= 1;
                    }
                    if (typeof(double) == srcMember.GetType())
                    {
                        return Convert.ToDouble(srcMember) >= 1;
                    }
                    return true;
                }
                else return false;
            })); ;
        }
    }
}