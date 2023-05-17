namespace Domain.Entities
{
    public class Chemical : BaseEntity
    {
        public string ChemicalType { get; set; }

        public int PreHarvestIntervalInDays { get; set; }

        public string ActiveIngredient { get; set; }

        public string Name { get; set; }
    }
}
