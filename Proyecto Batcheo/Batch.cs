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
                Console.WriteLine(" ");
                Console.WriteLine("\nRollos Asignados: ");
                Console.WriteLine(" ");

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
                Console.WriteLine(" ");
            }
        }


        public void AsignarTelaCrudaCompleta(IRolloDeTelaCruda rolloDeTelaCruda)
        {
            rolloDeTelaCruda.CambiarEstado(EstadosDeSerie.ASSIGNED);
            CantidadActual += rolloDeTelaCruda.Cantidad;
            RollosAsignados.Add(rolloDeTelaCruda);
            Console.WriteLine("\nSerie asignada a batch!");

            CambiarEstado();

        }

        public void ProcesoDeBatcheo(IRolloDeTelaCruda rolloDeTelaCruda)
        {
            if (rolloDeTelaCruda.Estilo == Estilo && rolloDeTelaCruda.EstadoActual == EstadosDeSerie.NEW)
            {
                int cantidadPendiente = CantidadRequerida - CantidadActual;
                
                if (rolloDeTelaCruda.Cantidad <= cantidadPendiente)
                {
                    AsignarTelaCrudaCompleta(rolloDeTelaCruda);

                }else if (rolloDeTelaCruda.Cantidad > cantidadPendiente)
                {
                    AsignarTelaCrudaParcial(rolloDeTelaCruda);
                }
                
            }
            else if (rolloDeTelaCruda.Estilo != Estilo)
            {
                Console.WriteLine("\nEl Estilo es incorrecto!");

            }
            else if (rolloDeTelaCruda.EstadoActual == EstadosDeSerie.ASSIGNED)
            {
                Console.WriteLine("\nEsta serie ya esta asignada!");
            }
            
            
        }

        private void AsignarTelaCrudaParcial(IRolloDeTelaCruda rolloDeTelaCruda)
        {
            rolloDeTelaCruda.CambiarEstado(EstadosDeSerie.ASSIGNED);
            int cantidadSobrante = rolloDeTelaCruda.Cantidad - (CantidadRequerida - CantidadActual);
            
            rolloDeTelaCruda.Cantidad -= cantidadSobrante;
            RollosAsignados.Add(rolloDeTelaCruda);

            CantidadActual = CantidadRequerida;
            CambiarEstado();
            Console.WriteLine("\nSerie asignada a batch!");

            InventarioGeneral.CrearRolloParcial(cantidadSobrante,rolloDeTelaCruda.Estilo);


        }

        public void CambiarEstado()
        {
            if (CantidadActual == CantidadRequerida)
            {
                Estado = EstadosDeBatch.READY;

            }
            else if (CantidadActual < CantidadRequerida && Estado == EstadosDeBatch.NEW)
            {
                Estado = EstadosDeBatch.PENDING;
            }
        }
    }
}
