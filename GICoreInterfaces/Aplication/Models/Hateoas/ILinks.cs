using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreInterfaces.Aplication.Models.Hateoas
{
    /// <summary>
    /// coleccion de links hateos
    /// </summary>
    public interface ILinks
    {
        /// <summary>
        /// lista de links hateos
        /// </summary>
        List<ILink> links { get; set; }
    }
}
