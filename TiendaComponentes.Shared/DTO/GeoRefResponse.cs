using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiendaComponentes.Shared.DTO
{
    public class GeoRefResponse<T>
    {
        public List<T> Provincias { get; set; }
        public List<T> Departamentos { get; set; }
        public List<T> Localidades { get; set; }
    }
}
