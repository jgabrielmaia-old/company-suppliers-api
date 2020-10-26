using CompanySupplier.Entity.DAL;
using CompanySupplier.Entity.Model;
using CompanySupplier.WebApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace CompanySupplier.WebApi.Repositories
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly CompanySupplierContext _context;

        public CompanyRepository(CompanySupplierContext context)
        {
            _context = context;
        }

        public Company Create(Company company)
        {
            if (company.Validate())
            {
                _context.Add(company);
                _context.SaveChanges();
            }

            return company;
        }

        public bool CompanyExists(string fantasyName) => _context.Companies.Any(c => c.FantasyName == fantasyName);

        public List<Company> GetCompanies() => _context.Companies
                                                    .Include(c => c.Document)
                                                    .Include(c => c.FederativeUnit)
                                                    .OrderBy(c => c.FantasyName)
                                                    .ToList();

        public Company GetCompany(int id) => _context.Companies
                                                        .Where(c => c.CompanyId == id)
                                                        .Include(c => c.Document)
                                                        .Include(c => c.FederativeUnit).FirstOrDefault();

        public void Update(int id, Company companyIn)
        {
            if (companyIn.Validate())
            {
                var produto = _context.Companies.Where(c => c.CompanyId == id).FirstOrDefault();
                produto = companyIn;
                _context.SaveChanges();
            }
        }

        public void Remove(Company companyIn) => _context.Companies.Remove(companyIn);

        public void Remove(int id)
        {
            var toRemove = _context.Companies.Where(company => company.CompanyId == id).FirstOrDefault();

            _context.Remove(toRemove);
            _context.SaveChanges();
        }
    }
}