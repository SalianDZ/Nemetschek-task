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

		public IActionResult Add()
		{
			PositionFormViewModel model = new();
			return View(model);
		}
	}
}
