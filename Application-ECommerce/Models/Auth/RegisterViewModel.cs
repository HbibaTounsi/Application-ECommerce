using System.ComponentModel.DataAnnotations;

namespace Application_ECommerce.Models.Auth
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "L'adresse email est requise.")]
        [EmailAddress(ErrorMessage = "Format d'email invalide.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Le numéro de téléphone est requis.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Le mot de passe est requis.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string? Role { get; set; }

        [Required(ErrorMessage = "L'adresse est requise.")]
        [StringLength(255, ErrorMessage = "L'adresse ne peut pas dépasser 255 caractères.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Le code postal est requis.")]
        [Range(1000, 99999, ErrorMessage = "Le code postal doit être entre 1000 et 99999.")]
        public int ZipCode { get; set; }
    }
}
