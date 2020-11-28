namespace Carpfish.Web.Controllers
{
    using System;
    using System.Threading.Tasks;

    using Carpfish.Data.Models;
    using Carpfish.Services.Data;
    using Carpfish.Web.ViewModels.Lakes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    public class LakesController : Controller
    {
        private readonly ICountriesService countriesService;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly ILakesService lakesService;

        public LakesController(
            ICountriesService countriesService,
            UserManager<ApplicationUser> userManager,
            ILakesService lakesService)
        {
            this.countriesService = countriesService;
            this.userManager = userManager;
            this.lakesService = lakesService;
        }

        public IActionResult Add()
        {
            var viewModel = new AddLakeInputModel();
            viewModel.CountriesItems = this.countriesService.GetAllAsKeyValuePairs();
            return this.View(viewModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Add(AddLakeInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CountriesItems = this.countriesService.GetAllAsKeyValuePairs();
                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.lakesService.AddAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            // TODO: Redirect to lake info page
            return this.Redirect("/");
        }
    }
}
