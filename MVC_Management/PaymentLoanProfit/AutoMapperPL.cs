using AutoMapper;
using MVC_Management.Models;
using MVC_Management.ViewModal;

namespace MVC_Management.PaymentLoanProfit
{
    public class AutoMapperPL: Profile
    {


        public AutoMapperPL()
        {

            CreateMap<Payment,  PaymentLoans>().ReverseMap();
           

        }


    }
}
