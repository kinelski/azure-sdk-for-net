// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace Azure.Core
{
    internal class OperationInternal<T>
    {
        private ClientDiagnostics _clientDiagnostics;

        private bool _hasCompleted;

        private Response _rawResponse;

        public OperationInternal(ClientDiagnostics clientDiagnostics)
        {
            _clientDiagnostics = clientDiagnostics;
        }

        public enum OperationStatus
        {
            Succeeded,
            Failed,
            Pending
        }

        public Response GetRawResponse() => _rawResponse;

        public async ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken) =>
            await UpdateStatusAsync(async: true, cancellationToken).ConfigureAwait(false);

        public Response UpdateStatus(CancellationToken cancellationToken) =>
            UpdateStatusAsync(async: false, cancellationToken).EnsureCompleted();

        private async ValueTask<Response> UpdateStatusAsync(bool async, CancellationToken cancellationToken)
        {
            if (_hasCompleted)
            {
                return GetRawResponse();
            }

            using DiagnosticScope scope = _clientDiagnostics.CreateScope();
            scope.Start();

            try
            {
                var response = async
                    ? await GetResponseAsync(cancellationToken).ConfigureAwait(false)
                    : GetResponse(cancellationToken);

                _rawResponse = response.GetRawResponse();
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }

            return GetRawResponse();
        }

        private static Task<Response<T>> GetResponseAsync(CancellationToken cancellationToken)
        {
            throw new Exception(nameof(cancellationToken));
        }

        private static Response<T> GetResponse(CancellationToken cancellationToken)
        {
            throw new Exception(nameof(cancellationToken));
        }

        private static OperationStatus GetStatus(Response<T> response)
        {
            if (response == null)
            {
                return OperationStatus.Pending;
            }
            else
            {
                return OperationStatus.Succeeded;
            }
        }
    }
}
