using IdeaCoreInterfaces.Aplication.Models.Hateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdeaCoreUtils.Hateoas
{
    /// <summary>
    /// coleccion de links hateos
    /// </summary>
    public class Links : ILinks
    {
        /// <summary>
        /// lista de links hateos
        /// </summary>
        public List<ILink> links { get; set; }
    }
}
