using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace herencia2
{
   class gestionEmpleados
    {
       public static void Gestion()
        {
            List<Empleado> empleados = new List<Empleado>
        {
            new EmpleadoAsalariado("Luis", "Ramírez", "123-45-6789", 900m),
            new EmpleadoPorHora( "Gómez", "987-65-4321", 20m, 45),
            new EmpleadoPorComision("Carlos", "Díaz", "111-22-3333", 15000m, 0.05m),
            new EmpleadoAsalariadoPorComision("Laura", "Vega", "222-33-4444", 12000m, 0.04m, 600m)
        };

            bool salir = false;
            while (!salir)
            {
                Console.WriteLine("\n1. Mostrar empleados");
                Console.WriteLine("2. Actualizar datos del empleado");
                Console.WriteLine("3. Salir");
                Console.Write("Seleccione una opción: ");
                int opcion = int.Parse(Console.ReadLine()!);

                switch (opcion)
                {
                    case 1:
                        MostrarEmpleados(empleados);
                        break;
                    case 2:
                        ActualizarEmpleado(empleados);
                        break;
                    case 3:
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción inválida, intente de nuevo.");
                        break;
                }
            }
        }

        static void MostrarEmpleados(List<Empleado> empleados)
        {
            int i = 1;
            foreach (Empleado emp in empleados)
            {
                Console.WriteLine($"\nEmpleado #{i++}:");
                emp.Mostrar();
                Console.WriteLine(new string('-', 30));
            }
        }

        static void ActualizarEmpleado(List<Empleado> empleados)
        {
            Console.WriteLine("\nSeleccione el número del empleado a actualizar:");
            for (int i = 0; i < empleados.Count; i++)
            {
                Console.WriteLine($"{i + 1}. Empleado #{i + 1}");
            }

            if (!int.TryParse(Console.ReadLine(), out int index) || index < 1 || index > empleados.Count)
            {
                Console.WriteLine("Selección inválida.");
                return;
            }

            Empleado emp = empleados[index - 1];
            Console.WriteLine("Datos actuales:");
            emp.Mostrar();

            if (emp is EmpleadoAsalariado ea)
            {
                Console.Write("Ingrese nuevo salario semanal: ");
                if (decimal.TryParse(Console.ReadLine(), out decimal nuevoSalario))
                    ea.actualizarSalario(nuevoSalario);
                else
                    Console.WriteLine("Valor inválido.");
            }
            else if (emp is EmpleadoPorHora eh)
            {
                Console.Write("Ingrese nuevo pago por hora: ");
                bool pagoValido = decimal.TryParse(Console.ReadLine(), out decimal nuevoPago);
                Console.Write("Ingrese nuevas horas trabajadas: ");
                bool horasValidas = int.TryParse(Console.ReadLine(), out int nuevasHoras);

                if (pagoValido && horasValidas)
                    eh.actualizarSalario(nuevoPago, nuevasHoras);
                else
                    Console.WriteLine("Valores inválidos.");
            }
            else if (emp is EmpleadoPorComision ec && !(emp is EmpleadoAsalariadoPorComision))
            {
                Console.Write("Ingrese nuevas ventas brutas: ");
                bool ventasValidas = decimal.TryParse(Console.ReadLine(), out decimal nuevasVentas);
                Console.Write("Ingrese nueva tarifa de comisión (ej. 0.05 para 5%): ");
                bool tarifaValida = decimal.TryParse(Console.ReadLine(), out decimal nuevaTarifa);

                if (ventasValidas && tarifaValida)
                    ec.actualizarSalario(nuevasVentas, nuevaTarifa);
                else
                    Console.WriteLine("Valores inválidos.");
            }
            else if (emp is EmpleadoAsalariadoPorComision eac)
            {
                Console.Write("Ingrese nuevas ventas brutas: ");
                bool ventasValidas = decimal.TryParse(Console.ReadLine(), out decimal nuevasVentas);
                Console.Write("Ingrese nueva tarifa de comisión (ej. 0.04 para 4%): ");
                bool tarifaValida = decimal.TryParse(Console.ReadLine(), out decimal nuevaTarifa);
                Console.Write("Ingrese nuevo salario base: ");
                bool salarioValido = decimal.TryParse(Console.ReadLine(), out decimal nuevoSalarioBase);

               if (ventasValidas && tarifaValida && salarioValido)
                    eac.actualizarSalario(nuevasVentas, nuevaTarifa, nuevoSalarioBase);
                else
                    Console.WriteLine("Valores inválidos.");
            }

            Console.WriteLine("Datos actualizados correctamente.");
        }
    }


}
