namespace Contaminados.Aplication.Queries
{
    public class GetPlayerByIdQuery
    {
        public Guid Id { get; }
        public GetPlayerByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}