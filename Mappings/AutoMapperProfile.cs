using AutoMapper;
using PmfBackend.Commands;
using PmfBackend.Database.Entities;

namespace PmfBackend.Mappings {
    public class AutoMapperProfile : Profile {

        public AutoMapperProfile() {
            CreateMap<CreateTransactionCommand,TransactionEntity>();

        }

    }
}