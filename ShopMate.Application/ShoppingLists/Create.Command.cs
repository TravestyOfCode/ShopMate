using MediatR;
using Microsoft.Extensions.Logging;
using ShopMate.Data;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ShopMate.Application.ShoppingLists
{
    public partial class Create
    {
        public class Command : IRequest<CommandResult>
        {
            public string Title { get; set; }

            public DateTime TripDate { get; set; }

            public string Store { get; set; }

            public Data.ShoppingListItem[] Items { get; set; }
            
            internal ShoppingList ToShoppingList()
            {
                return new ShoppingList()
                {
                    Title = Title,
                    TripDate = TripDate,
                    Store = Store

                };
            }

            //public class ShoppingListItem
            //{
            //    public int ProductId { get; set; }

            //    public decimal Quantity { get; set; }

            //    public int UnitSizeId { get; set; }
            //}
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
                    var entity = _dbContext.ShoppingLists.Add(request.ToShoppingList());

                    await _dbContext.SaveChangesAsync(cancellationToken);

                    return CommandResult.Ok();

                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unexpected error handling ShoppingLists.Create.Command with request: {request}", request);

                    return CommandResult.ServerError();
                }
            }
        }
    }
}
