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

namespace IdeaCoreTesting
{
    public class UnitTest1
    {
        [Fact]
        public async void Test1()
        {
            var options = new DbContextOptionsBuilder<TestDBContext>().UseInMemoryDatabase(databaseName: "TestDB").Options;
            
            using (var dbcontext = new TestDBContext(options))
            {
                dbcontext.Tipos.Add(new Tipo
                {
                    IdTipo = 1,
                    Descripcion = "Linea Blanca"
                });

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

                dbcontext.SaveChanges();
            }

            using (var dbcontext = new TestDBContext(options))
            {
                //var valores = dbcontext.Set<Electrodomestico>().ToList();

                var mockHttpContextAccesor = new Mock<IHttpContextAccessor>();
                var context = new DefaultHttpContext();
                context.Connection.RemoteIpAddress = new IPAddress(16885952);
                context.Request.Headers["Accept"] = "application/api.genesis.hateoas+json";
                context.Request.Scheme = "https";
                context.Request.Host = new HostString("localhost");
                context.Request.Path = "/api/Tipo";
                mockHttpContextAccesor.Setup(_ => _.HttpContext).Returns(context);

                IHateoasListWrapperService hateoasListWrapperService = new HateoasListWrapperService(mockHttpContextAccesor.Object);

                IUnitOfWork unitOfWork = new TestUnitOfWork(dbcontext);

                var linkGenerator = new Mock<LinkGenerator>();
                string mockLink = "https://foo";
                linkGenerator.Setup(m => m.GetUriByAddress(context, It.IsAny<RouteValuesAddress>(), It.IsAny<RouteValueDictionary>(), default, default, default, default, default, default)).Returns(mockLink);

                //string link = linkGenerator.Object.GetUriByAction(context, "MyAction", "MyController", new { route = "value" });

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

                var mapper = mockMapper.CreateMapper();

                var appService = new AppServices(unitOfWork, mapper, hateoasListWrapperService);

                var mockLogger = new Mock<ILogger<TipoController>>();
                ILogger<TipoController> logger = mockLogger.Object;
                logger = Mock.Of<ILogger<TipoController>>();


                var controller = new TipoController(logger, appService);
                controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext();
                controller.ControllerContext.HttpContext = context;


                var todos = await controller.GetAll();
            }


            Assert.Equal(StatusCodes.Status200OK, StatusCodes.Status200OK);
        }
    }
}