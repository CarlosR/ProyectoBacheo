using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Batcheo.Estados;

namespace Proyecto_Batcheo.Interfaces
{
    public interface IRolloDeTelaCruda
    {
        long Serie { get; set; }
        string Estilo { get; set; }
        int Cantidad { get; set; }
        EstadosDeSerie EstadoActual { get; set; }

        void CrearRollo();
        void MostrarDatos();
        void ModificarSerie();
        void CambiarEstado(EstadosDeSerie estado);

    }
}
