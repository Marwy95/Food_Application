using Food_Application.Enums;

namespace Food_Application.Models
{
    public class UserRoles :BaseModel
    {
         public int UserID { get; set; }
        public int RecipeID { get; set; }
        public User User { get; set; }
        public Recipe Recipe { get; set; }
        public Role Role { get; set; }  
    }
}
