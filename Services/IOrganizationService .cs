namespace LODSInterviewProject
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using LODSInterviewProject.Models;

    public interface IOrganizationService
    {
        Task<IEnumerable<Organization>> GetAllAsync(string query);
        Task<Organization> GetAsync(string id);
        Task AddAsync(Organization item);
        Task UpdateAsync(string id, Organization item);
        Task DeleteAsync(string id);
        Task AddUser(string id, Organization item, LODSInterviewProject.Models.User user);
        Task RemoveUser(string id, Organization item, LODSInterviewProject.Models.User user);
    }
}
