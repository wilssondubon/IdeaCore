using Entities;
using IdeaCoreInfraestructure.Repository;
using IdeaCoreInterfaces.Infraestructure;
using Infraestructure.DBContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Identity.Client;
using Moq;

namespace IdeaCoreInfraestructure.Tests
{
    public class GenericRepositoryTest
    {
        private DbContext _dbcontext;
        private IGenericRepository<Tipo> _repository;

        public GenericRepositoryTest()
        {
            _dbcontext = getTestDbContext();
            Func<IQueryable<Tipo>, IIncludableQueryable<Tipo, object>>[] funct = {((IQueryable<Tipo> t) => t.Include(i => i.Electrodomestico))};
            _repository = new GenericRepository<Tipo>(_dbcontext, funct);
        }

        private TestDBContext getTestDbContext()
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

            dbcontext.SaveChanges();
            return dbcontext;
        }

        [Fact]
        public void GenericRepository_GetNavigationProperties_NotNull()
        {
            var response = _repository.GetNavigationProperties();

            Assert.NotNull(response);
            Assert.True(response.Count() == 1);
        }

        [Fact]
        public void GenericRepository_GetPrimaryKeyProperties_NotNull()
        {
            var response = _repository.GetPrimaryKeyProperties();

            Assert.NotNull(response);
            Assert.True(response.Count() == 1);
        }

        [Fact]
        public void GenericRepository_Insert_EntityAdded()
        {
            var tipo = Mock.Of<Tipo>();
            tipo.IdTipo = 4;

            _repository.Insert(tipo);

            Assert.True(_repository.DbSet.Local.Count == 4);
        }

        [Fact]
        public void GenericRepository_Update_EntityModified()
        {
            var firstElement = _repository.DbSet.Find((short)1);
            firstElement.Descripcion = "Linea Blanca.";

            _repository.Update(firstElement);

            Assert.True(_repository.DbSet.Local.Where(t=>t.IdTipo == 1).FirstOrDefault().Descripcion == "Linea Blanca.");
        }

        [Fact]
        public void GenericRepository_Delete_ByEntity_EntityDeleted()
        {
            var firstElement = _repository.DbSet.Find((short)1);

            _repository.Delete(firstElement);

            Assert.True(_repository.DbSet.Local.Count == 2);
        }

        [Fact]
        public void GenericRepository_Delete_ById_EntityDeleted()
        {
            _repository.Delete((short)1);

            Assert.True(_repository.DbSet.Local.Count == 2);
        }

        [Fact]
        public void GenericRepository_Delete_ByParams_EntityDeleted()
        {
            object[] parameters = { (short)1 };

            _repository.Delete(parameters);

            Assert.True(_repository.DbSet.Local.Count == 2);
        }

        [Fact]
        public async Task GenericRepository_FindById_EntityRetrieved()
        {
            object[] parameters = { (short)1 };

            var element = await _repository.FindByID(parameters);

            Assert.NotNull(element);
        }

        [Fact]
        public async Task GenericRepository_Get_ByObject_EntityRetrieved()
        {
            Tipo tipo = new Tipo() { IdTipo = 1 };

            var element = await _repository.Get(tipo);

            Assert.NotNull(element);
        }

        [Fact]
        public async Task GenericRepository_GetFirst_EntityRetrieved()
        {
            var element = await _repository.GetFirst();

            Assert.NotNull(element);
        }

        [Fact]
        public async Task GenericRepository_GetFirst_WithPredicate_EntityRetrieved()
        {
            var element = await _repository.GetFirst(t=>t.IdTipo == 2);

            Assert.NotNull(element);
        }

        [Fact]
        public async Task GenericRepository_GetFirst_WithPredicateAndOrderByExpression_EntityRetrieved()
        {
            var elementfirst = await _repository.GetFirst(t => t.IdTipo > 0,  t => t.OrderBy(t => t.IdTipo));
            var elementlast = await _repository.GetFirst(t => t.IdTipo > 0,  t => t.OrderByDescending(t => t.IdTipo));

            Assert.NotNull(elementfirst);
            Assert.True(elementfirst.IdTipo == 1);
            Assert.NotNull(elementlast);
            Assert.True(elementlast.IdTipo == 3);
        }

