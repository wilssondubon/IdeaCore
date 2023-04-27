using AutoMapper;
using DTOs;
using Entities;
using IdeaCoreApplication;
using IdeaCoreApplication.Contracts;
using IdeaCoreHateoas;
using IdeaCoreInterfaces.Application.Response;
using IdeaCoreInterfaces.Hateoas;
using IdeaCoreInterfaces.Infraestructure;
using IdeaCoreTestAPI.Controllers;
using IdeaCoreTestAPI.hateoas;
using IdeaCoreTestAPI.MapperProfile;
using Infraestructure.DBContext;
using Infraestructure.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreTestAPI.Tests
{
    public class ElectrodomesticoControllerTests
    {
        private IUnitOfWork _unitOfWork;
        private IHateoasListWrapperService _hateoasListWrapperService;
        private IMapper _mapper;
        private IAppServices _appServices;
        private ILogger<ElectrodomesticoController> _logger;
        private DefaultHttpContext _context;

        private ElectrodomesticoController _controller;

        private async Task<TestDBContext> getTestDbContext()
        {
            var options = new DbContextOptionsBuilder<TestDBContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()).Options;

            var dbcontext = new TestDBContext(options);

            if (dbcontext.Tipos.Count() <= 0)
            {
                dbcontext.Tipos.Add(new Tipo
                {
                    IdTipo = 1,
                    Descripcion = "Linea Blanca"
                });
                dbcontext.Tipos.Add(new Tipo
                {
                    IdTipo = 2,
                    Descripcion = "Electronicos"
                });
                dbcontext.Tipos.Add(new Tipo
                {
                    IdTipo = 3,
                    Descripcion = "Cocina"
                });
            }

            if (dbcontext.Electrodomesticos.Count() <= 0)
            {
                dbcontext.Electrodomesticos.Add(new Electrodomestico
                {
                    Codigo = 1,
                    IdTipo = 1,
                    Descripcion = "Refrigeradora"
                });

                dbcontext.Electrodomesticos.Add(new Electrodomestico
                {
                    Codigo = 2,
                    IdTipo = 1,
                    Descripcion = "Lavadora"
                });

                dbcontext.Electrodomesticos.Add(new Electrodomestico
                {
                    Codigo = 3,
                    IdTipo = 2,
                    Descripcion = "Television"
                });

                dbcontext.Electrodomesticos.Add(new Electrodomestico
                {
                    Codigo = 4,
                    IdTipo = 2,
                    Descripcion = "Teatro en Casa"
                });

                dbcontext.Electrodomesticos.Add(new Electrodomestico
                {
                    Codigo = 5,
                    IdTipo = 3,
                    Descripcion = "Licuadora"
                });

                dbcontext.Electrodomesticos.Add(new Electrodomestico
                {
                    Codigo = 6,
                    IdTipo = 3,
                    Descripcion = "Batidora"
                });
            }

            await dbcontext.SaveChangesAsync();
            return dbcontext;
        }

        private void installEnvironment(string accept)
        {
            var mockHttpContextAccesor = new Mock<IHttpContextAccessor>();
            _context = new DefaultHttpContext();
            _context.Connection.RemoteIpAddress = new IPAddress(16885952);
            _context.Request.Headers["Accept"] = accept;
            _context.Request.Scheme = "https";
            _context.Request.Host = new HostString("localhost");
            _context.Request.Path = "/api/Tipo";
            mockHttpContextAccesor.Setup(_ => _.HttpContext).Returns(_context);

            _hateoasListWrapperService = new HateoasListWrapperService(mockHttpContextAccesor.Object);

            var linkGenerator = new Mock<LinkGenerator>();
            string mockLink = "https://foo";
            linkGenerator.Setup(m => m.GetUriByAddress(_context, It.IsAny<RouteValuesAddress>(), It.IsAny<RouteValueDictionary>(), default, default, default, default, default, default)).Returns(mockLink);

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(
                    new HateoasEntitiesProfile(
                        new HateoasProfileParameters(
                        new EndpointToModelRelation(new HateoasAPI().Get()),
                        mockHttpContextAccesor.Object,
                        linkGenerator.Object
                        ))
                    );
            });
            _mapper = mockMapper.CreateMapper();

            _appServices = new AppServices(_unitOfWork, _mapper, _hateoasListWrapperService);

            _controller = new ElectrodomesticoController(_logger, _appServices);
            _controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext();
            _controller.ControllerContext.HttpContext = _context;
        }

        public ElectrodomesticoControllerTests()
        {
            var dbcontext = getTestDbContext().Result;
            _unitOfWork = new TestUnitOfWork(dbcontext);

            _logger = Mock.Of<ILogger<ElectrodomesticoController>>();
        }

        [Fact]
        public async void ElectrodomesticoController_GetByTipo_ResultOk()
        {
            //Arrange
            installEnvironment("application/api.genesis.hateoas+json");

            //Act
            var bytype = await _controller.GetByTipo(1, new IdeaCoreUtils.Models.FilterQueryParams() { OrderBy = "Descripcion" });
            IServiceResponseList<ElectrodomesticoDTO> responseList = (IServiceResponseList<ElectrodomesticoDTO>)((ObjectResult)bytype).Value;

            //Assert
            Assert.IsType<OkObjectResult>(bytype);
        }
    }
}
