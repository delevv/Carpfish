namespace Carpfish.Services.Data.Tests
{
    using System.Collections.Generic;

    using Carpfish.Web.Controllers;
    using Carpfish.Web.ViewModels.Trophies;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class TrophyControllerTests
    {
        [Fact]
        public void AllShouldReturnNotFoundWithIdLessOrEqualToOne()
        {
            var lakesService = new Mock<ILakesService>();
            var rigsService = new Mock<IRigsService>();
            var trophiesService = new Mock<ITrophiesService>();

            var controller = new TrophiesController(
                 lakesService.Object,
                 trophiesService.Object,
                 rigsService.Object);

            var result = controller.All(-1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AllShouldReturnCorrectViewModel()
        {
            var lakesService = new Mock<ILakesService>();
            var rigsService = new Mock<IRigsService>();

            var trophiesService = new Mock<ITrophiesService>();
            trophiesService.Setup(t => t.GetAll<TrophyInListViewModel>(1, 6))
                .Returns(new List<TrophyInListViewModel>());
            trophiesService.Setup(t => t.GetCount()).Returns(0);

            var controller = new TrophiesController(
                lakesService.Object,
                trophiesService.Object,
                rigsService.Object);

            var result = controller.All();
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsType<TrophiesListViewModel>(viewResult.Model);

            var viewModel = viewResult.Model as TrophiesListViewModel;
            Assert.Empty(viewModel.Trophies);
        }

        [Fact]
        public void AddShouldReturnCorrectViewModel()
        {
            var lakesService = new Mock<ILakesService>();
            lakesService.Setup(l => l.GetAllAsKeyValuePairs())
                .Returns(new List<KeyValuePair<string, string>>());

            var rigsService = new Mock<IRigsService>();
            rigsService.Setup(r => r.GetAllAsKeyValuePairs())
                .Returns(new List<KeyValuePair<string, string>>());

            var trophiesService = new Mock<ITrophiesService>();

            var controller = new TrophiesController(
                 lakesService.Object,
                 trophiesService.Object,
                 rigsService.Object);

            var result = controller.Add();
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsType<AddTrophyInputModel>(viewResult.Model);
        }
    }
}
