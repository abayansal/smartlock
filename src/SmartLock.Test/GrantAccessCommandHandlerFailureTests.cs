using System.Threading;
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
    public class GrantAccessCommandHandlerFailureTests
    {
        private readonly Mock<IUserRepository> mockUserRepository;
        private readonly Mock<IGateRepository> mockGateRepository;
        private readonly GrantAccessCommandHandler handler;

        public GrantAccessCommandHandlerFailureTests()
        {
            mockUserRepository = new Mock<IUserRepository>();
            mockGateRepository = new Mock<IGateRepository>();
            var mockMediator = new Mock<IMediator>();

            handler = new GrantAccessCommandHandler(mockUserRepository.Object, mockGateRepository.Object, mockMediator.Object);
        }

        [Test]
        public void IfUserDoesNotExistErrorShouldBeThrown()
        {
            mockGateRepository.Setup(r => r.Get(It.IsAny<string>())).Returns(new Gate("gate-1", "Outer Gate"));
            mockUserRepository.Setup(r => r.Get(It.IsAny<string>())).Returns((User)null);

            Assert.ThrowsAsync<UserDoesNotExistException>( async () => await handler.Handle(new GrantAccessCommand("some user", "gate-1"), CancellationToken.None));
        }

        [Test]
        public void IfGateDoesNotExistErrorShouldBeThrown()
        {
            mockGateRepository.Setup(r => r.Get(It.IsAny<string>())).Returns((Gate)null);
            mockUserRepository.Setup(r => r.Get(It.IsAny<string>())).Returns(new User("Aykut", "Bayansal", "aykut"));

            Assert.ThrowsAsync<GateDoesNotExistException>(async () => await handler.Handle(new GrantAccessCommand("aykut", "gate-nonexistant"), CancellationToken.None));
        }

        [Test]
        public void IfTheCommandIsNotCompleteShouldThrowValidationException()
        {
            Assert.ThrowsAsync<MissingInformationProvidedException>(async () => await handler.Handle(new GrantAccessCommand(string.Empty, "gate-nonexistant"), CancellationToken.None));
        }
    }
}
