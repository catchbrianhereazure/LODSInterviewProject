namespace LODSInterviewProject.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LODSInterviewProject.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;

    public class UserService : IUserService
    {
        private readonly Container _container;
        private readonly IEmailServiceProvider _emailServiceProvider;

        public UserService(
            CosmosClient dbClient,
            IEmailServiceProvider emailServiceProvider,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
            _emailServiceProvider = emailServiceProvider;
        }

        public async Task AddAsync(LODSInterviewProject.Models.User item)
        {
            await this._container.CreateItemAsync<LODSInterviewProject.Models.User>(item, new PartitionKey(item.Id));
            await _emailServiceProvider.Execute(item.Email, string.Format("{0} {1}", item.FirstName, item.LastName));
        }

        public async Task DeleteAsync(string id)
        {
            await this._container.DeleteItemAsync<LODSInterviewProject.Models.User>(id, new PartitionKey(id));
        }

        public async Task<LODSInterviewProject.Models.User> GetAsync(string id)
        {
            try
            {
                ItemResponse<LODSInterviewProject.Models.User> response = await this._container.ReadItemAsync<LODSInterviewProject.Models.User>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<LODSInterviewProject.Models.User>> GetAllAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<LODSInterviewProject.Models.User>(new QueryDefinition(queryString));
            List<LODSInterviewProject.Models.User> results = new List<LODSInterviewProject.Models.User>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateAsync(string id, LODSInterviewProject.Models.User item)
        {
            await this._container.UpsertItemAsync<LODSInterviewProject.Models.User>(item, new PartitionKey(id));
        }

    }
}
