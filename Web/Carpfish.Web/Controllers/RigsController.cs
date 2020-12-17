namespace Carpfish.Web.Controllers
{
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    using Carpfish.Common;
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

        public IActionResult All(int id = 1)
        {
            if (id <= 0)
            {
                return this.NotFound();
            }

            var viewModel = new RigsListViewModel()
            {
                ItemsPerPage = GlobalConstants.RigsCountPerPage,
                ItemsCount = this.rigsService.GetCount(),
                PageNumber = id,
                Rigs = this.rigsService.GetAll<RigInListViewModel>(id, GlobalConstants.RigsCountPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult ById(int id)
        {
            var viewModel = this.rigsService.GetById<RigByIdViewModel>(id);
            return this.View(viewModel);
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            var inputModel = this.rigsService.GetById<EditRigInputModel>(id);

            return this.View(inputModel);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EditRigInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.rigsService.UpdateAsync(id, input);

            return this.RedirectToAction(nameof(this.ById), new { id });
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            await this.rigsService.DeleteAsync(id);

            return this.RedirectToAction(nameof(this.All));
        }
    }
}
