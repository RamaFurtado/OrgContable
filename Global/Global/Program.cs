using Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Global
{

    class Program
    {
        static void Main()
        {
            ContabilidadService contabilidadService = new ContabilidadService();

            // Ejemplo de cómo agregar un asiento y una cuenta.
            AsientoContable asiento = new AsientoContable
            {
                Fecha = DateTime.Now,
                CuentaId = 1,
                NombreCuenta = "Cuenta de Ejemplo",
                Importe = 100.00m,
                Tipo = TipoAsiento.Debe
            };

            contabilidadService.GuardarAsiento(asiento);
            // Ejemplo de cómo obtener cuentas de un asiento por fecha.
            DateTime fechaBusquedaAsiento = DateTime.Now;
            List<CuentaAsiento> cuentasDeAsiento = contabilidadService.ObtenerCuentasDeAsiento(fechaBusquedaAsiento);

            foreach (var cuentaAsiento in cuentasDeAsiento)
            {
                if (cuentaAsiento.Cuenta != null)
                {
                    Console.WriteLine($"ID de Cuenta: {cuentaAsiento.Cuenta.CuentaId}, Nombre de Cuenta: {cuentaAsiento.Cuenta.Nombre}, Importe: {cuentaAsiento.Importe}");
                }
                else
                {
                    Console.WriteLine($"CuentaAsiento.Cuenta es null, no se puede acceder a las propiedades.");
                }
            }
            CuentaContable cuenta = new CuentaContable
            {
                CuentaId = 1,
                Nombre = "Cuenta de Ejemplo"
            };

            contabilidadService.GuardarCuenta(cuenta);

            // Ejemplo de cómo obtener un asiento por fecha.

            DateTime fechaBusqueda = DateTime.Now;
            List<AsientoContable> asientosPorFecha = contabilidadService.ObtenerAsientoPorFecha(fechaBusqueda);

            // Ejemplo de cómo obtener todos los asientos.
            List<AsientoContable> todosLosAsientos = contabilidadService.ObtenerTodosLosAsientos();

            // Ejemplo de cómo filtrar asientos entre dos fechas.
            DateTime fechaInicio = DateTime.Now.AddDays(-7);
            DateTime fechaFin = DateTime.Now;

            List<AsientoContable> asientosFiltrados = contabilidadService.FiltrarAsientosEntreFechas(fechaInicio, fechaFin);


            // Ejemplo de cómo generar un libro mayor.
            Dictionary<int, decimal> libroMayor = contabilidadService.GenerarLibroMayor(1);

            // Ejemplo de cómo obtener una cuenta por ID.
            CuentaContable cuentaObtenida = contabilidadService.ObtenerCuentaPorId(1);

            if (cuentaObtenida != null)
            {
                Console.WriteLine($"Cuenta encontrada: {cuentaObtenida.Nombre}");
            }
            else
            {
                Console.WriteLine("Cuenta no encontrada.");
            }

            // Ejemplo de cómo obtener todas las cuentas.
            List<CuentaContable> todasLasCuentas = contabilidadService.ObtenerTodasLasCuentas();

            Console.WriteLine("Todas las cuentas:");
            foreach (var c in todasLasCuentas)
            {
                Console.WriteLine($"ID: {c.CuentaId}, Nombre: {c.Nombre}");
            }
        }
    }
}
