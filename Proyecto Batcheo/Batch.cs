using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Batcheo.Estados;
using Proyecto_Batcheo.Interfaces;

namespace Proyecto_Batcheo
{
    class Batch:IBatch
    {
        public long Id { get; set; }
        public string Estilo { get; set; }
        public int CantidadRequerida { get; set; }
        public int CantidadActual { get; set; }
        public EstadosDeBatch Estado { get; set; }
        public List<IRolloDeTelaCruda> RollosAsignados { get; set; }
        
        public void CrearBatch()
        {
            Console.WriteLine("\nIntroduzca los datos correspondientes.");

            Console.WriteLine("\nId: ");
            Id = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("\nEstilo: ");
            Estilo = Console.ReadLine();
            Console.WriteLine("\nCantidad Requerida: ");
            CantidadRequerida = Convert.ToInt32(Console.ReadLine());
            
            CantidadActual = 0;

            Estado = EstadosDeBatch.NEW;

            RollosAsignados = new List<IRolloDeTelaCruda>();
        }

        public void MostrarDatos()
        {
            Console.Write("\nId: ");
            Console.WriteLine(Id);
            Console.Write("\nEstilo: ");
            Console.WriteLine(Estilo);
            Console.Write("\nCantidad Requerida: ");
            Console.WriteLine(CantidadRequerida + " lbs");
            Console.Write("\nCantidad Actual: ");
            Console.WriteLine(CantidadActual + " lbs");
            Console.Write("\nEstado De Batch:  ");
            Console.WriteLine(Estado);
        }

        public void MostrarSeriesAsignadas()
        {
            if (RollosAsignados.Count != 0)
            {
                Console.WriteLine("\nRollos Asignados: ");

                foreach (var rollo in RollosAsignados)
                {
                    Console.Write("\nSerie: ");
                    Console.WriteLine(rollo.Serie);
                    Console.Write("\nCantidad: ");
                    Console.WriteLine(rollo.Cantidad);
                }
                
            }
            else
            {
                Console.WriteLine("\nSin rollos asignados.");
            }
        }
    }
}
