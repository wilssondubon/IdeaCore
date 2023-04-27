using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;
using IdeaCoreInterfaces.Hateoas;
using IdeaCoreUtils.Models;

namespace IdeaCoreHateoas
{
    /// <summary>
    /// coleccion de links hateos
    /// </summary>
    public class Links : ILinks
    {
        [JsonIgnore]
        public IHateoasProfileParameters HateoasParameters { get; set; }

        /// <summary>
        /// lista de links hateos
        /// </summary>
        public List<ILink> links { get; set; }

        public Links() { }

        public Links(IHateoasProfileParameters parameters)
        {
            HateoasParameters = parameters;
        }

        public void setLinks()
        {
            IEndpointDescriptorList modelEndpoints = HateoasParameters.Endpoints.GetEndpointsForModel(this.GetType());

            if (!isHateoasRequest())
                return;

            if (this.links == null)
                this.links = new List<ILink>();

            if (modelEndpoints != null && modelEndpoints.Count > 0)
            {
                foreach (IEndpointDescriptor endpoint in modelEndpoints)
                {
                    string uriString = HateoasParameters.LinkGenerator.GetUriByAction(HateoasParameters.HttpContextAccessor.HttpContext, endpoint.Accion, endpoint.Controlador, values: endpoint.Valores != null ? endpoint.Valores(this) : null);
                    Uri uri = new Uri(uriString);
                    Link newLink = new Link(uri, endpoint.Relacion, endpoint.Metodo);
                    this.links.Add(newLink);
                }
            }
        }

        private bool isHateoasRequest()
        {
            StringValues acceptHeader;
            bool getResult = HateoasParameters.HttpContextAccessor.HttpContext.Request.Headers.TryGetValue("Accept", out acceptHeader);

            MediaTypeHeaderValue AcceptmediaType = MediaTypeHeaderValue.Parse(acceptHeader.ToString());

            if (getResult && !AcceptmediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
                return false;

            return true;
        }
    }
}
