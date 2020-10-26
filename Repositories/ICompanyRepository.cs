using CompanySupplier.Entity.Model;
using System.Collections.Generic;

namespace CompanySupplier.WebApi.Repositories
{
    public interface ICompanyRepository
    {
        public bool CompanyExists(int id);

        public Company Create(Company company);

        public List<Company> GetCompanies();

        public Company GetCompany(int id);

        public void Update(int id, Company companyIn);

        public void Remove(Company companyIn);

        public void Remove(int id);
    }
}