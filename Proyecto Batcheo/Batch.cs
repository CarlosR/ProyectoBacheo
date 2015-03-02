using System;
using System.Collections.Generic;
using System.Linq;
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
        
        public bool CrearBatch()
        {
            Console.WriteLine("\nIntroduzca los datos correspondientes.");

            Console.WriteLine("\nId: ");
            Id = InventarioGeneral.GetLastId() + 1;
            InventarioGeneral.SetLastId(Id);
            Console.WriteLine(Id);

            Console.WriteLine("\nNumero de Estilo y Cilindro: (Ej. 2231,10)");
            string estilo = Console.ReadLine();
            int[] estiloInts = ValidarEstilo(estilo);
            if (estiloInts != null)
            {
                Estilo = string.Format("GC.{0}..{1}", estiloInts[0], estiloInts[1]);
            }
            else
            {
                InventarioGeneral.SetLastId(Id-1);
                return false;
            }

            Console.WriteLine("\nCantidad Requerida: ");
            CantidadRequerida = Convert.ToInt32(Console.ReadLine());
            
            CantidadActual = 0;

            Estado = EstadosDeBatch.NEW;

            RollosAsignados = new List<IRolloDeTelaCruda>();
            return true;
        }

        public void MostrarDatos()
        {
            Console.Write("\nId: ");
            Console.WriteLine(Id);
            Console.Write("\nEstilo: ");
            Console.Write(Estilo);
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

        public int[] ValidarEstilo(string estilo)
        {
            string[] numeroDeEstilo = estilo.Split(',');


            if (numeroDeEstilo.Count() == 2)
            {
                int r1, r2;

                if (Int32.TryParse(numeroDeEstilo.ElementAt(0), out r1) && Int32.TryParse(numeroDeEstilo.ElementAt(1), out r2))
                {
                    return new[] { r1, r2 };
                }
            }

            return null;

        }
    }
}
