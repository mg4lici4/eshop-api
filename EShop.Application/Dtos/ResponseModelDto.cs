using EShop.Application.Helpers;
using System.Text.Json.Serialization;

namespace EShop.Application.Dtos
{
    public class ResponseModelDto
    {
        public object Datos { get; set; }
        public string Mensaje { get; set; } = string.Empty;

        [JsonConstructor]
        public ResponseModelDto(object datos, string mensaje)
        {
            Datos = datos;
            Mensaje = mensaje;
        }

        public ResponseModelDto(object datos)
        {
            Datos = datos;
            Mensaje = MensajesHelper.OPERACION_CORRECTA;
        }
        public ResponseModelDto(string mensaje)
        {
            Mensaje = mensaje;
            Datos = null!;
        }
    }
}
