using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Behaviours
{
    public class CurrentApiUserBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IInsuranceDbContext _insuranceDbContext;

        public CurrentApiUserBehaviour(
            ICurrentUserService currentUserService,
            IInsuranceDbContext insuranceDbContext)
        {
            _currentUserService = currentUserService;
            _insuranceDbContext = insuranceDbContext;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            if (_currentUserService.KeycloakUserId != null)
            {
                _currentUserService.ApiUserId = (await _insuranceDbContext.Users
                    .FirstOrDefaultAsync(u => u.KeycloakIdentifier == 
                                         new Guid(_currentUserService.KeycloakUserId), cancellationToken))?.Id;
            }
        }
    }
}