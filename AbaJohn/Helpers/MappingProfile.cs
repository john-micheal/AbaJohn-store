using AbaJohn.Models;
using AbaJohn.ViewModel;
using AutoMapper;

namespace AbaJohn.Helpers
{
    public class MappingProfile: Profile 
    {
        public MappingProfile() {
            CreateMap<Product, productViewModel>()
                .ForMember(dest => dest.category_id,
                src => src.MapFrom(src => src.category.Id))
                .ForMember(dest=>dest.BaseImg , 
                src=>src.MapFrom(src=>src.images.BaseImg))
                .ForMember(dest => dest.Img1,
                src => src.MapFrom(src => src.images.Img1))
                .ForMember(dest => dest.Img2,
                src => src.MapFrom(src => src.images.Img2))
                .ForMember(dest => dest.Img3,
                src => src.MapFrom(src => src.images.Img3));

                 CreateMap<ItemViewModel, Item>().ForMember(s=>s.ID ,D=>D.Ignore());

            CreateMap<Item,Item>().ForMember(d => d.ID,  D => D.Ignore());
        } 

     
    }
}