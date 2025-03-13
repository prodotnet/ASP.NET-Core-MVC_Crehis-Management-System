using AutoMapper;
using MVC_Management.Models;
using MVC_Management.ViewModal;

namespace MVC_Management.cllientprofile
{
    public class AutoMapperPerLinePayment : Profile
    {


        public AutoMapperPerLinePayment()
        {
            CreateMap<ClientloansP, PerLinePaymentViewModel>().ReverseMap();
        }


    }
}
