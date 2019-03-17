using AutoMapper;
using DataAccess.Models;

namespace Service.DataMapper
{
    public class DataMapperService : IDataMapperService
    {
        public DataMapperService()
        {
            Mapper.Initialize(c =>
            {
                c.CreateMap<Group, Group>()
                    .ForMember(g => g.Id, opt => opt.Ignore())
                    .ForMember(g => g.Default, opt => opt.Ignore());
                c.CreateMap<Contact, Contact>()
                    .ForMember(t => t.Id, opt => opt.Ignore());
            });
        }

        public void Map<T>(T from, T to)
        {
            Mapper.Map(from, to);
        }
    }
}
