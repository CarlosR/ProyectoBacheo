using System;
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
        
        public void CrearRollo()
        {
            Console.WriteLine("\nIntroduzca los datos correspondientes.");

            Console.WriteLine("\nSerie: ");
            Serie = Convert.ToInt64(Console.ReadLine());
            Console.WriteLine("\nEstilo: ");
            Estilo = Console.ReadLine();
            Console.WriteLine("\nCantidad: ");
            Cantidad = Convert.ToInt32(Console.ReadLine());
            
            EstadoActual = EstadosDeSerie.NEW;
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
    }

}
