namespace Contaminados.Application.Queries
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