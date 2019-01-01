using System;

namespace Service.DataMapper
{
    public interface IDataMapperService
    {
        void Map<T>(T from, T to);
    }
}
