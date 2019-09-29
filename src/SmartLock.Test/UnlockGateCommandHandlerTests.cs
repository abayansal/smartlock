using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Moq;
using NUnit.Framework;
using SmartLock.Business.Commands;
using SmartLock.Business.Exceptions;
using SmartLock.Business.Handlers;
using SmartLock.Domain;
using SmartLock.Persistence.Contracts;

namespace SmartLock.Test
{
    public class UnlockGateCommandHandlerTests
    {
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly UnlockGateCommandHandler handler;

        public UnlockGateCommandHandlerTests()
        {
            mockUserRepository = new Mock<IUserRepository>();
            var mockMediator = new Mock<IMediator>();

            handler = new UnlockGateCommandHandler(mockUserRepository.Object, mockMediator.Object);
        }

        [Test]
        public async Task IfUserIsGrantedAccessGateShouldOpen()
        {
            string gateToBeAccessed = "gate-1";
            var user = new User("Aykut", "Bayansal", "aykut");
            user.GrantAccess(gateToBeAccessed);

            mockUserRepository.Setup(r => r.Get(It.IsAny<string>())).Returns(user);

            var result = await handler.Handle(new UnlockGateCommand("aykut", gateToBeAccessed), CancellationToken.None);
            Assert.AreEqual(result.IsSuccess, true);
        }

        [Test]
        public async Task IfUserIsNotGrantedAccessGateShouldNotOpen()
        {
            string gateToBeAccessed = "gate-1";
            var user = new User("Aykut", "Bayansal", "aykut");

            mockUserRepository.Setup(r => r.Get(It.IsAny<string>())).Returns(user);

            var result = await handler.Handle(new UnlockGateCommand("aykut", gateToBeAccessed), CancellationToken.None);
            Assert.AreEqual(result.IsSuccess, false);
        }

        [Test]
        public void IfUserDoesNotExistErrorShouldBeThrown()
        {
            mockUserRepository.Setup(r => r.Get(It.IsAny<string>())).Returns((User)null);

            Assert.ThrowsAsync<UserDoesNotExistException>(async () => await handler.Handle(new UnlockGateCommand("aykut", "gate-1"), CancellationToken.None));
        }
    }
}
