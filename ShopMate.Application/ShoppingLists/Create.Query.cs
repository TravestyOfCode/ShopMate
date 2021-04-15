using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.ShoppingLists
{
    public partial class Create
    {
        public class Query : IRequest<Model>
        {

        }

        public class Model
        {
            public string Title { get; set; }

            public DateTime TripDate { get; set; }

            public string Store { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Model>
        {            
            public QueryHandler()
            {                
            }

            public Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new Model());
            }
        }
    }
}
