using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
using Chrominsky.Utils.Models.Base;

namespace PromoCode.Domain.Models
{
    /// <summary>
    /// Represents a promotional code in the system.
    /// </summary>
    [Table("PromotionalCodes")]
    public class PromotionalCode : BaseDatabaseEntity
    {
        /// <summary>
        /// Gets or sets the name of the promotional code.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the code of the promotional code.
        /// </summary>
        [Required]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the remaining number of uses for the promotional code.
        /// </summary>
        [Required]
        public int? RemainingUses { get; set; }

        /// <summary>
        /// Gets or sets the maximum number of uses for the promotional code.
        /// </summary>
        [Required]
        public int? MaxUses { get; set; }

        /// <summary>
        /// Gets or sets the status of the promotional code.
        /// </summary>
        [Required]
        public string Status { get; set; }
    }
}