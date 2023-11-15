using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Global;

namespace Global
{
    public class LibroDiario
    {
        public List<AsientoContable> Asientos { get; set; }

        public LibroDiario()
        {
            Asientos = new List<AsientoContable>();
        }

        public void AgregarAsiento(AsientoContable asiento)
        {
            Asientos.Add(asiento);
        }
    }
}