using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System;
using System.Collections.Generic;
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

            public List<ShoppingListItem> Items { get; set; }

            public Dictionary<int, string> Products { get; set; }

            public Dictionary<int, string> UnitSizes { get; set; }

            public class ShoppingListItem
            {
                public int ProductId { get; set; }

                public int ProductDefaultUnitSizeId { get; set; }

                public decimal Quantity { get; set; }

                public int UnitSizeId { get; set; }
            }

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

            public QueryHandler()
            {                
            }

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    Model result = new Model();

                    result.Items = new List<Model.ShoppingListItem>();

                    result.Products = await _dbContext.Products
                        .ToDictionaryAsync(p => p.Id, p => p.Name, cancellationToken);

                    result.UnitSizes = await _dbContext.UnitSizes
                        .ToDictionaryAsync(p => p.Id, p => p.Name, cancellationToken);

                    return result;
                }
                catch(Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error when handling ShoppingLists.Create.Query with request: {request}", request);
                    throw;
                }

            }
        }
    }
}
