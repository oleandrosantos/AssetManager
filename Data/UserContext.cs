using Microsoft.EntityFrameworkCore;

namespace AssetManager.Data;

public class UserContext :DbContext
{
    public class UserContext(DbContextOptions<UserContext> options): base(options)
    {
        
    }
}