using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShopMate.Application.UnitSizes;
using System.Threading.Tasks;

namespace ShopMate.Web.Controllers
{
    public class UnitSizesController : Controller
    {
        private readonly IMediator _mediator;

        public UnitSizesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync(Index.Query query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreateAsync(Create.Query query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync(Create.Command command)
        {
            if (!ModelState.IsValid)
            {
                return View(_mediator.Send(new Create.Query()));
            }

            await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditAsync(Edit.Query query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditAsync(Edit.Command command)
        {
            if(!ModelState.IsValid)
            {
                return View(await _mediator.Send(new Edit.Query()));
            }

            var result = await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(Delete.Query query)
        {
            return View(await _mediator.Send(query));
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(Delete.Command command)
        {
            if(!ModelState.IsValid)
            {
                return View(await _mediator.Send(new Delete.Query()));
            }

            var result = await _mediator.Send(command);

            return RedirectToAction(nameof(Index));
        }
    }
}
