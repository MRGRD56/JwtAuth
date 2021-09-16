using System.ComponentModel.DataAnnotations;

namespace Mrgrd56.JwtAuth.TestApp.Models
{
    public record UserCredentials(
        [Required(AllowEmptyStrings = false)]
        string Login,
        [Required(AllowEmptyStrings = false)]
        string Password);
}