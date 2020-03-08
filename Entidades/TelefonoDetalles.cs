using System.ComponentModel.DataAnnotations;

namespace RegistroDetails.Entidades
{
    public class TelefonoDetalles
    {
        [Key]
        public int Id { get; set; }
     //  public int PersonaId { get; set; }
        public string TipoTelefono { get; set; }
        public string Telefono { get; set; }

        public TelefonoDetalles() //int id, string telefono, string tipoTelefono)
        {

            Id = 0;
           // PersonaId = 0;
            TipoTelefono = string.Empty;
            Telefono = string.Empty;
        }

        
    }
}
