using MediatR;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.Products
{
    public partial class Create
    {
        public class Command : IRequest<int>
        {
            public string Name { get; set; }

            public int DefaultUnitSizeId { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command,int>
        {
            private readonly ApplicationDbContext _dbContext;

            private readonly ILogger<CommandHandler> _logger;

            public CommandHandler(ApplicationDbContext dbContext, ILogger<CommandHandler> logger)
            {
                _dbContext = dbContext;

                _logger = logger;
            }

            public async Task<int> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = _dbContext.Add(request.ToProduct());

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    return entity.Entity.Id;
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error handling Product.Create.Command with request: {request}", request);
                    throw;
                }

            }
        }
    }
}
