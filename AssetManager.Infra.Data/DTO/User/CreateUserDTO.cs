using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManager.Infra.Data.DTO.User;
public class CreateUserDTO
{
    public string? Name { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string? Role { get; set; }
    public int IdCompany { get; set; }
}
