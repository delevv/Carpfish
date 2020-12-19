namespace Carpfish.Services.Data.Tests
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using Microsoft.AspNetCore.Http;
    using Moq;
    using Xunit;

    public class CloudinaryServiceTests
    {
        // TODO: Finish test
        public async Task UploadAsyncSholdWorkCorrect()
        {
            var func = new Func<ImageUploadResult>(() => new ImageUploadResult()
            {
                Url = new Uri("www.fakeUrl.com"),
            });

            var uploadResult = new Task<ImageUploadResult>(func);

            var clodinaryMock = new Mock<Cloudinary>();

            clodinaryMock.Setup(c => c.UploadAsync(It.IsAny<ImageUploadParams>(), null))
                .Returns(uploadResult);

            var service = new CloudinaryService(clodinaryMock.Object);

            // Arrange
            var fileMock = new Mock<IFormFile>();

            // Setup mock file using a memory stream
            var content = "1010101";
            var fileName = "test.jpg";
            var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            var file = fileMock.Object;

            var result = await service.UploadAsync(file, fileName);

            Assert.Equal("www.fakeUrl.com", result);
        }
    }
}
