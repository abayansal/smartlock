using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NUnit.Framework;
using SmartLock.Business.Commands;
using SmartLock.Business.Events;
using SmartLock.Business.Handlers;
using SmartLock.Domain;
using SmartLock.Persistence.Contracts;

namespace SmartLock.Test
{
    public class GrantAccessCommandHandlerTests
    {
        private readonly Mock<IMediator> mockMediator;
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly Mock<IGateRepository> mockGateRepository;
        private readonly User user;
        private readonly GrantAccessCommandHandler handler;

        public GrantAccessCommandHandlerTests()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockGateRepository = new Mock<IGateRepository>();
            mockMediator = new Mock<IMediator>();

            handler = new GrantAccessCommandHandler(mockUserRepository.Object, mockGateRepository.Object, mockMediator.Object);

            user = new User("Aykut", "Bayansal", "aykut");
            mockUserRepository.Setup(r => r.Get(It.IsAny<string>())).Returns(user);
            mockGateRepository.Setup(r => r.Get(It.IsAny<string>())).Returns(new Gate("gate-1", "Outer Gate"));
        }

        [Test]
        public async Task IfEverythingExistAccessShouldBeGranted()
        {
            await handler.Handle(new GrantAccessCommand("aykut", "gate-1"), CancellationToken.None);
            Assert.AreEqual(user.GrantedAccessList.Count, 1);
        }

        [Test]
        public async Task ResultShouldBeSaved()
        {
            await handler.Handle(new GrantAccessCommand("aykut", "gate-1"), CancellationToken.None);
            mockUserRepository.Verify(r => r.Save(user), Times.AtLeastOnce);
        }

        [Test]
        public async Task EventResultShouldBePublished()
        {
            await handler.Handle(new GrantAccessCommand("aykut", "gate-1"), CancellationToken.None);
            mockMediator.Verify(r => r.Publish(It.IsAny<AccessGrantedEvent>(), CancellationToken.None), Times.Once);
        }
    }
}