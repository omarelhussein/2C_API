namespace _2C_API.Data.DTOs
{
    public abstract class BaseEntityModel
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } 
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime ChangedAt { get; set; } 
        public string ChangedBy { get; set; } = string.Empty;
        public bool IsDeleted { get; set; } 
    }
}
