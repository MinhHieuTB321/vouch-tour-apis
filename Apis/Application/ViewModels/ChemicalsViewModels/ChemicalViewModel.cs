namespace Application.ViewModels.ChemicalsViewModels
{
    public class ChemicalViewModel
    {
        public string _Id { get; set; }

        public string ChemicalType { get; set; }

        public int PreHarvestIntervalInDays { get; set; }

        public string ActiveIngredient { get; set; }

        public string Name { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime? ModificationDate { get; set; }

        public DateTime? DeletionDate { get; set; }
    }
}
