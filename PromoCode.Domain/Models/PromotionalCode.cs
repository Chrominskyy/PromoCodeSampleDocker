using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Chrominsky.Utils.Models.Base;
using PromoCode.Domain.Enums;

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
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public StatusEnum Status { get; set; }
    }
}