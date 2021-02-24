using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

#pragma warning disable 1998
        public async Task Process(TRequest request, CancellationToken cancellationToken)
#pragma warning restore 1998
        {
            var requestName = typeof(TRequest).Name;
            string userId = _currentUserService.KeycloakUserId ?? String.Empty;

            _logger.LogInformation("Insurance-API Request: {Name} {@UserId} {@Request}",
                requestName, userId, request);
        }
    }
}