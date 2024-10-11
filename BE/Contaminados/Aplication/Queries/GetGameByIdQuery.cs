namespace Contaminados.Aplication.Queries
{
    public class GetGameByIdQuery
    {
        public Guid Id { get; }
        public GetGameByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}