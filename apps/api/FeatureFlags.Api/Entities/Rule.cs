namespace FeatureFlags.Api.Entities
{
    public class Rule
    {
        public Guid Id { get; set; }
        public Guid FeatureId { get; set; }
        public Guid EnvironmetId { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Conditions { get; set; } = string.Empty;
        public bool Value { get; set; }
        public int Priority { get; set; }
        public DateTime CreatedAt { get; set; }
        
        public Feature Feature { get; set; } = null!;
        public Environment Environment { get; set; } = null!;
    }
}