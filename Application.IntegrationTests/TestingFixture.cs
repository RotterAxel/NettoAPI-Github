using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Entities.Insurance;
using Infrastructure.Persistence.DbContexts.Insurance;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Respawn;
using WebUI;

namespace Application.IntegrationTests
{
    [SetUpFixture]
    public class TestingFixture
    {
        private static IConfigurationRoot _configuration;
        private static IServiceScopeFactory _scopeFactory;
        private static Checkpoint _checkpoint;
        private static IWebHostEnvironment _hostEnvironment;
        private static ServiceCollection _services;
        private static CurrentUser _currentUser = new CurrentUser();

        public class CurrentUser
        {
            public string KeycloakUserGuid;
            public int? ApiUserId { get; set; }
            public bool IsAdmin;
            public bool IsBearbeiter;
            public bool IstVermittler;

            public void ResetUser()
            {
                KeycloakUserGuid = null;
                IsAdmin = false;
                IsBearbeiter = false;
                IstVermittler = false;
            }
        }

        [OneTimeSetUp]
        public void RunBeforeAnyTests()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true)
                .AddEnvironmentVariables();

            _configuration = builder.Build();

            _hostEnvironment = Mock.Of<IWebHostEnvironment>(w =>
                w.EnvironmentName == "Development" &&
                w.ApplicationName == "WebUI");

            var startup = new Startup(_configuration, _hostEnvironment);

            _services = new ServiceCollection();

            _services.AddSingleton(_hostEnvironment);

            _services.AddLogging();

            startup.ConfigureServices(_services);

            // START - Replace service registration for ICurrentUserService
            var currentUserServiceDescriptor = _services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            _services.Remove(currentUserServiceDescriptor);

            // Register testing version
            _services.AddTransient(provider =>
                Mock.Of<ICurrentUserService>(s => s.KeycloakUserId == _currentUser.KeycloakUserGuid));
            //END - Replace service registration for ICurrentUserService
            
            //START - Replace Hangfire with Mock service
            
            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            _checkpoint = new CheckpointMySql()
            {
                DbAdapter = DbAdapter.MySql,
                TablesToIgnore = new[] {"__EFMigrationsHistory"}
            };

