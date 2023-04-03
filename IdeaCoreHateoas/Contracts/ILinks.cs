using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace IdeaCoreHateoas.Contracts
{
    /// <summary>
    /// coleccion de links hateos
    /// </summary>
    public interface ILinks
    {
        [JsonIgnore]
        IHateoasProfileParameters HateoasParameters { get; set; }
        void setLinks();
        /// <summary>
        /// lista de links hateos
        /// </summary>
        List<ILink> links { get; set; }
    }
}
