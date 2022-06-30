using System;
using System.Data;
using DataTablePrettyPrinter;//NuGet Install-Package DataTablePrettyPrinter -Version 0.2.0

namespace AmortizationCalculator
{
    public class Program
    {
        static void Main(string[] args)
        {
            Calculadora();
        }
        public static void Calculadora()
        {
            try
            {
                int plazos = 0;
                decimal monto = 0,
                    tasainteres = 0,
                    aval = 0,
                    saldoInicial = 0,
                    cuota = 0,
                    saldoFinal = 0,
                    interes = 0,
                    amortizacion = 0,
                    cuotaFinal = 0;

                Console.WriteLine("Ingrese el valor del monto del prestamo: ");
                monto = Convert.ToDecimal(Console.ReadLine());

                Console.WriteLine("Ingrese el porcentaje del interés del prestamo: ");
                tasainteres = Convert.ToDecimal(Console.ReadLine());
                tasainteres = tasainteres / 100;

                Console.WriteLine("Ingrese el número de cuotas a diferir el pago: ");
                plazos = Convert.ToInt16(Console.ReadLine());

                Console.WriteLine("Ingrese el porcentaje del valor del aval: ");
                aval = Convert.ToDecimal(Console.ReadLine());
                aval =
                    Math.Round((monto * (aval / 100)) / plazos, 2);

                saldoInicial = monto;

                //(CREACION DE LA TABLA)
                // Se tiene que crear primero la columna asignandole Nombre y Tipo de datos
                DataTable table = new DataTable();
                table.TableName = "Amortización";

                table.Columns.Add("Periodo", typeof(int));
                table.Columns.Add("Saldo Inicial", typeof(decimal));
                table.Columns.Add("Interés", typeof(decimal));
                table.Columns.Add("Abono a capital", typeof(decimal));
                table.Columns.Add("Cuota Fija", typeof(decimal));
                table.Columns.Add("Aval", typeof(decimal));
                table.Columns.Add("Saldo Final", typeof(decimal));


                if (monto != 0 && plazos != 0 && tasainteres != 0)
                {
                    cuota = (monto * (decimal)(Math.Pow(1 + (double)tasainteres, plazos) * (double)tasainteres) / (decimal)(Math.Pow(1 + (double)tasainteres, plazos) - 1));

                    for (int i = 1; i <= plazos; i++)
                    {
                        interes =
                            Math.Round(saldoInicial * tasainteres, 2);
                        cuotaFinal =
                             Math.Round(cuota + aval, 2);
                        amortizacion =
                            Math.Round(cuota - interes, 2);
                        saldoFinal =
                            Math.Round(saldoInicial - amortizacion, 2);

                        if (saldoFinal < 0.5m)
                        {
                            saldoFinal = 0;
                        }

                        table.Rows.Add(i, saldoInicial, interes, amortizacion, cuotaFinal, aval, saldoFinal);

                        saldoInicial = saldoFinal;

                    }
                    Console.WriteLine(table.ToPrettyPrintedString());
                }
                else
                {
                    Console.WriteLine("No deje en cero los campos requeridos.");
                    Calculadora();
                }
            }
            catch
            {
                Console.WriteLine("Ingrese valores númericos y no deje vacío los campos requeridos.");
                Calculadora();
            }
            
        }       
    }
}