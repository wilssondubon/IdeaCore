using IdeaCoreHateoas;
using IdeaCoreInterfaces.Hateoas;
using IdeaCoreInterfaces.Infraestructure;
using IdeaCoreTesting.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System;
using IdeaCoreTesting.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.Common;
using System.Net;
using static System.Collections.Specialized.BitVector32;

namespace IdeaCoreTesting
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
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
                var fakeTenantId = "abcd";
                context.Request.Headers["Tenant-ID"] = fakeTenantId;
                mockHttpContextAccesor.Setup(_ => _.HttpContext).Returns(context);

                IHateoasListWrapperService hateoasListWrapperService = new HateoasListWrapperService(mockHttpContextAccesor.Object);

                IUnitOfWork unitOfWork = new TestUnitOfWork(dbcontext);

                var linkGenerator = new Mock<LinkGenerator>();
                string mockLink = "foo";
                linkGenerator.Setup(m => m.GetUriByAddress(context, It.IsAny<RouteValuesAddress>(), It.IsAny<RouteValueDictionary>(), default, default, default, default, default, default)).Returns(mockLink);

                //string link = linkGenerator.Object.GetUriByAction(context, "MyAction", "MyController", new { route = "value" });
            }


            Assert.Equal(StatusCodes.Status200OK, StatusCodes.Status200OK);
        }
    }
}