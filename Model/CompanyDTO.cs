using CompanySupplier.Entity.Enums;

namespace CompanySupplier.WebApi.Model
{
    public class CompanyDTO
    {
        public string FantasyName { get; set; }
 
        public string DocumentValue { get; set; }

        public EDocumentType DocumentType { get; set; }

        public string FederativeUnit { get; set; }
    }
}
