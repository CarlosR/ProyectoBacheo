using System;
using System.Collections.Generic;
using System.Linq;
using Proyecto_Batcheo.Estados;
using Proyecto_Batcheo.Interfaces;

namespace Proyecto_Batcheo
{
    public class RolloDeTelaCruda:IRolloDeTelaCruda
    {
        public long Serie { get; set; }
        public string Estilo { get; set; }
        public int Cantidad { get; set; }
        public EstadosDeSerie EstadoActual { get; set; }
        
        public bool CrearRollo()
        {
            Console.WriteLine("\nIntroduzca los datos correspondientes.");

            Console.WriteLine("\nSerie: ");
            Serie = InventarioGeneral.GetLastSerie() + 1;
            InventarioGeneral.SetLastSerie(Serie);
            Console.WriteLine(Serie);

            Console.WriteLine("\nNumero de Estilo y Cilindro: (Ej. 2231,10)");
            string estilo = Console.ReadLine();
            int[] estiloInts = ValidarEstilo(estilo);
            if (estiloInts != null)
            {
                Estilo = string.Format("GC.{0}..{1}", estiloInts[0], estiloInts[1]);
            }
            else
            {
                InventarioGeneral.SetLastSerie(Serie - 1);
                return false;
            }

            Console.WriteLine("\nCantidad: ");
            Cantidad = Convert.ToInt32(Console.ReadLine());
            
            EstadoActual = EstadosDeSerie.NEW;
            return true;
        }

        public void MostrarDatos()
        {
            Console.Write("\nSerie: ");
            Console.WriteLine(Serie);
            Console.Write("\nEstilo: ");
            Console.WriteLine(Estilo);
            Console.Write("\nCantidad: ");
            Console.WriteLine(Cantidad + " lbs");
            Console.Write("\nEstado De Serie:  ");
            Console.WriteLine(EstadoActual);
        }

        public void ModificarSerie()
        {
            throw new NotImplementedException();
        }


        public void CambiarEstado(EstadosDeSerie estado)
        {
            EstadoActual = estado;
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
