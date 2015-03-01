using System;
using System.Collections.Generic;
using System.Linq;
using Proyecto_Batcheo.Estados;
using Proyecto_Batcheo.Interfaces;

namespace Proyecto_Batcheo
{
    public class InventarioGeneral:InventarioTelaCruda
    {
        protected static List<IBatch> InventarioDeBatches { get; set; }

        public InventarioGeneral()
        {
            InventarioDeBatches = new List<IBatch>();
        }

        public static List<IBatch> GetInventarioDeBatches()
        {
            return InventarioDeBatches;
        }

        public static void SetInventarioDeBatches(List<IBatch> lista)
        {
            InventarioDeBatches = lista;
        } 

        public override void AgregarRollo(IRolloDeTelaCruda rollo)
        {
            base.AgregarRollo(rollo);
            Console.WriteLine("\nRollo Creado!");
            Console.ReadKey();
        }

        public void AgregarBatch(IBatch batch)
        {
            InventarioDeBatches.Add(batch);
            Console.WriteLine("\nBatch Creado!");
            Console.ReadKey();
        }

        public void ReporteInventarioDeBatches()
        {

            if (InventarioDeBatches.Count != 0)
            {
                var cont = 1;
                Console.WriteLine("\nInventario de Batches");

                foreach (var x in InventarioDeBatches)
                {
                    Console.WriteLine(" ");
                    Console.WriteLine("\n" + cont);
                    x.MostrarDatos();

                    cont++;
                }
            }
            else
            {
                Console.WriteLine("\nInventario Vacio!");
            }
            Console.ReadKey();
        }

        public void ReporteDeBatchPorId()
        {
            Console.WriteLine("\nIngrese el Id del Batch que desea consultar");
            long id = Convert.ToInt64(Console.ReadLine());

            IBatch batch = InventarioDeBatches.Find(x => x.Id == id);
            if (batch != null)
            {
                batch.MostrarDatos();
                batch.MostrarSeriesAsignadas();
                
            }
            else
            {
                Console.WriteLine("\nNo existe el Batch!");
            }
            Console.ReadKey();
            
        }

        public void ReporteGeneral()
        {
            ReporteInventarioDeRollos();
            ReporteInventarioDeBatches();
        }

        public void Batcheo()
        {
            Console.WriteLine("\nIngrese el Id del Batch al que desea asignar");
            long id = Convert.ToInt64(Console.ReadLine());

            IBatch batch = InventarioDeBatches.Find(x => x.Id == id);
            if (batch != null)
            {
                if (batch.Estado == EstadosDeBatch.READY)
                {
                    Console.WriteLine("\nEl Batch ya esta a capacidad!");
                }
                else
                {
                    validarAsignacionTelaCruda(batch);

                }
                
            }
            else
            {
                Console.WriteLine("\nNo existe el Batch!");
            }

            Console.ReadKey();
        }

        private void validarAsignacionTelaCruda(IBatch batch)
        {
            Console.WriteLine("Ingrese la serie del rollo que desea asignar: ");
            long serie = Convert.ToInt64(Console.ReadLine());

            IRolloDeTelaCruda rollo = InventarioDeRollos.Find(x => x.Serie == serie);

            if (rollo != null)
            {
                if (rollo.EstadoActual == EstadosDeSerie.ASSIGNED)
                {
                    Console.WriteLine("\nEl rollo ya esta asignado!");
                }
                else
                {
                    batch.ProcesoDeBatcheo(rollo);

                }

            }
            else
            {
                Console.WriteLine("\nNo existe la serie!");
            }
        }

        public static void CrearRolloParcial(int cantidad, string estilo)
        {
            Console.WriteLine("\nIngrese un nuevo codigo de serie para la cantidad sobrante: ");
            long serie = Convert.ToInt64(Console.ReadKey());

            InventarioDeRollos.Add(new RolloDeTelaCruda()
            {
                Cantidad = cantidad,
                EstadoActual = EstadosDeSerie.NEW,
                Estilo = estilo,
                Serie = serie
            });
        }


    }
}
