using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Batcheo.Interfaces;

namespace Proyecto_Batcheo
{
    public class InventarioTelaCruda
    {
        protected static List<IRolloDeTelaCruda> InventarioDeRollos { get; set; }
        protected static long LastSerie { get; set; }

        public InventarioTelaCruda()
        {
            InventarioDeRollos = new List<IRolloDeTelaCruda>();
        }

        public static List<IRolloDeTelaCruda> GetInventarioDeRollos()
        {
            return InventarioDeRollos;
        }

        public static void SetInventarioDeRollos(List<IRolloDeTelaCruda> lista)
        {
            InventarioDeRollos = lista;
        } 

        public virtual void AgregarRollo(IRolloDeTelaCruda rollo)
        {
            InventarioDeRollos.Add(rollo);
        }

        public void ReporteInventarioDeRollos()
        {
            if (InventarioDeRollos.Count != 0)
            {
                var cont = 1;
                Console.WriteLine("\nInventario de Tela Cruda");

                foreach (var x in InventarioDeRollos)
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

    }
}
