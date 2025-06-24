namespace Model.Entities
{
    public class RefreshToken : BaseEntity
    {
        public string Value { get; set; }
        public DateTime DateCreate { get; set; } = DateTime.UtcNow;
        public DateTime DateUpdate { get; set; } 
        public DateTime DateExpire { get; set; } = DateTime.UtcNow.AddMonths(1);
        public bool IsActive { get; set; }
        public User User { get; set; }
    }
}
