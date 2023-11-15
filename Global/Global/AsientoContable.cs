using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Global;

namespace Global
{

    public class AsientoContable
    {
        public DateTime Fecha { get; set; }
        public int? CuentaId { get; set; }  // Cambié a int? para permitir valores nulos
        public string NombreCuenta { get; set; }
        public decimal Importe { get; set; }
        public TipoAsiento Tipo { get; set; }
    }

    public enum TipoAsiento
    {
        Debe,
        Haber
    }
}