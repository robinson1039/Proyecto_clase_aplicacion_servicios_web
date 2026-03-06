using System.ComponentModel.DataAnnotations;

namespace WebApplicationApi.DTOs
{
    public class RedDTO
    {
        public int Id { get; set; }
        [Display(Name = "Red Name")]
        [Required(ErrorMessage = "Red Name is required.")]
        public string Nombre { get; set; } = string.Empty;
        [Display(Name = "Red URL")]
        public string? Url { get; set; }
        [Display(Name = "Red Country")]
        public string? Pais { get; set; }
    }
}
