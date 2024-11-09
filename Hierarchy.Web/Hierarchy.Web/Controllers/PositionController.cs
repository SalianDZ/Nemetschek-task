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
                return NotFound("An error occured during the process of connecting to the database!");
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
                return NotFound("There is already a position with the same name!");
            }

			try
			{
				await positionService.AddPositionAsync(model);
				return RedirectToAction("All", "Position");
			}
			catch (Exception)
			{
                return NotFound("An error occured during the process of connecting to the database!");
            }
		}

		public async Task<IActionResult> Details(string id)
		{
			try
			{
				bool isGuidCorrect = Guid.TryParse(id, out var checkedId);

				if (!isGuidCorrect) 
				{
					return NotFound("Please enter a valid Guid!");
                }

				var position = await positionService.GetPositionDetailsAsync(Guid.Parse(id));

                if (position == null)
                {
                    return NotFound("There is no such a position!");
                }

                return View(position);
			}
			catch (Exception)
			{
				return NotFound("An error occured during the process of connecting to the database!");
			}
		}

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (!Guid.TryParse(id, out var correctId))
            {
                return NotFound("Please enter a valid id!");
            }

            var position = await positionService.GetPositionDetailsAsync(correctId);

            if (position == null)
            {
                return NotFound();
            }

            return View(position);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
		{
            if (!Guid.TryParse(id, out var correctId))
            {
                return NotFound("Please enter a valid id!");
            }

			try
			{
				await positionService.DeletePositionAsync(correctId);
				return RedirectToAction("All", "Position");
			}
			catch (Exception ex)
			{
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction("Delete", "Position");
            }
        }
    }
}
