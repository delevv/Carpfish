namespace Carpfish.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Carpfish.Services.Data;
    using Carpfish.Web.ViewModels.Rigs;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    public class RigsController : Controller
    {
        private readonly IRigsService rigsService;

        public RigsController(IRigsService rigsService)
        {
            this.rigsService = rigsService;
        }

        [Authorize]
        public IActionResult Add()
        {
            var viewModel = new AddRigInputModel();
            return this.View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddRigInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            try
            {
                await this.rigsService.AddAsync(input, userId);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            // TODO: Redirect to rig byid page
            return this.Redirect("/");
        }
    }
}
