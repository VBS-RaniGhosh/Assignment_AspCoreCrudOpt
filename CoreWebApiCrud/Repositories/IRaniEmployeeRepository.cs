using DataAccessLayerOne;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWebApiCrud.Repositories
{
    public interface IRaniEmployeeRepository
    {
        Task<IEnumerable<RaniEmployee>> SearchRaniEmployee(string name);
        Task<IEnumerable<RaniEmployee>> GetRaniEmployees();
        Task<RaniEmployee> GetRaniEmployee(int Id);
        Task<RaniEmployee> AddRaniEmployee(RaniEmployee raniEmployee);
        Task<RaniEmployee> UpdateRaniEmployee(RaniEmployee raniEmployee);
        Task<RaniEmployee> DeleteRaniEmployee(int Id);



    }
}
