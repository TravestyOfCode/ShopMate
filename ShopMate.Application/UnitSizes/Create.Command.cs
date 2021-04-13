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
        public class Command : IRequest<string>
        {
            public string Name { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, string>
        {
            private readonly ApplicationDbContext _dbContext;

            private readonly ILogger<CommandHandler> _logger;

            public CommandHandler(ApplicationDbContext dbContext, ILogger<CommandHandler> logger)
            {
                _dbContext = dbContext;

                _logger = logger;
            }

            public async Task<string> Handle(Command request, CancellationToken cancellationToken)
            {
                try
                {
                    var entity = _dbContext.UnitSizes.Add(request.ToUnitSize());

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    return entity.Entity.Id;
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
