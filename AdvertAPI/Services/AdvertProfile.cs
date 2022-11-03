using AdvertApi.Models;
using AutoMapper;
namespace AdvertAPI.Services
{
    public class AdvertProfile : Profile 
    
    { 
        //in the contrusctor add mapping
        public AdvertProfile()
        {
            //its going to map based on attribute names as long as attirbutes names are same
            CreateMap<AdvertModel, AdvertDbModel>();

        }

    }
}
