using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Application.Products;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopMate.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IMediator _mediator;

        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync(Index.Query query)
        {
            return View(await _mediator.Send(query));
        }
        

        [HttpGet]
        public async Task<ActionResult> CreateAsync(Create.Query query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Create.Command command)
        {
            if(!ModelState.IsValid)
            {
                return View(_mediator.Send(new Create.Query()));
            }
            
            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> EditAsync(Edit.Query query)
        {            
            var result = await _mediator.Send(query);
            if(result == null)
            {                
                return NotFound(query.Id);
            }
            return View(result);
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(Edit.Command command)
        {            
            await _mediator.Send(command);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<ActionResult> DeleteAsync(Delete.Query query)
        {
            var result = await _mediator.Send(query);

            if(result == null)
            {
                return NotFound(query.Id);
            }
            
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteAsync(Delete.Command command)
        {
            if(!ModelState.IsValid)
            {
                return View(await _mediator.Send(new Delete.Query()));
            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
    }
}
