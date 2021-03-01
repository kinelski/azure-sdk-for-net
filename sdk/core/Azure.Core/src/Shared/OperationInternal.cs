// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Azure.Core.Pipeline;

namespace Azure.Core
{
    internal class OperationInternal<TValue, TResult>
    {
        private readonly string _operationTypeName;

        private readonly TimeSpan _defaultPollingInterval;

        private readonly ClientDiagnostics _clientDiagnostics;

        private readonly Func<CancellationToken, Task<Response<TResult>>> _getResponseAsync;

        private readonly Func<CancellationToken, Response<TResult>> _getResponse;

        private readonly Func<Response<TResult>, OperationInternalResponseStatus> _getStatus;

        private readonly Func<Response<TResult>, TValue> _parseResponse;

        private readonly Func<Response<TResult>, RequestFailedException> _getFailure;

        private Response _rawResponse;

        private TValue _value;

        private RequestFailedException _requestFailedException;

        public OperationInternal(string operationTypeName,
            ClientDiagnostics clientDiagnostics,
            Func<CancellationToken, Task<Response<TResult>>> getResponseAsync,
            Func<CancellationToken, Response<TResult>> getResponse,
            Func<Response<TResult>, OperationInternalResponseStatus> getStatus,
            Func<Response<TResult>, TValue> parseResponse,
            Func<Response<TResult>, RequestFailedException> getFailure,
            TimeSpan? defaultPollingInterval = null)
        {
            _operationTypeName = operationTypeName;
            _clientDiagnostics = clientDiagnostics;
            _getResponseAsync = getResponseAsync;
            _getResponse = getResponse;
            _getStatus = getStatus;
            _parseResponse = parseResponse;
            _getFailure = getFailure;
            _defaultPollingInterval = defaultPollingInterval ?? TimeSpan.FromSeconds(1);
        }

        public bool HasCompleted { get; private set; }

        public bool HasValue { get; private set; }

        public TValue Value
        {
            get
            {
                if (HasValue)
                {
                    return _value;
                }
                else if (_requestFailedException != null)
                {
                    throw _requestFailedException;
                }
                else
                {
                    throw new InvalidOperationException("The operation has not completed yet.");
                }
            }
        }

        public Response GetRawResponse() => _rawResponse;

        public async ValueTask<Response> UpdateStatusAsync(CancellationToken cancellationToken) =>
            await UpdateStatusAsync(async: true, cancellationToken).ConfigureAwait(false);

        public Response UpdateStatus(CancellationToken cancellationToken) =>
            UpdateStatusAsync(async: false, cancellationToken).EnsureCompleted();

        public async ValueTask<Response<TValue>> WaitForCompletionAsync(CancellationToken cancellationToken) =>
            await WaitForCompletionAsync(_defaultPollingInterval, cancellationToken).ConfigureAwait(false);

        public async ValueTask<Response<TValue>> WaitForCompletionAsync(TimeSpan pollingInterval, CancellationToken cancellationToken)
        {
            while (true)
            {
                await UpdateStatusAsync(cancellationToken).ConfigureAwait(false);
                if (HasCompleted)
                {
                    return Response.FromValue(Value, GetRawResponse());
                }

                await Task.Delay(pollingInterval, cancellationToken).ConfigureAwait(false);
            }
        }

        private async ValueTask<Response> UpdateStatusAsync(bool async, CancellationToken cancellationToken)
        {
            if (HasCompleted)
            {
                return GetRawResponse();
            }

            var scopeName = async
                ? $"{_operationTypeName}.UpdateStatusAsync"
                : $"{_operationTypeName}.UpdateStatus";

            using DiagnosticScope scope = _clientDiagnostics.CreateScope(scopeName);
            scope.Start();

            try
            {
                var response = async
                    ? await _getResponseAsync(cancellationToken).ConfigureAwait(false)
                    : _getResponse(cancellationToken);

                _rawResponse = response.GetRawResponse();

                var status = _getStatus(response);

                if (status == OperationInternalResponseStatus.Succeeded)
                {
                    _value = _parseResponse(response);
                    HasValue = true;
                    HasCompleted = true;
                }
                else if (status == OperationInternalResponseStatus.Failed)
                {
                    _requestFailedException = _getFailure(response);
                    HasCompleted = true;

                    throw _requestFailedException;
                }
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }

            return GetRawResponse();
        }
    }

    internal enum OperationInternalResponseStatus
    {
        Pending,
        Succeeded,
        Failed
    }
}
