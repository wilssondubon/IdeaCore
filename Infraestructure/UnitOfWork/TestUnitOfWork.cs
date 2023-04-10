using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infraestructure.DBContext;

namespace Infraestructure.UnitOfWork
{
    public class TestUnitOfWork : IdeaCoreInfraestructure.UnitOfWork.UnitOfWork
    {
        public TestUnitOfWork(TestDBContext db) : base(db)
        {
        }
    }
}
