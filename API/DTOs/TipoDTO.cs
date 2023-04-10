using IdeaCoreApplication.Attributes;
using IdeaCoreHateoas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    [NoNavigationsOnCreateUpdate]
    public class TipoDTO : Links
    {
        [LastOnInsert]
        public short IdTipo { get; set; }
        [MergeOnUpdate]
        public string Descripcion { get; set; }
    }
}
