using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AssetManager.ViewModel
{
  public class UpdateUserViewModel
  {
    public string? name { get; set; }
    public string? password { get; set; }
    [Required]
    public string email { get; set; }
    public string? role { get; set; }
    [DefaultValue(true)]
    public bool? isActive { get; set; }
  }
}