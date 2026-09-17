namespace CatchLightning.Core.Abstractions
{
    /// <summary>
    /// Every model has a name property.
    /// </summary>
    public interface IEntity
    {
        public int Id { get; init; }
        public string Name { get; set; }
    }
}
