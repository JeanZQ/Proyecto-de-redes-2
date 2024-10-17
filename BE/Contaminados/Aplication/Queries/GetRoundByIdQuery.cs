namespace Contaminados.Aplication.Queries
{
    public class GetRoundByIdQuery
    {
        public Guid Id { get; }
        public GetRoundByIdQuery(Guid id)
        {
            Id = id;
        }
    }
}