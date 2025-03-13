using AutoMapper;
using MVC_Management.Models;
using MVC_Management.ViewModal;

namespace MVC_Management.StatementView
{
    public class statementAutomapper : Profile
    {

        public statementAutomapper() 
        {
            CreateMap<Statement, statementAutomapper>().ReverseMap();
        }
    }
}
