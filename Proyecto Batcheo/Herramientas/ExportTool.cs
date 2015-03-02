using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Proyecto_Batcheo.Interfaces;

namespace Proyecto_Batcheo.Herramientas
{
    public class ExportTool:IFileTool
    {
        public string FileName { get; set; }

        public bool AsignarNombre()
        {
            Console.WriteLine("\nComo desea que se llame su archivo? ");
            FileName = Console.ReadLine();
            
            if (File.Exists(FileName))
            {
                Console.WriteLine("\n" + FileName + " ya existe!");
                return false;
            }

            return true;
        }

        public void ExportarArchivo()
        {
            if (!AsignarNombre())
            {
                return;
            }


            var fs = new FileStream(FileName, FileMode.CreateNew);

            var w = new BinaryWriter(fs);

            int contR = InventarioGeneral.GetInventarioDeRollos().Count;
            int contB = InventarioGeneral.GetInventarioDeBatches().Count;
            var contA = InventarioGeneral.GetInventarioDeBatches().Select(inv => inv.RollosAsignados.Count).ToList();
            long lastId = InventarioGeneral.GetLastId();
            long lastSerie = InventarioGeneral.GetLastSerie();

            w.Write(contR);
            w.Write(contB);
            foreach (var i in contA)
            {
                w.Write(i);
            }
            w.Write(lastId);
            w.Write(lastSerie);

            foreach (var inv in InventarioTelaCruda.GetInventarioDeRollos())
            {
                w.Write(inv.Serie);
                w.Write(inv.Estilo);
                w.Write(inv.Cantidad);
                w.Write((int)inv.EstadoActual);

            }

            foreach (var inv in InventarioGeneral.GetInventarioDeBatches())
            {
                w.Write(inv.Id);
                w.Write(inv.Estilo);
                w.Write(inv.CantidadActual);
                w.Write(inv.CantidadRequerida);
                w.Write((int)inv.Estado);

                foreach (var rollo in inv.RollosAsignados)
                {
                    w.Write(rollo.Serie);
                    w.Write(rollo.Estilo);
                    w.Write(rollo.Cantidad);
                    w.Write((int)rollo.EstadoActual);

                }


            }

            w.Close();
            fs.Close();
            Console.WriteLine("\nArchivo guardado con exito!");
            Console.ReadKey();
        
        }
        
    }
}
