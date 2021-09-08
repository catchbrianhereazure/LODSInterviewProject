namespace LODSInterviewProject.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using LODSInterviewProject.Models;
    using Microsoft.Azure.Cosmos;
    using Microsoft.Azure.Cosmos.Fluent;
    using Microsoft.Extensions.Configuration;

    public class OrganizationService : IOrganizationService
    {
        private Container _container;

        public OrganizationService(
            CosmosClient dbClient,
            string databaseName,
            string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddAsync(Organization item)
        {
            await this._container.CreateItemAsync<Organization>(item, new PartitionKey(item.Id));
        }

        public async Task DeleteAsync(string id)
        {
            await this._container.DeleteItemAsync<Organization>(id, new PartitionKey(id));
        }

        public async Task<Organization> GetAsync(string id)
        {
            try
            {
                ItemResponse<Organization> response = await this._container.ReadItemAsync<Organization>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }

        }

        public async Task<IEnumerable<Organization>> GetAllAsync(string queryString)
        {
            var query = this._container.GetItemQueryIterator<Organization>(new QueryDefinition(queryString));
            List<Organization> results = new List<Organization>();
            while (query.HasMoreResults)
            {
                var response = await query.ReadNextAsync();

                results.AddRange(response.ToList());
            }

            return results;
        }

        public async Task UpdateAsync(string id, Organization item)
        {
            item.User = new Models.User
            {
                FirstName="FName",
                LastName= "LName"
            };

            await this._container.UpsertItemAsync<Organization>(item, new PartitionKey(id));
        }
    }
}
