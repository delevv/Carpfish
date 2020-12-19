namespace Carpfish.Services.Data.Tests
{
    using System.Collections.Generic;

    using Carpfish.Web.Controllers;
    using Carpfish.Web.ViewModels.Lakes;
    using Microsoft.AspNetCore.Mvc;
    using Moq;
    using Xunit;

    public class LakesControllerTests
    {
        [Fact]
        public void AddShouldReturnCorrectViewModel()
        {
            var lakesService = new Mock<ILakesService>();

            var countriesService = new Mock<ICountriesService>();
            countriesService.Setup(c => c.GetAllAsKeyValuePairs())
                .Returns(new List<KeyValuePair<string, string>>());

            var controller = new LakesController(
                 countriesService.Object,
                 lakesService.Object);

            var result = controller.Add();
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsType<AddLakeInputModel>(viewResult.Model);
        }

        [Fact]
        public void AllShouldReturnNotFoundWithIdLessOrEqualToOne()
        {
            var lakesService = new Mock<ILakesService>();
            var countriesService = new Mock<ICountriesService>();

            var controller = new LakesController(
                 countriesService.Object,
                 lakesService.Object);

            var result = controller.All(null, -1);
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void AllShouldReturnLakeInListViewModelWithAllType()
        {
            var lakesService = new Mock<ILakesService>();
            lakesService
                .Setup(l => l.GetAll<LakeInListViewModel>(1, 6))
                .Returns(new List<LakeInListViewModel>());

            var countriesService = new Mock<ICountriesService>();

            var controller = new LakesController(
                 countriesService.Object,
                 lakesService.Object);

            var result = controller.All("All", 1);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsType<LakesListViewModel>(viewResult.Model);
        }

        [Fact]
        public void AllShouldReturnLakeInListViewModelWithFreeType()
        {
            var lakesService = new Mock<ILakesService>();
            lakesService
                .Setup(l => l.GetAll<LakeInListViewModel>("Free", 1, 6))
                .Returns(new List<LakeInListViewModel>());

            var countriesService = new Mock<ICountriesService>();

            var controller = new LakesController(
                 countriesService.Object,
                 lakesService.Object);

            var result = controller.All("Free", 1);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsType<LakesListViewModel>(viewResult.Model);
        }

        [Fact]
        public void AllShouldReturnLakeInListViewModelWithPaidType()
        {
            var lakesService = new Mock<ILakesService>();
            lakesService
                .Setup(l => l.GetAll<LakeInListViewModel>("Paid", 1, 6))
                .Returns(new List<LakeInListViewModel>());

            var countriesService = new Mock<ICountriesService>();

            var controller = new LakesController(
                 countriesService.Object,
                 lakesService.Object);

            var result = controller.All("Paid", 1);
            Assert.IsType<ViewResult>(result);

            var viewResult = result as ViewResult;
            Assert.IsType<LakesListViewModel>(viewResult.Model);
        }
    }
}
