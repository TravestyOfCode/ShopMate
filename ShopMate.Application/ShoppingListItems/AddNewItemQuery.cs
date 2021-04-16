using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.ShoppingListItems
{
    public partial class AddNewItem
    {
        public class Query : IRequest<Model>
        {
        }

        public class Model
        {
            public int Index { get; set; }

            public int ProductId { get; set; }

            public decimal Quantity { get; set; }

            public int UnitSizeId { get; set; }

            public Dictionary<int, ProductModel> Products { get; set; }

            public Dictionary<int, string> UnitSizes { get; set; }

            public class ProductModel
            {
                public string Name { get; set; }

                public int DefaultUnitId { get; set; }
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

            public async Task<Model> Handle(Query request, CancellationToken cancellationToken)
            {
                try
                {
                    return new Model()
                    {
                        Products = await _dbContext.Products.ToDictionaryAsync(p => p.Id, p => new Model.ProductModel() { Name = p.Name, DefaultUnitId = p.DefaultUnitSizeId }),
                        UnitSizes = await _dbContext.UnitSizes.ToDictionaryAsync(p => p.Id, p => p.Name)
                    };
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error handling ShoppingListItems.AddNewItem.Query with request: {request}", request);
                    throw;
                }


            }
        }
    }
}