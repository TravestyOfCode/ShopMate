using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Application.ShoppingLists;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopMate.Web.Controllers
{
    public class ShoppingListsController : Controller
    {
        private readonly IMediator _mediator;

        public ShoppingListsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(Index.Query query)
        {
            return View(await _mediator.Send(query));
        }
    }
}
