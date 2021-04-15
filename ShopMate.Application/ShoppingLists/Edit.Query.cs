using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.ShoppingLists
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

            public string Title { get; set; }

            public DateTime TripDate { get; set; }

            public string Store { get; set; }

            //public List<ShoppingListItem> Items { get; set; }
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
                    return await _dbContext.ShoppingLists                        
                        .Select(p => new Model()
                        {
                            Id = p.Id,
                            Title = p.Title,
                            TripDate = p.TripDate,
                            Store = p.Store
                        }).SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);
                        
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error handling ShoppingLists.Edit.Query with request: {request}", request);
                    throw;
                }
            }
        }
    }
}