            EnsureDatabase();
        }

        public static CurrentUser RunAsAdminUser()
        {
            var currentUserServiceDescriptor = _services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            _services.Remove(currentUserServiceDescriptor);

            // Define what this service returns
            var userServiceMock = new Mock<ICurrentUserService>();

            userServiceMock.SetupGet(s => s.KeycloakUserId).Returns(new Guid().ToString);
            userServiceMock.SetupGet(s => s.IsAdmin).Returns(true);

            _currentUser.KeycloakUserGuid = userServiceMock.Object.KeycloakUserId;
            _currentUser.IsAdmin = userServiceMock.Object.IsAdmin;

            _services.AddTransient(provider => userServiceMock.Object);

            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            return _currentUser;
        }

        public static CurrentUser RunAsBearbeiterUser()
        {
            var currentUserServiceDescriptor = _services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            _services.Remove(currentUserServiceDescriptor);

            // Define what this service returns
            var userServiceMock = new Mock<ICurrentUserService>();

            userServiceMock.SetupGet(s => s.KeycloakUserId).Returns(new Guid().ToString);
            userServiceMock.SetupGet(s => s.IsBearbeiter).Returns(true);

            _currentUser.KeycloakUserGuid = userServiceMock.Object.KeycloakUserId;
            _currentUser.IsBearbeiter = userServiceMock.Object.IsBearbeiter;

            _services.AddTransient(provider => userServiceMock.Object);

            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            return _currentUser;
        }

        public static CurrentUser RunAsVermittlerUser()
        {
            var currentUserServiceDescriptor = _services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            _services.Remove(currentUserServiceDescriptor);

            // Define what this service returns
            var userServiceMock = new Mock<ICurrentUserService>();

            userServiceMock.SetupGet(s => s.KeycloakUserId).Returns(new Guid().ToString);
            userServiceMock.SetupGet(s => s.IstVermittler).Returns(true);
            userServiceMock.SetupGet(s => s.ApiUserId).Returns(1);

            _currentUser.KeycloakUserGuid = userServiceMock.Object.KeycloakUserId;
            _currentUser.IstVermittler = userServiceMock.Object.IstVermittler;

            _services.AddTransient(provider => userServiceMock.Object);

            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            return _currentUser;
        }

        public static CurrentUser RunAsNewVermittler()
        {
            var currentUserServiceDescriptor = _services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            _services.Remove(currentUserServiceDescriptor);

            // Define what this service returns
            var userServiceMock = new Mock<ICurrentUserService>();

            userServiceMock.SetupGet(s => s.KeycloakUserId).Returns(new Guid().ToString);
            userServiceMock.SetupGet(s => s.IstVermittler).Returns(true);
            userServiceMock.SetupGet(s => s.Email).Returns("bspemail@localhost");

            _currentUser.KeycloakUserGuid = userServiceMock.Object.KeycloakUserId;
            _currentUser.IstVermittler = userServiceMock.Object.IstVermittler;

            _services.AddTransient(provider => userServiceMock.Object);

            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            return _currentUser;
        }

        public static CurrentUser RunAsPassedInVermittler(Vermittler vermittler)
        {
            var currentUserServiceDescriptor = _services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            _services.Remove(currentUserServiceDescriptor);

            // Define what this service returns
            var userServiceMock = new Mock<ICurrentUserService>();

            userServiceMock.SetupGet(s => s.KeycloakUserId)
                .Returns(vermittler.User.KeycloakIdentifier.ToString());
            userServiceMock.SetupGet(s => s.IstVermittler).Returns(true);
            userServiceMock.SetupGet(s => s.ApiUserId).Returns(vermittler.User.Id);

            _currentUser.KeycloakUserGuid = userServiceMock.Object.KeycloakUserId;
            _currentUser.IstVermittler = userServiceMock.Object.IstVermittler;

            _services.AddTransient(provider => userServiceMock.Object);

            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();

            return _currentUser;
        }

        private static void RunAsAnonymous()
        {
            var currentUserServiceDescriptor = _services.FirstOrDefault(d =>
                d.ServiceType == typeof(ICurrentUserService));

            _services.Remove(currentUserServiceDescriptor);

            // Define what this service returns
            var userServiceMock = new Mock<ICurrentUserService>();

            userServiceMock.SetupGet(s => s.KeycloakUserId).Returns(new Guid().ToString);

            _currentUser.KeycloakUserGuid = userServiceMock.Object.KeycloakUserId;

            _services.AddTransient(provider => userServiceMock.Object);

            _scopeFactory = _services.BuildServiceProvider().GetService<IServiceScopeFactory>();
        }

        private static void EnsureDatabase()
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<InsuranceDbContext>();

            context.Database.Migrate();
        }

        public static async Task<TResponse> SendAsync<TResponse>(IRequest<TResponse> request)
        {
            using var scope = _scopeFactory.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediator>();

            return await mediator.Send(request);
        }

        public static bool ValidateEinladecodeVermittler(string einladecode)
        {
            using var scope = _scopeFactory.CreateScope();

            var einladecodeVermittlerValidation = scope.ServiceProvider.GetService<IEinladecodeVermittlerValidation>();

            return einladecodeVermittlerValidation.Validate(einladecode);
        }

        public static string AESEncrypt(string message)
        {
            using var scope = _scopeFactory.CreateScope();

            var aesCryptographyService = scope.ServiceProvider.GetService<IAESCryptographyService>();

            return aesCryptographyService.EncryptString(message);
        }

        public static string AESDecrypt(string message)
        {
            using var scope = _scopeFactory.CreateScope();

            var aesCryptographyService = scope.ServiceProvider.GetService<IAESCryptographyService>();

            return aesCryptographyService.DecryptString(message);
        }
        
        public static string GenerateRandomStringOfLength(int length, 
            bool includeUppercaseLetters, 
            bool includeLowercaseLetters, 
            bool includeDigits)
        {
            using var scope = _scopeFactory.CreateScope();

            var randomStringGenerator = scope.ServiceProvider.GetService<IRandomStringGenerator>();

            return randomStringGenerator.GenerateRandomStringOfLength(length, 
                includeUppercaseLetters, 
                includeLowercaseLetters, 
                includeDigits);
        }

        public static async Task<string> GenerateVermittlerNoAsync()
        {
            using var scope = _scopeFactory.CreateScope();

            var vermittlerNoGenerator = scope.ServiceProvider.GetService<IVermittlerNoGenerator>();

            return await vermittlerNoGenerator.GenerateVermittlerNoAsync();
        }

        public static async Task ResetState()
        {
            await _checkpoint.Reset(_configuration.GetConnectionString("InsuranceConnection"));
            _currentUser.ResetUser();
            RunAsAnonymous();
        }

        public static async Task<TEntity> FindAsync<TEntity>(int id)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<InsuranceDbContext>();

            return await context.FindAsync<TEntity>(id);
        }

        public static async Task<List<Dokument>> GetDokumentListeFürVermittler(int id)
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<InsuranceDbContext>();

            return (await context.Vermittler
                .Include(v => v.RegistrierungsDokumente)
                .FirstAsync(v => v.Id == id)).RegistrierungsDokumente.ToList();
        }

        public static async Task<Vermittler> FindVermittlerAsync(int id)
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<InsuranceDbContext>();

            return await context.Vermittler
                .Include(v => v.User)
                .ThenInclude(u => u.Adresse)
                .ThenInclude(a => a.Land)
                .Include(v => v.Bankverbindung)
                .Include(v => v.RegistrierungsDokumente)
                .Include(v => v.RegistrierungsDokumenteHistorie)
                .Include(v => v.EinladecodeVermittler)
                .Include(v => v.VermittlerGesellschafften)
                .FirstAsync(v => v.Id == id);
        }
        
        public static async Task<VermittlerGesellschafft> FindVermittlerGesellschaftAsync(int gesellschaftId, int vermittlerId)
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<InsuranceDbContext>();

            return await context.VermittlerGesellschafften
                .FirstAsync(v => v.GesellschaftId == gesellschaftId && v.VermittlerId == vermittlerId);
        }

        public static async Task AddAsync<TEntity>(TEntity entity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<InsuranceDbContext>();

            context.Add(entity);

            await context.SaveChangesAsync();
        }

        public static async Task UpdateAsync<TEntity>(TEntity updatedEntity)
            where TEntity : class
        {
            using var scope = _scopeFactory.CreateScope();

            var context = scope.ServiceProvider.GetService<InsuranceDbContext>();

            context.Update(updatedEntity);

            await context.SaveChangesAsync();
        }

        [OneTimeTearDown]
        public void RunAfterAnyTests()
        {
            _configuration = null;
            _scopeFactory = null;
            _checkpoint = null;
            _hostEnvironment = null;
        }
    }
}