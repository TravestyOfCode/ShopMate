﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.Products
{
    public partial class Delete
    {
        public class Command : IRequest<Unit>
        {
            public int Id { get; set; }
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
                        throw new KeyNotFoundException($"The Product with Id: {request.Id} could not be found.");
                    }

                    _dbContext.Products.Remove(entity);

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    return Unit.Value;
                }
                catch(KeyNotFoundException)
                {
                    throw;
                }
                catch (System.Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error handling Products.Delete.Command with request: {request}", request);
                    throw;
                }
            }
        }
    }
}