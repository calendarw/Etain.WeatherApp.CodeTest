using System.Collections.Generic;

namespace Etain.WeatherApp.Data
{    
    public interface IReadOnlyRepository<T, IdT> where T : class, IEntity<IdT>
    {
        List<T> GetAll();
    }
}