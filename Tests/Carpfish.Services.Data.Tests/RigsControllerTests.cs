namespace Carpfish.Services.Data.Tests
{
    using System.Collections.Generic;

    using Carpfish.Web.Controllers;
    using Carpfish.Web.ViewModels.Rigs;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class RigsControllerTests
    {
        [Fact]
        public void AllShouldReturnNotFoundWithIdLessOrEqualToOne()
        {
            var rigsService = new Mock<IRigsService>();

            var controller = new RigsController(rigsService.Object);

            Assert.IsType<NotFoundResult>(controller.All(0));

            Assert.IsType<NotFoundResult>(controller.All(-1));
        }

        [Fact]
        public void AllShouldReturnCorrectViewModel()
        {
            var rigsService = new Mock<IRigsService>();
            rigsService
                .Setup(t => t.GetAll<AddRigInputModel>(1, 6))
                .Returns(new List<AddRigInputModel>());

            var controller = new RigsController(rigsService.Object);

            var result = controller.All();
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsType<RigsListViewModel>(viewResult.Model);

            var viewModel = viewResult.Model as RigsListViewModel;
            Assert.Empty(viewModel.Rigs);
        }

        [Fact]
        public void AddShouldReturnCorrectViewModel()
        {
            var rigsService = new Mock<IRigsService>();

            var controller = new RigsController(rigsService.Object);

            var result = controller.Add();
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsType<AddRigInputModel>(viewResult.Model);
        }
    }
}
