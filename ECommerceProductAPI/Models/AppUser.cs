namespace ECommerceProductAPI.Models
{
    public class AppUser         : Microsoft.AspNetCore.Identity.IdentityUser
    {
        public  string FullName { get; set; } = string.Empty;
    }
}
