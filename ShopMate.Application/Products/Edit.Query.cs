using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.Products
{
    public partial class Edit
    {
        public class Query : IRequest<Model>
        {
            public int Id { get; set; }
        }

        public class Model
        {
            public int Id { get; set; }

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
                    var result = await _dbContext.Products
                        .Select(p => new Model()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            DefaultUnitSizeId = p.DefaultUnitSizeId
                        })
                        .SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

                    if(request == null)
                    {
                        return null;
                    }

                    result.UnitSizes = await _dbContext.UnitSizes.ToDictionaryAsync(p => p.Id, p => p.Name, cancellationToken);

                    return result;

                }
                catch (System.Exception ex)
                {
                    _logger.LogCritical(ex, "Unexpected error handling Product.Edit.Query with request: {request}", request);
                    throw;
                }
            }
        }
    }
}
