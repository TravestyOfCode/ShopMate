using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
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

            [DataType(DataType.Date)]
            public DateTime TripDate { get; set; }

            public string Store { get; set; }

            public List<ShoppingListItem> Items { get; set; }

            public List<Product> Products { get; set; }

            public List<UnitSize> UnitSizes { get; set; }
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
                    return new Model()
                    {
                        TripDate = DateTime.Now,
                        Items = new List<ShoppingListItem>(),
                        Products = await _dbContext.Products.Select(p => new Product()
                        {
                            Id = p.Id,
                            Name = p.Name,
                            DefaultUnitSizeId = p.DefaultUnitSizeId
                        }).ToListAsync(cancellationToken),
                        UnitSizes = await _dbContext.UnitSizes.Select(p => new UnitSize()
                        {
                            Id = p.Id,
                            Name = p.Name
                        }).ToListAsync(cancellationToken)
                    };
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error when handling ShoppingLists.Create.Query with request: {request}", request);
                    throw;
                }

            }
        }
    }
}
