namespace Etain.WeatherApp.Data
{    
    public interface IEntity<IdT>
    {
        IdT Id { get; set; }
    }
}