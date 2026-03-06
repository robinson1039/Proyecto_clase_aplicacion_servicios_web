using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationApi.Data.Entities
{
    public class Red
    {
        [Key]
        [Column("idr")]
        public int Idr { get; set; }

        [Required]
        [Column("nombre")]
        public string Nombre { get; set; } = string.Empty;

        [Column("url")]
        public string? Url { get; set; }

        [Column("pais")]
        public string? Pais { get; set; }
    }
}