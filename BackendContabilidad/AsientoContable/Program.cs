using System;
using System.Collections.Generic;

namespace Contabilidad
{
    public class AsientoContable
    {
        public string? Nombre { get; set; }
        public decimal Importe { get; set; }
        public string? Destino { get; set; }
        public DateTime Fecha { get; }

        public AsientoContable(string? nombre, decimal importe, string? destino, DateTime fecha)
        {
            this.Nombre = nombre ?? string.Empty;
            this.Importe = importe;
            this.Destino = destino;
            this.Fecha = fecha;
        }
    }

    public class Cuenta
    {
        public string Nombre { get; }
        public List<AsientoContable> Asientos { get; }

        public Cuenta(string nombre)
        {
            this.Nombre = nombre;
            this.Asientos = new List<AsientoContable>();
        }

        public void AgregarAsiento(AsientoContable asiento)
        {
            this.Asientos.Add(asiento);
        }

        public void MostrarLibroMayor()
        {
            Console.WriteLine($"Libro Mayor para la cuenta: {this.Nombre}");
            
            decimal saldoActual = 0;

            foreach (AsientoContable asiento in this.Asientos)
            {
                string tipoMovimiento = asiento.Destino == "Debe" ? "Ingreso" : "Egreso";
                saldoActual += asiento.Importe * (asiento.Destino == "Debe" ? 1 : -1);

                Console.WriteLine($"Fecha: {asiento.Fecha}, Tipo: {tipoMovimiento}, Importe: {asiento.Importe}, Saldo Actual: {saldoActual}");
            }
            Console.WriteLine();
        }
    }

    public class LibroDiario
    {
        public readonly List<Cuenta> cuentas;

        public LibroDiario()
        {
            this.cuentas = new List<Cuenta>();
        }

        public void AgregarCuenta(Cuenta cuenta)
        {
            this.cuentas.Add(cuenta);
        }

        public void MostrarAsientos()
        {
            foreach (Cuenta cuenta in this.cuentas)
            {
                Console.WriteLine($"Cuenta: {cuenta.Nombre}");
                foreach (AsientoContable asiento in cuenta.Asientos)
                {
                    Console.WriteLine($"  Fecha: {asiento.Fecha}, Importe: {asiento.Importe}, Destino: {asiento.Destino}");
                }
                Console.WriteLine();
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Declarar un objeto LibroDiario
            LibroDiario libroDiario = new LibroDiario();

            do
            {
                // Obtener la cuenta del Debe
                Console.Write("Ingrese la cuenta del Debe: ");
                string cuentaDebeNombre = Console.ReadLine();
                Cuenta cuentaDebe = ObtenerCuenta(libroDiario, cuentaDebeNombre);

                // Obtener el importe del asiento del Debe
                Console.Write("Ingrese el importe del asiento del Debe: ");
                decimal importeDebe = decimal.Parse(Console.ReadLine());

                // Obtener la cuenta del Haber
                Console.Write("Ingrese la cuenta del Haber: ");
                string cuentaHaberNombre = Console.ReadLine();
                Cuenta cuentaHaber = ObtenerCuenta(libroDiario, cuentaHaberNombre);

                // Obtener el importe del asiento del Haber
                Console.Write("Ingrese el importe del asiento del Haber: ");
                decimal importeHaber = decimal.Parse(Console.ReadLine());

                // Obtener la fecha del asiento contable
                Console.Write("Ingrese la fecha del asiento contable (yyyy-MM-dd): ");
                DateTime fechaAsiento = DateTime.Parse(Console.ReadLine());

                // Crear asientos contables y agregarlos a las cuentas
                AsientoContable asientoDebe = new AsientoContable(cuentaDebe.Nombre, importeDebe, "Debe", fechaAsiento);
                cuentaDebe.AgregarAsiento(asientoDebe);

                AsientoContable asientoHaber = new AsientoContable(cuentaHaber.Nombre, importeHaber, "Haber", fechaAsiento);
                cuentaHaber.AgregarAsiento(asientoHaber);

                // Mostrar los asientos del libro diario
                libroDiario.MostrarAsientos();

                // Mostrar el Libro Mayor de cada cuenta
                foreach (Cuenta cuenta in libroDiario.cuentas)
                {
                    cuenta.MostrarLibroMayor();
                }

                // Preguntar si el usuario desea ingresar otro asiento
                Console.Write("¿Desea ingresar otro asiento? (S/N): ");
            } while (Console.ReadLine()?.ToUpper() == "S");

            // Mostrar el registro completo de todas las cuentas
            Console.WriteLine("Registro completo:");
            libroDiario.MostrarAsientos();
        }

        private static Cuenta ObtenerCuenta(LibroDiario libroDiario, string nombre)
        {
            Cuenta cuenta = libroDiario.cuentas.Find(c => c.Nombre == nombre);

            if (cuenta == null)
            {
                cuenta = new Cuenta(nombre);
                libroDiario.AgregarCuenta(cuenta);
            }

            return cuenta;
        }
    }
}
