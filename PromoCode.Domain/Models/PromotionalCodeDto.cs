using Chrominsky.Utils.Models.Base;
using Newtonsoft.Json;

namespace PromoCode.Domain.Models;

public class PromotionalCodeDto : BaseEntity
{
    /// <summary>
    /// Gets or sets the name of the promotional code.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
    public string? Name { get; set; }

    /// <summary>
    /// Gets or sets the code of the promotional code.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
    public string? Code { get; set; }

    /// <summary>
    /// Gets or sets the remaining number of uses for the promotional code.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
    public int? RemainingUses { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of uses for the promotional code.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
    public int? MaxUses { get; set; }

    /// <summary>
    /// Gets or sets the status of the promotional code.
    /// </summary>
    [JsonProperty(DefaultValueHandling = DefaultValueHandling.IgnoreAndPopulate)]
    public string Status { get; set; }
}