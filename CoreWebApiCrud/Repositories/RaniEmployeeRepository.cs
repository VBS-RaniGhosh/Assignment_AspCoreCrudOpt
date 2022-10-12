using DataAccessLayerOne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CoreWebApiCrud.DataContext;

namespace CoreWebApiCrud.Repositories
{
    public class RaniEmployeeRepository : IRaniEmployeeRepository
    {
        private readonly ApplicationDbContext _Context;

        public RaniEmployeeRepository(ApplicationDbContext Context)
        {
            _Context = Context;
        }
        public async Task<RaniEmployee> AddRaniEmployee(RaniEmployee raniEmployee)
        {
            var result = await _Context.RaniEmployees.AddAsync(raniEmployee);
              
            await _Context.SaveChangesAsync();
            return result.Entity;

        }

        public async Task<RaniEmployee> DeleteRaniEmployee(int Id)
        {
            var result = await _Context.RaniEmployees.Where(x => x.Id == Id).FirstOrDefaultAsync();
            if (result != null)
            {
                _Context.RaniEmployees.Remove(result);
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }

        public async Task<RaniEmployee> GetRaniEmployee(int Id)
        {
            return await _Context.RaniEmployees.FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<RaniEmployee>> GetRaniEmployees()
        {
            return await _Context.RaniEmployees.ToListAsync();   
        }

        public async Task<IEnumerable<RaniEmployee>> SearchRaniEmployee(string name)
        {
            IQueryable<RaniEmployee> query = _Context.RaniEmployees;
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(x => x.Name.Contains(name));
            }
            return await query.ToListAsync();
        }

        public async Task<RaniEmployee> UpdateRaniEmployee(RaniEmployee raniEmployee)
        {
            var result = await _Context.RaniEmployees.FirstOrDefaultAsync(x => x.Id == raniEmployee.Id);
            if (result != null)
            {
                result.Name = raniEmployee.Name;
                result.City = raniEmployee.City;
                result.Age = raniEmployee.Age;
                await _Context.SaveChangesAsync();
                return result;
            }
            return null;
        }
    }
}
