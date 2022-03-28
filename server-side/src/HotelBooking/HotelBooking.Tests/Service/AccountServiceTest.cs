using System.Threading.Tasks;
using HotelBooking.Contracts.Domain.Repositories;
using HotelBooking.Contracts.Dto.Data;
using HotelBooking.Contracts.Dto.Models.Register;
using HotelBooking.Services;
using HotelBooking.Services.Interfaces;
using HotelBooking.Services.Interfaces.Factories;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;

namespace HotelBooking.Tests.Service
{
    [TestFixture]
    internal class AccountServiceTest : UnitTestBase
    {
        private const string UpdateClientAccountAsyncMethodName = nameof(AccountService.UpdateClientAccountAsync) + ". ";
        private const string GetClientAccountAsyncMethodName = nameof(AccountService.GetClientAccountAsync) + ". ";
        private const string RegisterAsyncMethodName = nameof(AccountService.RegisterAsync) + ". ";
        private const string LogoutAsyncMethodName = nameof(AccountService.LogoutAsync) + ". ";
        private const string LogInAsyncMethodName = nameof(AccountService.LogInAsync) + ". ";

        private RegistrationRequestModel registrationModel = new RegistrationRequestModel
        {
            Role = "Client",
            Email = "TestEmail1@gmail.com",
            Password = "Qwerty"
        };

        private Mock<IApplicationUserRepository> applicationUserRepositoryMock;
        private Mock<ISessionRepository> sessionRepositoryMock;
        private Mock<IClientRepository> clientRepositoryMock;
        private Mock<IJavascriptWebTokenFactory> javascriptWebTokenFactoryMock;
        private Mock<IApplicationRoleRepository> applicationRoleRepositoryMock;
        private Mock<ISessionHandler> sessionHandlerMock;
        private Mock<ICreditCardRepository> creditCardRepository;

        private AccountService accountService;

        [SetUp]
        public void TestInitialize()
        {
            applicationUserRepositoryMock = MockRepository.Create<IApplicationUserRepository>();
            sessionRepositoryMock = MockRepository.Create<ISessionRepository>();
            clientRepositoryMock = MockRepository.Create<IClientRepository>();
            javascriptWebTokenFactoryMock = MockRepository.Create<IJavascriptWebTokenFactory>();
            applicationRoleRepositoryMock = MockRepository.Create<IApplicationRoleRepository>();
            sessionHandlerMock = MockRepository.Create<ISessionHandler>();
            creditCardRepository = MockRepository.Create<ICreditCardRepository>();


            accountService = new AccountService(
                applicationUserRepositoryMock.Object,
                sessionRepositoryMock.Object,
                clientRepositoryMock.Object,
                javascriptWebTokenFactoryMock.Object,
                applicationRoleRepositoryMock.Object,
                sessionHandlerMock.Object,
                creditCardRepository.Object);
        }

        [TestCase(TestName = RegisterAsyncMethodName + "Should return result from applicationUserRepository CreateAsync method")]
        public async Task RegisterAsyncTest()
        {
            string token = string.Empty;
            RoleData roleData = CreateRoleData();
            UserData userData = CreateUserData(roleData);
            ClientData clientData = CreateClientData(userData);
            SessionData sessionData = CreateSessionData();
            RegistrationResponseModel expected = new RegistrationResponseModel() { IsSuccessful = true };

            SetupApplicationRoleRepositoryGetMock(roleData);
            SetupApplicationUserRepositoryCreateAsyncMock(userData);
            SetupApplicationUserRepositoryFindByEmailAsyncMock(userData);
            SetupClientRepositoryAddAsyncMock(clientData);
            SetupJavascriptWebTokenFactoryCreateMock(userData.Id, token);
            SetupSessionRepositoryCreateAsyncMock(sessionData);

            RegistrationResponseModel actual = await accountService.RegisterAsync(registrationModel);

            Assert.AreEqual(expected, actual);
        }

        [TestCase(TestName = LogInAsyncMethodName + "")]
        public async Task LogInAsync()
        {

        }

        [TestCase(TestName = GetClientAccountAsyncMethodName + "")]
        public async Task GetClientAccountAsync()
        {

        }

        [TestCase(TestName = UpdateClientAccountAsyncMethodName + "")]
        public async Task UpdateClientAccountAsync()
        {

        }

        [TestCase(TestName = LogoutAsyncMethodName + "")]
        public async Task LogoutAsync()
        {

        }

        private void SetupApplicationRoleRepositoryGetMock(RoleData roleData)
            => applicationRoleRepositoryMock
                .Setup(repository => repository.Get(registrationModel.Role))
                .Returns(roleData);

        private void SetupApplicationUserRepositoryCreateAsyncMock(UserData userData)
            => applicationUserRepositoryMock
               .Setup(repository => repository.CreateAsync(userData))
               .ReturnsAsync(CreateIdentityResult);

        private void SetupApplicationUserRepositoryFindByEmailAsyncMock(UserData userData)
            => applicationUserRepositoryMock
                .Setup(repository => repository.FindByEmailAsync(userData.Email))
                .ReturnsAsync(userData);

        private void SetupClientRepositoryAddAsyncMock(ClientData clientData)
            => clientRepositoryMock
                .Setup(repository => repository.AddAsync(clientData))
                .ReturnsAsync(CreateClientData());

        private void SetupJavascriptWebTokenFactoryCreateMock(int userId, string token)
            => javascriptWebTokenFactoryMock
                .Setup(repository => repository.Create(userId))
                .Returns(token);

        private void SetupSessionRepositoryCreateAsyncMock(SessionData sessionData)
            => sessionRepositoryMock
                .Setup(repository => repository.CreateAsync(sessionData));

        private RoleData CreateRoleData()
            => new RoleData();

        private UserData CreateUserData(RoleData role)
            => new UserData
            {
                Email = registrationModel.Email,
                Password = registrationModel.Password,
                RoleId = role.Id
            };

        private IdentityResult CreateIdentityResult()
            => new IdentityResult();

        private ClientData CreateClientData()
            => new ClientData();

        private ClientData CreateClientData(UserData userData)
            => new ClientData()
            {
                Name = registrationModel.UserName,
                Surname = registrationModel.Surname,
                PhotoPath = "/default/profile.png",
                UserId = userData.Id
            };

        private SessionData CreateSessionData()
            => new SessionData();
    }
}