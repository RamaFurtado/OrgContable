using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Global;


namespace Global
{
    public class ContabilidadService
    {
        private List<LibroDiario> librosDiarios;
        private Dictionary<int, CuentaContable> cuentas;
        private List<AsientoContable> asientos;

        public ContabilidadService()
        {
            librosDiarios = new List<LibroDiario>();
            cuentas = new Dictionary<int, CuentaContable>();
            asientos = new List<AsientoContable>();
        }

        public void GuardarAsiento(AsientoContable asiento)
        {
            asientos.Add(asiento);
            ObtenerLibroDiarioActual().AgregarAsiento(asiento);
        }
        public List<AsientoContable> ObtenerAsientoPorFecha(DateTime fecha)
        {
            return asientos.Where(a => a.Fecha.Date == fecha.Date).ToList();
        }

        public List<AsientoContable> ObtenerTodosLosAsientos()
        {
            return asientos.ToList();
        }

        public List<AsientoContable> FiltrarAsientosEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {
            return asientos.Where(a => a.Fecha >= fechaInicio && a.Fecha <= fechaFin).ToList();
        }
        public void GuardarCuenta(CuentaContable cuenta)
        {
            cuentas[cuenta.CuentaId] = cuenta;
        }

        public CuentaContable ObtenerCuentaPorId(int cuentaId)
        {
            if (cuentas.ContainsKey(cuentaId))
            {
                return cuentas[cuentaId];
            }
            return null;
        }

        public List<CuentaContable> ObtenerTodasLasCuentas()
        {
            return cuentas.Values.ToList();
        }
        public Dictionary<int, decimal> GenerarLibroMayor(int cuentaId)
        {
            Dictionary<int, decimal> libroMayor = new Dictionary<int, decimal>();

            foreach (var libroDiario in librosDiarios)
            {
                foreach (var asiento in libroDiario.Asientos)
                {
                    // Verificar si CuentaId tiene un valor antes de acceder a su propiedad Value.
                    if (asiento.CuentaId.HasValue && asiento.CuentaId.Value == cuentaId)
                    {
                        if (!libroMayor.ContainsKey(asiento.CuentaId.Value))
                        {
                            libroMayor[asiento.CuentaId.Value] = 0;
                        }

                        // Sumar al debe o al haber según el tipo de asiento.
                        libroMayor[asiento.CuentaId.Value] += asiento.Tipo == TipoAsiento.Debe ? asiento.Importe : -asiento.Importe;
                    }
                }
            }


            return libroMayor;

        }

        private LibroDiario ObtenerLibroDiarioActual()
        {
           

            // Por ahora, simplemente crearemos un nuevo libro diario si no hay ninguno
            if (librosDiarios.Count == 0 || librosDiarios.Last().Asientos.Count >= 100) // Por ejemplo, cada libro diario tiene 100 asientos
            {
                var nuevoLibroDiario = new LibroDiario();
                librosDiarios.Add(nuevoLibroDiario);
                return nuevoLibroDiario;
            }

            return librosDiarios.Last();
        }
        public int ObtenerIdPorNombreDeCuenta(string nombreCuenta)
        {
            // Buscar en el diccionario de cuentas por nombre
            var cuenta = cuentas.Values.FirstOrDefault(c => c.Nombre == nombreCuenta);

            // Devolver el ID si se encuentra la cuenta, o un valor predeterminado si no se encuentra
            return cuenta?.CuentaId ?? -1;
        }
        public List<CuentaAsiento> ObtenerCuentasDeAsiento(DateTime fecha)
        {
            var asiento = asientos.FirstOrDefault(a => a.Fecha.Date == fecha.Date);

            if (asiento != null)
            {
                // Obtener las cuentas asociadas al asiento
                var cuentasAsiento = asiento.CuentaId == null
                    ? new List<CuentaAsiento>()  // En caso de que no haya ID de cuenta
                    : new List<CuentaAsiento> { new CuentaAsiento { Cuenta = ObtenerCuentaPorId(asiento.CuentaId.Value), Importe = asiento.Importe } };

                return cuentasAsiento;
            }

            return new List<CuentaAsiento>();
        }
    }
}
