using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Application
{
    /// <summary>
    /// Interfaz para la creacion de un servicio con operacion de tipo CRUD
    /// </summary>
    /// <typeparam name="Model">tipo del modelo en el que se basara el servicio</typeparam>
    public interface ICRUDService<Model, Entity> : IRService<Model, Entity>, ICUDService<Model, Entity>, IBasicService where Model : class where Entity : class
    {
    }
}
