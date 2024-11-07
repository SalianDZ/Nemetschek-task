using Hierarchy.Services.Data;
using Hierarchy.Services.Data.Interfaces;
using Hierarchy.Web.Models.Position;
using Microsoft.AspNetCore.Mvc;

namespace Hierarchy.Web.Controllers
{
	public class PositionController : Controller
	{
		private readonly IPositionService positionService;
        public PositionController(IPositionService positionService)
        {
            this.positionService = positionService;
        }
        public async Task<IActionResult> All()
		{
			var positions = await positionService.GetAllPositionsAsync();
			return View(positions);
		}

		[HttpGet]
		public IActionResult Add()
		{
			try
			{
				PositionFormViewModel model = new();
				return View(model);
			}
			catch (Exception)
			{

				return View("Error");
			}
		}

		[HttpPost]
		public async Task<IActionResult> Add(PositionFormViewModel model)
		{
			if (!ModelState.IsValid)
			{
				return View(model);
			}


			if (await positionService.DoesPositionExistAsync(model.Name))
			{
				//There should be a better way for error handling!
                return RedirectToAction("All", "Position");
            }

			try
			{
				await positionService.AddPositionAsync(model);
				return RedirectToAction("All", "Position");
			}
			catch (Exception)
			{
				return View("Error");
			}
		}

		public async Task<IActionResult> Details(string id)
		{
			try
			{
				var position = await positionService.GetPositionDetailsAsync(Guid.Parse(id));
				return View(position);
			}
			catch (Exception)
			{
				return View("Error");
			}
		}
	}
}
