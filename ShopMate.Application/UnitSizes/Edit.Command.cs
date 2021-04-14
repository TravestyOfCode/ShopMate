using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.UnitSizes
{
    public partial class Edit
    {
        public class Command : IRequest<CommandResult>
        {
            public string Id { get; set; }

            public string Name { get; set; }

            internal void Map(UnitSize unitSize)
            {
                if (unitSize == null)
                {
                    return;
                }

                unitSize.Name = Name;
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
                    var entity = await _dbContext.UnitSizes.SingleOrDefaultAsync(p => p.Id.Equals(request.Id), cancellationToken);
                    
                    if(entity == null)
                    {
                        return CommandResult.NotFound();
                    }

                    request.Map(entity);

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    return CommandResult.Ok();
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error handling UnitSizes.Edit.Command with request: {request}", request);
                    throw;
                }
            }
        }
    }
}