        [Fact]
        public async Task GenericRepository_GetFirst_WithPredicateAndOrderByAsText_EntityRetrieved()
        {
            var elementfirst = await _repository.GetFirst(t => t.IdTipo > 0, "IdTipo");
            var elementlast = await _repository.GetFirst(t => t.IdTipo > 0, "IdTipo desc");

            Assert.NotNull(elementfirst);
            Assert.True(elementfirst.IdTipo == 1);
            Assert.NotNull(elementlast);
            Assert.True(elementlast.IdTipo == 3);
        }

        [Fact]
        public async Task GenericRepository_GetFirst_WithOrderByExpression_EntityRetrieved()
        {
            var elementfirst = await _repository.GetFirst(t => t.OrderBy(t => t.IdTipo));
            var elementlast = await _repository.GetFirst(t => t.OrderByDescending(t => t.IdTipo));

            Assert.NotNull(elementfirst);
            Assert.True(elementfirst.IdTipo == 1);
            Assert.NotNull(elementlast);
            Assert.True(elementlast.IdTipo == 3);
        }

        [Fact]
        public async Task GenericRepository_GetFirst_WithOrderByAsText_EntityRetrieved()
        {
            var elementfirst = await _repository.GetFirst("IdTipo");
            var elementlast = await _repository.GetFirst("IdTipo desc");

            Assert.NotNull(elementfirst);
            Assert.True(elementfirst.IdTipo == 1);
            Assert.NotNull(elementlast);
            Assert.True(elementlast.IdTipo == 3);
        }

        [Fact]
        public async Task GenericRepository_GetFirst_WithDictionary_EntityRetrieved()
        {
            KeyValuePair<string, string> idtipo = new KeyValuePair<string, string>("IdTipo", "1");
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add(idtipo);

            var element = await _repository.GetFirst(parameters);

            Assert.NotNull(element);
        }

        [Fact]
        public async Task GenericRepository_GetFirst_WithDictionaryAndOrderByExpression_EntityRetrieved()
        {
            KeyValuePair<string, string> idtipo = new KeyValuePair<string, string>("IdTipo", "1");
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add(idtipo);

            var elementfirst = await _repository.GetFirst(parameters, t => t.OrderBy(t => t.IdTipo));

            Assert.NotNull(elementfirst);
            Assert.True(elementfirst.IdTipo == 1);
        }

        [Fact]
        public async Task GenericRepository_GetFirst_WithDictionaryAndOrderByAsText_EntityRetrieved()
        {
            KeyValuePair<string, string> idtipo = new KeyValuePair<string, string>("IdTipo", "1");
            IDictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add(idtipo);

            var elementfirst = await _repository.GetFirst(parameters, "IdTipo");

            Assert.NotNull(elementfirst);
            Assert.True(elementfirst.IdTipo == 1);
        }

        [Fact]
        public void GenericRepository_GetById_WithObject_EntityRetrieved()
        {
            short parameter = 1;

            var element = _repository.GetByID(parameter);

            Assert.NotNull(element);
            Assert.True(element.IdTipo == 1);
        }

        [Fact]
        public async Task GenericRepository_GetByIdAsync_WithObject_EntityRetrieved()
        {
            short parameter = 1;

            var element = await _repository.GetByIDAsync(parameter);

            Assert.NotNull(element);
            Assert.True(element.IdTipo == 1);
        }

        [Fact]
        public void GenericRepository_GetById_WithParams_EntityRetrieved()
        {
            object[] parameters = { (short)1 };

            var element =  _repository.GetByID(parameters);

            Assert.NotNull(element);
            Assert.True(element.IdTipo == 1);
        }

        [Fact]
        public async Task GenericRepository_GetByIdAsync_WithParams_EntityRetrieved()
        {
            object[] parameters = { (short)1 };

            var element =await  _repository.GetByIDAsync(parameters);

            Assert.NotNull(element);
            Assert.True(element.IdTipo == 1);
        }
    }
}