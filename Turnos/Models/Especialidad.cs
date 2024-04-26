using System.ComponentModel.DataAnnotations;

namespace Turnos.Models
{
    public class Especialidad
    {
        [Key] //De esta manera le especifico a entity framework que este campo es primary key
        public int EspecialidadID { get; set; }
        public string Descripcion { get; set; } = "";
    }
}
