using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.Products
{
    public partial class Create
    {
        public class Query : IRequest<Model>
        {

        }

        public class Model
        {
            public string Name { get; set; }

            public int DefaultUnitSizeId { get; set; }

            public Dictionary<int, string> UnitSizes { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, Model>
        {
            private readonly ApplicationDbContext _dbContext;

            private readonly ILogger<QueryHandler> _logger;

            public QueryHandler(ApplicationDbContext dbContext, ILogger<QueryHandler> logger)
            {
                _dbContext = dbContext;

                _logger = logger;
            }

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    return new Model()
                    {
                        UnitSizes = await _dbContext.UnitSizes.ToDictionaryAsync(p => p.Id, p => p.Name)
                    };
                }
                catch(System.Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error handling Product.Create.Query with request: {request}", request);
                    throw;
                }
            }
        }
    }
}
