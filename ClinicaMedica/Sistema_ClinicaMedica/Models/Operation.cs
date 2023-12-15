namespace Sistema_ClinicaMedica.Models
{
    public class Operation {
        public Operation()
        {
            esValida = false;
            Mensaje = "";
        }
        public bool esValida { get; set; }
        public string Mensaje { get; set; }
        public object resultado { get; set; }
    }
}
