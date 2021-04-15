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
    public partial class Index
    {
        public class Query : IRequest<IEnumerable<Model>>
        {

        }

        public class Model
        {
            public int Id { get; set; }

            public string Title { get; set; }

            public DateTime TripDate { get; set; }

            public string Store { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, IEnumerable<Model>>
        {
            private readonly ApplicationDbContext _dbContext;

            private readonly ILogger<QueryHandler> _logger;

            private readonly ICurrentUser _user;

            public QueryHandler(ApplicationDbContext dbContext, ILogger<QueryHandler> logger, ICurrentUser user)
            {
                _dbContext = dbContext;

                _logger = logger;

                _user = user;
            }

            public async Task<IEnumerable<Model>> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    return await _dbContext.ShoppingLists
                        .Where(p => p.UserId.Equals(_user.UserId))
                        .Select(p => new Model()
                        {
                            Id = p.Id,
                            Title = p.Title,
                            TripDate = p.TripDate,
                            Store = p.Store
                        }).ToListAsync(cancellationToken);                        
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error handling ShoppingLists.Index.Query with request: {request}", request);
                    throw;
                }
            }
        }
    }
}
