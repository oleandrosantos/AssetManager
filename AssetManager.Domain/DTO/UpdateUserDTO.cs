using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace AssetManager.Domain.DTO;
public class UpdateUserDTO
{
    public string? Name { get; set; }
    public string? Password { get; set; }
    [Required]
    public string Email { get; set; }
    public string? Role { get; set; }
    [DefaultValue(true)]
    public bool? IsActive { get; set; }
}
