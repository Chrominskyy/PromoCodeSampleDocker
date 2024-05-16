using System.Text.Json.Serialization;
using Chrominsky.Utils.Models.Base;
using Newtonsoft.Json;
using PromoCode.Domain.Enums;

namespace PromoCode.Domain.Models;

/// <summary>
/// Represents a DTO for promotional codes.
/// </summary>
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
    [System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverter))]
    public StatusEnum? Status { get; set; }
}