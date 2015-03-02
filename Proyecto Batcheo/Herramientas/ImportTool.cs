using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Proyecto_Batcheo.Estados;
using Proyecto_Batcheo.Interfaces;

namespace Proyecto_Batcheo.Herramientas
{
    public class ImportTool:IFileTool
    {
        public string FileName { get; set; }
        
        public bool AsignarNombre()
        {
            Console.WriteLine("\nPorfavor ingrese la ruta o nombre de su archivo si esta en el root: ");
            FileName = Console.ReadLine();

            if (!File.Exists(FileName))
            {
                Console.WriteLine("\n" + FileName + " no existe!");
                return false;
            }

            return true;
        }

        public void ImportarArchivo()
        {
            if (!AsignarNombre())
            {
                return;
            }

            var fs = new FileStream(FileName, FileMode.Open, FileAccess.Read);

            var r = new BinaryReader(fs);

            var rollo = new List<IRolloDeTelaCruda>();
            var batch = new List<IBatch>();

            int contR = r.ReadInt32();
            int contB = r.ReadInt32();
            var contA = new List<int>();

            for (int i = 0; i < contB; i++)
            {
                contA.Add(r.ReadInt32());
            }

            long lastId = r.ReadInt64();
            long lastSerie = r.ReadInt64();

            for (int i = 0; i<contR ; i++)
            {
                long serie = r.ReadInt64();
                string estilo = r.ReadString();
                int cantidad = r.ReadInt32();
                int estado = r.ReadInt32();

                rollo.Add(new RolloDeTelaCruda()
                {
                    Cantidad=cantidad,
                    EstadoActual = (EstadosDeSerie) estado,
                    Estilo = estilo,
                    Serie = serie
                });
            }

            InventarioTelaCruda.SetInventarioDeRollos(rollo);


            for (int i = 0; i < contB; i++)
            {
                long id = r.ReadInt64();
                string estilo = r.ReadString();
                int cantidadActual = r.ReadInt32();
                int cantidadRequerida = r.ReadInt32();
                int estado = r.ReadInt32();

                batch.Add(new Batch()
                {
                    CantidadActual = cantidadActual,
                    CantidadRequerida = cantidadRequerida,
                    Estado = (EstadosDeBatch)estado,
                    Estilo = estilo,
                    Id = id,
                    RollosAsignados = new List<IRolloDeTelaCruda>()
                });

                for (int p = 0; p < contA.ElementAt(i); p++)
                {
                    long serie = r.ReadInt64();
                    string estilo2 = r.ReadString();
                    int cantidad = r.ReadInt32();
                    int estado2 = r.ReadInt32();

                    batch.ElementAt(i).RollosAsignados.Add(new RolloDeTelaCruda()
                    {
                        Cantidad = cantidad,
                        EstadoActual = (EstadosDeSerie)estado2,
                        Estilo = estilo2,
                        Serie = serie
                    });
                }

            }

            InventarioGeneral.SetInventarioDeBatches(batch);

            InventarioGeneral.SetLastId(lastId);
            InventarioGeneral.SetLastSerie(lastSerie);

            r.Close();
            fs.Close();
            Console.WriteLine("\nEl archivo fue cargado exitosamente!");
            Console.ReadKey();
        }

        
    }
}
