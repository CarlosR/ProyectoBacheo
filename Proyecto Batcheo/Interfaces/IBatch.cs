using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proyecto_Batcheo.Estados;

namespace Proyecto_Batcheo.Interfaces
{
    public interface IBatch
    {
        long Id { get; set; }
        string Estilo { get; set; }
        int CantidadRequerida { get; set; }
        int CantidadActual { get; set; }
        EstadosDeBatch Estado { get; set; }
        List<IRolloDeTelaCruda> RollosAsignados { get; set; }

        bool CrearBatch();
        void MostrarDatos();
        void MostrarSeriesAsignadas();
        void ProcesoDeBatcheo(IRolloDeTelaCruda rolloDeTelaCruda);
        int[] ValidarEstilo(string estilo);
    }
}
