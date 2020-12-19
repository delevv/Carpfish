namespace Carpfish.Services.Data.Tests
{
    using System.Collections.Generic;

    using Carpfish.Web.Controllers;
    using Carpfish.Web.ViewModels.Home;
    using Carpfish.Web.ViewModels.Lakes;
    using Carpfish.Web.ViewModels.Rigs;
    using Carpfish.Web.ViewModels.Trophies;

    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class HomeControllerTests
    {
        [Fact]
        public void IndexShouldReturnCorrectViewModel()
        {
            var lakesService = new Mock<ILakesService>();
            lakesService.Setup(l => l.GetAll<LakeInIndexMapViewModel>()).Returns(new List<LakeInIndexMapViewModel>());

            var trophiesService = new Mock<ITrophiesService>();
            trophiesService.Setup(t => t.GetTopFiveBiggestTrophies<TopTrophiesInIndexViewModel>()).Returns(new List<TopTrophiesInIndexViewModel>());

            var rigsService = new Mock<IRigsService>();
            rigsService.Setup(r => r.GetFiveRigsWithMostTrophies<TopRigInIndexViewModel>()).Returns(new List<TopRigInIndexViewModel>());

            var controller = new HomeController(
                lakesService.Object,
                rigsService.Object,
                trophiesService.Object);

            var result = controller.Index();
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsType<IndexViewModel>(viewResult.Model);

            var viewModel = viewResult.Model as IndexViewModel;
            Assert.Empty(viewModel.MapLakes);
            Assert.Empty(viewModel.Trophies);
            Assert.Empty(viewModel.Rigs);
        }
    }
}
