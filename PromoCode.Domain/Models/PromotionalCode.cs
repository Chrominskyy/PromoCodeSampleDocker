using System.ComponentModel.DataAnnotations.Schema;

namespace PromoCode.Domain.Models
{
    [Table("PromotionalCodes")]
    public class PromotionalCode
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int RemainingUses { get; set; }
        public int MaxUses { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}