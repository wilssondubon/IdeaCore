using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IdeaCoreInfraestructure.UnitOfWork;
using IdeaCoreTesting.Context;

namespace IdeaCoreTesting
{
    public class TestUnitOfWork : UnitOfWork
    {
        public TestUnitOfWork(TestDBContext db) : base(db)
        {
        }
    }
}
