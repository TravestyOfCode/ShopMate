using MediatR;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.UnitSizes
{
    public partial class Create
    {
        public class Command : IRequest<CommandResult>
        {
            public string Name { get; set; }

            internal UnitSize ToUnitSize()
            {
                return new UnitSize()
                {
                    Name = Name
                };
            }
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
                    var entity = _dbContext.UnitSizes.Add(request.ToUnitSize());

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    return CommandResult.Ok();
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error adding UnitSize with request: {request}", request);
                    throw;
                }
            }
        }
    }
}
