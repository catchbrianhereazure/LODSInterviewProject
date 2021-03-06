namespace LODSInterviewProject
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using LODSInterviewProject.Services;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // <ConfigureServices> 
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddSingleton<IOrganizationService>(InitializeOrganizationClientInstanceAsync(Configuration.GetSection("Organization")).GetAwaiter().GetResult());
            services.AddSingleton<IUserService>(InitializeUserClientInstanceAsync(Configuration.GetSection("User")).GetAwaiter().GetResult());
        }
        // </ConfigureServices> 

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Organization}/{action=Index}/{id?}");
            });
        }

        // <InitializeOrganizationClientInstanceAsync>        
        /// <summary>
        /// Creates a Cosmos DB database and a container with the specified partition key. 
        /// </summary>
        /// <returns></returns>
        private static async Task<OrganizationService> InitializeOrganizationClientInstanceAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);
            OrganizationService organizationService = new OrganizationService(client, databaseName, containerName);
            Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return organizationService;
        }
        // </InitializeOrganizationClientInstanceAsync>

        // <InitializeUserClientInstanceAsync>        
        /// <summary>
        /// Creates a Cosmos DB database and a container with the specified partition key. 
        /// </summary>
        /// <returns></returns>
        private static async Task<UserService> InitializeUserClientInstanceAsync(IConfigurationSection configurationSection)
        {
            string databaseName = configurationSection.GetSection("DatabaseName").Value;
            string containerName = configurationSection.GetSection("ContainerName").Value;
            string account = configurationSection.GetSection("Account").Value;
            string key = configurationSection.GetSection("Key").Value;
            Microsoft.Azure.Cosmos.CosmosClient client = new Microsoft.Azure.Cosmos.CosmosClient(account, key);

            string apiKey = configurationSection.GetSection("Email_APIKey").Value;
            string subject = configurationSection.GetSection("Email_Subject").Value;
            string from = configurationSection.GetSection("Email_From").Value;
            string fromName = configurationSection.GetSection("Email_FromName").Value;
            string plainText = configurationSection.GetSection("Email_PlainText").Value;
            string htmlContent = configurationSection.GetSection("Email_HtmlContent").Value;

            IEmailServiceProvider emailServiceProvider = new EmailServiceProvider(apiKey, subject, from, fromName, plainText, htmlContent);

            UserService userService = new UserService(client, emailServiceProvider, databaseName, containerName);
            
            Microsoft.Azure.Cosmos.DatabaseResponse database = await client.CreateDatabaseIfNotExistsAsync(databaseName);
            await database.Database.CreateContainerIfNotExistsAsync(containerName, "/id");

            return userService;
        }
        // </InitializeUserClientInstanceAsync>
    }
}
