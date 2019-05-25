using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Controllers.Api
{
	public class MessageController: BaseApiController
	{
		[HttpGet]
		public async Task<IActionResult> Get(int? brandFilterApplied, int? typesFilterApplied, int? page)
		{
			//var itemsPage = 10;
			//var catalogModel = await _catalogViewModelService.GetCatalogItems(page ?? 0, itemsPage, brandFilterApplied, typesFilterApplied);
			return Ok(null);
		}
	}
}
