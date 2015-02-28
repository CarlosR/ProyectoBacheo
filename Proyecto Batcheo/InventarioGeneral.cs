using System;
using System.Collections.Generic;
using System.Linq;
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
        }

        public void ReporteDeBatchPorId()
        {
            Console.WriteLine("\nIngrese el id del Batch que desea consultar");
            long id = Convert.ToInt64(Console.ReadLine());

            IBatch batch = InventarioDeBatches.Find(x => x.Id == id);
            if (batch != null)
            {
                batch.MostrarDatos();
                batch.MostrarSeriesAsignadas();
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("\nNo existe el Batch!");
            }
            
        }

        public void ReporteGeneral()
        {
            ReporteInventarioDeRollos();
            ReporteInventarioDeBatches();
        }

       
    }
}
