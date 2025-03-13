using AutoMapper;
using MVC_Management.Models;
using MVC_Management.ViewModal;

namespace MVC_Management.Profiles
{
    public class AutoMapperProfiles:Profile
    {


        public AutoMapperProfiles()
        {

            CreateMap<Registration, ClientsViewModal>().ReverseMap();

           
        }



    }
}
