using AutoMapper;
using MVC_Management.Models;
using MVC_Management.ViewModal;

namespace MVC_Management.loansProfile
{
    public class AutoMapperLoans: Profile
    {


        public AutoMapperLoans()
        {

          
            CreateMap<ClientloansP, LoansModel>().ReverseMap();
        }


    }
}
