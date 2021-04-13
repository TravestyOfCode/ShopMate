﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.UnitSizes
{
    public partial class Delete
    {
        public class Command : IRequest<CommandResult>
        {
            public int Id { get; set; }
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
                    var entity = await _dbContext.UnitSizes.SingleOrDefaultAsync(p => p.Id.Equals(request.Id));

                    if(entity == null)
                    {
                        return CommandResult.NotFound();
                    }

                    _dbContext.UnitSizes.Remove(entity);

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    return CommandResult.Ok();

                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error handling UnitSizes.Delete.Command with request: {request}", request);
                    return CommandResult.ServerError();
                }
            }
        }
    }
}
