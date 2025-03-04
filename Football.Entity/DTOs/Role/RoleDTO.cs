namespace Basketball.Entity.DTOs.Role
{
    public class RoleDto
    {
        public int Id { get; set; }  // Role Id de olmalı
        public string Name { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
