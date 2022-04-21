using System;
using System.Collections.Generic;
using System.Text.Json;
using ExRam.Gremlinq.Core;
using Gremlin.Net.Driver;
using Gremlin.Net.Driver.Messages;
using GremlinIssueAzureFunction.Common;
using GremlinIssueAzureFunction.Common.Interfaces;
using GremlinIssueAzureFunction.Common.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using static ExRam.Gremlinq.Core.GremlinQuerySource;

namespace GremlinIssueAzureFunctionV4.Implementations
{
    public class GremlinQuery : IGremlinQuery
    {
        private readonly IGremlinQuerySource _g;
        private readonly ILogger<GremlinQuery> _logger;

        public GremlinQuery(IConfiguration configuration, ILogger<GremlinQuery> logger)
        {
            _logger = logger;
            var debugMode = configuration.GetValue<bool>("DebugMode");
            string uri = configuration.GetValue<string>("CosmosDb:Uri");
            string database = configuration.GetValue<string>("CosmosDb:Database");
            string graphName =  configuration.GetValue<string>("CosmosDb:GraphName");
            string authKey = configuration.GetValue<string>("CosmosDb:AuthKey");

            var connectionPoolSettingsConfigValues =
                JsonSerializer.Deserialize<ConnectionPoolSettingsConfigValues>(
                    configuration.GetValue<string>("ConnectionPoolSettingsConfigValues"), new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });
            var connectionPoolSettings = new Action<ConnectionPoolSettings>(
                c =>
                {
                    c.PoolSize = connectionPoolSettingsConfigValues.PoolSize;
                    c.MaxInProcessPerConnection = connectionPoolSettingsConfigValues.MaxInProcessPerConnection;
                    c.ReconnectionAttempts = connectionPoolSettingsConfigValues.ReconnectionAttempts;
                    c.ReconnectionBaseDelay =
                        TimeSpan.FromMilliseconds(
                            connectionPoolSettingsConfigValues.ReconnectionBaseDelayInMilliseconds);
                });
            _g = g
                .ConfigureEnvironment(env => env
                    .UseModel(GraphModel
                        .FromBaseTypes<IVertex, IEdge>(lookup => lookup
                            .IncludeAssembliesOfBaseTypes())
                        .ConfigureProperties(model => model
                            .ConfigureElement<IVertex>(conf => conf
                                .IgnoreOnUpdate(x => x.BucketNo))))
                    .UseCosmosDb(builder => builder
                        .At(new Uri(uri), database,
                            graphName)
                        .AuthenticateBy(authKey)
                        .ConfigureWebSocket(_ => _
                            .ConfigureGremlinClient(client => client
                                .ObserveResultStatusAttributes((requestMessage, statusAttributes) =>
                                {
                                    if (debugMode)
                                    {
                                        LogGremlinQuery(requestMessage, statusAttributes);
                                    }
                                }))
                            .ConfigureConnectionPool(connectionPoolSettings))));
        }

        private void LogGremlinQuery(RequestMessage requestMessage,
            IReadOnlyDictionary<string, object> statusAttributes)
        {
            object query = new object();
            if (requestMessage.Arguments != null)
            {
                if ((bool) requestMessage.Arguments?.TryGetValue("gremlin",
                    out query))
                {
                }
            }

            if (statusAttributes.TryGetValue("x-ms-total-request-charge",
                out var requestCharge))
            {
            }

            if (statusAttributes.TryGetValue("x-ms-server-time-ms",
                out var xMsServerTimeMs))
            {
            }

            if (statusAttributes.TryGetValue("x-ms-total-server-time-ms",
                out var xMsTotalServerTimeMs))
            {
            }

            _logger.LogInformation(
                // ReSharper disable once TemplateIsNotCompileTimeConstantProblem
                $"\n-------------------START Gremlin Log with Request Id {requestMessage.RequestId}-------------------\n" +
                $"--- Query : {query}\n--- RU Charge : {requestCharge}.\n" +
                $"--- x-ms-server-time-ms : {xMsServerTimeMs} ms.\n" +
                $"--- x-ms-total-server-time-ms : {xMsTotalServerTimeMs} ms.\n" +
                $"-------------------END Gremlin Log with Request Id {requestMessage.RequestId}-------------------");
        }

        public IGremlinQuerySource GetGremlinQuerySource()
        {
            return _g;
        }
    }
}
