using MediatR;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.Products
{
    public partial class Create
    {
        public class Command : IRequest<CommandResult>
        {
            public string Name { get; set; }

            public int DefaultUnitSizeId { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, CommandResult>
        {
            private readonly ApplicationDbContext _dbContext;

            private readonly ILogger<CommandHandler> _logger;

            public CommandHandler(ApplicationDbContext dbContext, ILogger<CommandHandler> logger)
            {
                _dbContext = dbContext;

                _logger = logger;
            }

            public async Task<CommandResult> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = _dbContext.Add(request.ToProduct());

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    return CommandResult.Ok();
                }
                catch (System.Exception ex)
                {
                    _logger.LogCritical(ex, "Unexpected error handling Product.Create.Command with request: {request}", request);
                    
                    return CommandResult.ServerError();
                }

            }
        }
    }
}
