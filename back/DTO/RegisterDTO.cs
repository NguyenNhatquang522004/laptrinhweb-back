using System.ComponentModel.DataAnnotations;
using static backapi.Enums.enums;

namespace backapi.DTO
{
    public class RegisterDTO
    {

        [Required(ErrorMessage = "Email là bắt buộc")]
        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; } = string.Empty;


        public string Username { get; set; } = string.Empty;

        public string prvider { get; set; } = "Local"; // Google, Facebook, etc.

        public string providerId { get; set; } = string.Empty; // Unique ID from the provider
        public bool EmailVerified { get; set; }

        public string? PhoneNumber { get; set; }
        public DateTime? LastLoginDate { get; set; }

        public string PasswordHash { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public DateTime CodeExpiration { get; set; }


        public string? FirstName { get; set; }


        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender? Gender { get; set; }


        public string? NativeLanguage { get; set; }

        public string? Country { get; set; }


        public string? Timezone { get; set; }

        public string? ProfileImageUrl { get; set; }

        public string? Bio { get; set; }

        public bool IsPremium { get; set; } = false;

        public CefrLevel CurrentLevel { get; set; } = CefrLevel.A1;


        public bool IsActive { get; set; } = true;



    }
}
