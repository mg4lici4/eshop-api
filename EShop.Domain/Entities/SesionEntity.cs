namespace EShop.Domain.Entities
{
    public class SesionEntity
    {
        public long IdSesion { get; set; }
        public long IdUsuario { get; set; }
        public string Jti { get; set; } = null!;
        public int Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaExpiracion { get; set; }
        public DateTime? FechaActualizacion { get; set; }
    }
}
