using AutoFixture;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using VooltChallenge.Controllers;
using VooltModels;
using VooltServices.Interfaces;


namespace VooltTests.Controlles
{
    public class VooltTests
    {
        /*
         * ILogger<Voolt> _logger { get; }
        IReadWriteFile iReadWriteFile { get; }
        */
        private Mock<ILogger<VooltController>> MockLog { get; set; }
        private Mock<IReadWriteFile> MockIReadWriteFile { get; set; }
        private Fixture Fixture { get; set; }

        [SetUp]
        public void Setup()
        {
            MockLog = new Mock<ILogger<VooltController>>();
            MockIReadWriteFile = new Mock<IReadWriteFile>();
            Fixture = new Fixture();
        }

        [Test]
        public async Task GetTest()
        {
            //Construindo os fakes
            var key = Fixture.Create<string>();

            //dados de retorno
            var Payload = Fixture.Build<ContentsData>()
                                    .Create();

            MockIReadWriteFile.Setup(x => x.ReadAsync<ContentsData>(It.IsAny<string>())).
                Returns(Task.FromResult(new Tuple<bool, ContentsData>(true, Payload)));

            var controller = new VooltController(MockIReadWriteFile.Object, MockLog.Object);

            var result = await controller.Get(key);
            var contentesult = result as OkObjectResult;

            contentesult?.Should().NotBeNull();
            contentesult?.Value.Should().BeEquivalentTo(Payload);
        }

        [Test]
        public async Task PostTest()
        {
            //Construindo os fakes
            var key = Fixture.Create<string>();

            MockIReadWriteFile.Setup(x => x.CreateAsync<ContentsData>(It.IsAny<string>(), It.IsAny<ContentsData>())).
                Returns(Task.FromResult(true));

            var controller = new VooltController(MockIReadWriteFile.Object, MockLog.Object);

            var result = await controller.Post(key);
            var contentesult = result as OkObjectResult;

            contentesult?.Should().NotBeNull();
        }

        [Test]
        public async Task PutTest()
        {
            //Construindo os fakes
            var key = Fixture.Create<string>();

            //dados de estrada
            var Payload = Fixture.Build<ContentsData>()
                                    .Create();

            MockIReadWriteFile.Setup(x => x.UpdateAsync<ContentsData>(It.IsAny<string>(), It.IsAny<ContentsData>())).
                Returns(Task.FromResult(true));

            var controller = new VooltController(MockIReadWriteFile.Object, MockLog.Object);

            var result = await controller.Put(key, Payload);
            var contentesult = result as CreatedResult;

            contentesult?.Should().NotBeNull();
        }

        [Test]
        public async Task DeleteTest()
        {
            //Construindo os fakes
            var key = Fixture.Create<string>();

            MockIReadWriteFile.Setup(x => x.DeleteAsync<ContentsData>(It.IsAny<string>())).
                Returns(Task.FromResult(true));

            var controller = new VooltController(MockIReadWriteFile.Object, MockLog.Object);

            var result = await controller.Delete(key);
            var contentesult = result as OkObjectResult;

            contentesult?.Should().NotBeNull();
        }
    }
}
