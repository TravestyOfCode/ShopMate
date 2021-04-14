using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.Products
{
    public partial class Edit
    {
        public class Command : IRequest
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public int DefaultUnitSizeId { get; set; }

            internal void Map(Product product)
            {
                if(product == null)
                {
                    return;
                }

                product.Name = Name;
                product.DefaultUnitSizeId = DefaultUnitSizeId;
            }
        }

        public class CommandHandler : IRequestHandler<Command, Unit>
        {
            private readonly ApplicationDbContext _dbContext;

            private readonly ILogger<CommandHandler> _logger;

            public CommandHandler(ApplicationDbContext dbContext, ILogger<CommandHandler> logger)
            {
                _dbContext = dbContext;

                _logger = logger;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = await _dbContext.Products.SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);

                    if(entity == null)
                    {
                        return Unit.Value;
                    }

                    request.Map(entity);

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    return Unit.Value;

                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error handling Products.Edit.Command with request: {request}", request);
                    throw;
                }
            }
        }
    }
}
