using AutoMapper;
using MVC_Management.Models;
using MVC_Management.ViewModal;

namespace MVC_Management.loansProfile
{
    public class RecordsAutomapper : Profile
    {
        public RecordsAutomapper()
        {
            CreateMap<ClientloansP,ClientsLoansRecords>().ReverseMap();
        }
    }
}
