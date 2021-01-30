using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Docker.DotNet.Models;

namespace Docker.DotNet
{
    internal class TasksOperations : ITasksOperations
    {
        private readonly DockerClient _client;

        internal TasksOperations(DockerClient client)
        {
            _client = client;
        }

        async Task<TaskResponse> ITasksOperations.InspectAsync(string id, CancellationToken cancellationToken)
        {
            var response = await _client.MakeRequestAsync(_client.NoErrorHandlers, HttpMethod.Get, $"tasks/{id ?? throw new ArgumentNullException(nameof(id))}", cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<TaskResponse>(response.Body);
        }

        Task<IList<TaskResponse>> ITasksOperations.ListAsync(CancellationToken cancellationToken)
        {
            return ((ITasksOperations)this).ListAsync(null, cancellationToken);
        }

        async Task<IList<TaskResponse>> ITasksOperations.ListAsync(TasksListParameters parameters, CancellationToken cancellationToken)
        {
            IQueryString query = null;
            if (parameters != null)
            {
                query = new QueryString<TasksListParameters>(parameters);
            }

            var response = await _client.MakeRequestAsync(_client.NoErrorHandlers, HttpMethod.Get, "tasks", query, cancellationToken).ConfigureAwait(false);
            return _client.JsonSerializer.DeserializeObject<IList<TaskResponse>>(response.Body);
        }
    }
}
