using IdeaCoreHateoas;
using IdeaCoreInterfaces.Hateoas;
using IdeaCoreInterfaces.Infraestructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.Common;
using System.Net;
using static System.Collections.Specialized.BitVector32;
using Infraestructure.DBContext;
using Entities;
using Infraestructure.UnitOfWork;
using AutoMapper;
using IdeaCoreTestAPI.MapperProfile;
using IdeaCoreTestAPI.hateoas;
using IdeaCoreTestAPI.Controllers;
using Microsoft.Extensions.Logging;
using IdeaCoreApplication;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Mvc;
using IdeaCoreApplication.Contracts;
using IdeaCoreInfraestructure.UnitOfWork;
using IdeaCorePresentation;
using IdeaCoreInterfaces.Application.Response;
using DTOs;

namespace IdeaCoreTesting
{
    public class TipoControllerTests
    {
        private IUnitOfWork _unitOfWork;
        private IHateoasListWrapperService _hateoasListWrapperService;
        private IMapper _mapper;
        private IAppServices _appServices;
        private ILogger<TipoController> _logger;
        private DefaultHttpContext _context;

        private TipoController _controller;

        private async Task<TestDBContext> getTestDbContext()
        {
            var options = new DbContextOptionsBuilder<TestDBContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;

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

            _controller = new TipoController(_logger, _appServices);
            _controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext();
            _controller.ControllerContext.HttpContext = _context;
        }

        public TipoControllerTests()
        {
            var dbcontext = getTestDbContext().Result;
            _unitOfWork = new TestUnitOfWork(dbcontext);

            _logger = Mock.Of<ILogger<TipoController>>();
        }


        [Fact]
        public async void TipoController_GetAll_ResultOk()
        {
            //Arrange
            installEnvironment("application/api.genesis.hateoas+json");

            //Act
            var todos =  await _controller.GetAll();
            IServiceResponseList<TipoDTO> responseList =(IServiceResponseList<TipoDTO>)((ObjectResult) todos).Value;

            //Assert
            Assert.IsType<OkObjectResult>(todos);
            Assert.True(responseList.data.Count() == 3);
            Assert.True(responseList.links.Count() == 1);
            Assert.True(responseList.data.Where(t=>t.IdTipo==1).First().links.Count() == 1);
        }


        [Fact]
        public async void TipoController_GetById_ResultOk()
        {
            //Arrange
            installEnvironment("application/api.genesis.hateoas+json");

            //Act
            var firstType = await _controller.GetById(1);
            
            //Assert
            Assert.IsType<OkObjectResult>(firstType);
        }
    }
}