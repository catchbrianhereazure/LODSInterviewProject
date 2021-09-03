namespace LODSInterviewProject.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LODSInterviewProject.Models;

    public interface IUserService
    {
        Task<IEnumerable<LODSInterviewProject.Models.User>> GetAllAsync(string query);
        Task<LODSInterviewProject.Models.User> GetAsync(string id);
        Task AddAsync(LODSInterviewProject.Models.User item);
        Task UpdateAsync(string id, LODSInterviewProject.Models.User item);
        Task DeleteAsync(string id);
    }
}
