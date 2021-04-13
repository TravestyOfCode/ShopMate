using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.UnitSizes
{
    public partial class Create
    {
        public class Query : IRequest<Model>
        {

        }

        public class Model
        {
            public string Name { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Model>
        {
            public Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new Model());
            }
        }

    }
}
