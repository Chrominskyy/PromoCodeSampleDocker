using System.ComponentModel.DataAnnotations;

namespace PromoCode.Domain.Models;

public class PromotionalCodeDto
{
    [Required]
    public Guid Id { get; set; }
        
    [Required]
    public string Name { get; set; }
        
    [Required]
    public string Code { get; set; }
    public int RemainingUses { get; set; }
    public int MaxUses { get; set; }
    public bool IsActive { get; set; }
}