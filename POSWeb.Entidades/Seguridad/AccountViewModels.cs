#region Using
    using System.ComponentModel.DataAnnotations;
#endregion

namespace POSWeb.Entidades
{
    public class AccountLoginModel
    {
        [Required]
        public string Usuario { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public string IP { get; set; }

        public string ReturnUrl { get; set; }
        public bool RememberMe { get; set; }
    }

    public class AccountForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }

    public class AccountResetPasswordModel
    {
        [Required(ErrorMessage = "El dato Contraseña es requerido")]
        [DataType(DataType.Password)]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "Password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "El dato Confirmar Contraseña es requerido")]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [Display(ResourceType = typeof(ResourceFiles.lblViews), Name = "PasswordConfirm")]
        public string PasswordConfirm { get; set; }
    }

    public class AccountRegistrationModel
    {
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [Compare("Email")]
        public string EmailConfirm { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password")]
        public string PasswordConfirm { get; set; }
    }
}