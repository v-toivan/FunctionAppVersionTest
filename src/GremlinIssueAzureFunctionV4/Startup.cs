using System;
using GremlinIssueAzureFunction.Common;
using GremlinIssueAzureFunction.Common.Implementations;
using GremlinIssueAzureFunction.Common.Interfaces;
using GremlinIssueAzureFunctionV4;
using GremlinIssueAzureFunctionV4.Implementations;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;



[assembly: FunctionsStartup(typeof(Startup))]
namespace GremlinIssueAzureFunctionV4
{
    public class Startup : FunctionsStartup
    {
        private static IConfiguration Configuration { set; get; }
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var azureWebJobsScriptRoot = Environment.GetEnvironmentVariable("AzureWebJobsScriptRoot");
            Configuration=   builder.ConfigurationBuilder

                .SetBasePath(azureWebJobsScriptRoot)
                .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                .Build();
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(s => Configuration);
            builder.Services.AddSingleton<IGremlinQuery, GremlinQuery>();
            builder.Services.AddSingleton<IPartitionKeyHelper, PartitionKeyHelper>();
            builder.Services.AddSingleton<IPersonService, PersonService>();
        }
    }
}
