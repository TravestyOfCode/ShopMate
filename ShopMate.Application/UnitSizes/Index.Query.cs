using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.UnitSizes
{
    public partial class Index
    {
        public class Query : IRequest<IEnumerable<Model>>
        {

        }

        public class Model
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly ApplicationDbContext _dbContext;

            private readonly ILogger<QueryHandler> _logger;

            public QueryHandler(ApplicationDbContext dbContext, ILogger<QueryHandler> logger)
            {
                _dbContext = dbContext;

                _logger = logger;
            }

            public async Task<IEnumerable<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    return await _dbContext.UnitSizes.Select(p => new Model()
                    {
                        Id = p.Id,
                        Name = p.Name
                    }).ToListAsync(cancellationToken);
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error handling UnitSizes.Index.Query with request: {request}", request);
                    throw;
                }
            }
        }
    }
}
