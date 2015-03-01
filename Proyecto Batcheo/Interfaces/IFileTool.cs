using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Batcheo.Interfaces
{
    public interface IFileTool
    {
        string FileName { get; set; }

        bool AsignarNombre();
    }
}
