namespace Turnos.Models
{
    public class MedicoEspecialidad
    {
        public int MedicoID { get; set; }
        public int EspecialidadID { get; set; }
        public Medico? Medico { get; set; }
        public Especialidad? Especialidad { get; set; }
    }
}