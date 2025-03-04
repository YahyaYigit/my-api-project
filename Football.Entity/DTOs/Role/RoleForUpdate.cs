namespace Basketball.Entity.DTOs.Role
{
    public class RoleForUpdate
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
