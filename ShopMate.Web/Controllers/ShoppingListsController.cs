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

        [HttpGet]
        public async Task<IActionResult> CreateAsync(Create.Query query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync(Create.Command command)
        {
            if(!ModelState.IsValid)
            {
                return View(_mediator.Send(new Create.Query()));
            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync(Edit.Query query)
        {
            return View(await _mediator.Send(query));
        }
                
        public IActionResult AddNewItem(int index)
        {
            try
            {
                return PartialView("_AddNewItemPartial");
            }
            catch
            {
                return new StatusCodeResult(500);
            }
        }
    }
}
