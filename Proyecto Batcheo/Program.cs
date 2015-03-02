using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Batcheo.Estados;
using Proyecto_Batcheo.Herramientas;

namespace Proyecto_Batcheo
{
    class Program
    {
        static void Main(string[] args)
        {
            InventarioGeneral.SetLastId(1);
            InventarioGeneral.SetLastSerie(100);
            var inventario = new InventarioGeneral();
            Menu(inventario);

        }

        static void Menu(InventarioGeneral inventario)
        {
            int opcion;

            do
            {
                Console.WriteLine(" ");
                Console.WriteLine("\n---------------Bienvenido al Sistema de Bacheo---------------");
                Console.WriteLine("\n1. Crear rollos de tela cruda");
                Console.WriteLine("2. Consultar inventario de tela cruda");
                Console.WriteLine("3. Crear batch");
                Console.WriteLine("4. Consulta general de batches");
                Console.WriteLine("5. Consultar batch por id");
                Console.WriteLine("6. Batcheo");
                Console.WriteLine("7. Cargar archivo de datos");
                Console.WriteLine("8. Exportar a archivo");
                Console.WriteLine("9. Limpiar Pantalla");
                Console.WriteLine("10. Salir");

                Console.WriteLine("\nIngrese la opcion que desee realizar:");
                opcion = Convert.ToInt32(Console.ReadLine());
                string seguir = "";

                switch (opcion)
                {
                    case 1:
                             
                        do
                        {
                            var rollo = new RolloDeTelaCruda();
                            if (!rollo.CrearRollo())
                            {
                                Console.WriteLine("Error al momento de ingresar, intentelo nuevamente");
                                continue;
                            }
                            inventario.AgregarRollo(rollo);
                            Console.WriteLine("\nDesea ingresar otro? S | s");
                            seguir = Console.ReadLine();

                        } while (seguir=="s" || seguir=="S");
                        
                        
                        break;
                
                    case 2:
                        inventario.ReporteInventarioDeRollos();
                        break;

                    case 3:
                        
                        do
                        {
                            var batch = new Batch();
                            if (!batch.CrearBatch())
                            {
                                Console.WriteLine("Error al momento de ingresar, intentelo nuevamente");
                                continue;
                            }
                            inventario.AgregarBatch(batch);
                            Console.WriteLine("\nDesea ingresar otro? S | s");
                            seguir = Console.ReadLine();

                        } while (seguir == "s" || seguir == "S");
                        
                        break;

                    case 4:
                        inventario.ReporteInventarioDeBatches();
                        break;
                    case 5:
                        inventario.ReporteDeBatchPorId();
                        break;
                    case 6:
                        inventario.Batcheo();
                        break;
                    case 7:
                        var imp = new ImportTool();
                        imp.ImportarArchivo();
                        break;
                    case 8:
                        var exp = new ExportTool();
                        exp.ExportarArchivo();
                        break;
                    case 9:
                        Console.Clear();
                        break;
                }

            } while (opcion != 10);
        }
    }
}
