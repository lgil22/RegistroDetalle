using System.ComponentModel.DataAnnotations;

namespace RegistroDetails.Entidades
{
    public class TelefonoDetalles
    {
        [Key]
        public int Id { get; set; }
     //   public int PersonaId { get; set; }
        public string TipoTelefono { get; set; }
        public string Telefono { get; set; }

        public TelefonoDetalles(int id, string telefono, string tipoTelefono)
        {
            Id = 0;
           // PersonaId = 0;
            TipoTelefono = string.Empty;
            Telefono = string.Empty;
        }

 
        //Constructor Con parametros con ID
        public TelefonoDetalles(int id, int idPersona, string tipoTelefono, string telefono)
        {
            Id = id;
            //PersonaId = idPersona;
            TipoTelefono = tipoTelefono;
            Telefono = telefono;
        }


        
    }
}
