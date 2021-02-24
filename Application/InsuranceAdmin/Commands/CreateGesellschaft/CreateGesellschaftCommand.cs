using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.BackgroundTasks.Commands;
using Application.Common.Exceptions;
using Application.Common.HangfireMediator;
using Application.Common.Interfaces;
using Domain.Entities.Insurance;
using MediatR;

namespace Application.InsuranceAdmin.Commands.CreateGesellschaft
{
    public class CreateGesellschaftCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateGesellschaftCommandHandler : IRequestHandler<CreateGesellschaftCommand, int>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IHangfireOverMediator _hangfireOverMediator;

        public CreateGesellschaftCommandHandler(IInsuranceDbContext insuranceDbContext,
            ICurrentUserService currentUserService,
            IHangfireOverMediator hangfireOverMediator)
        {
            _insuranceDbContext = insuranceDbContext;
            _currentUserService = currentUserService;
            _hangfireOverMediator = hangfireOverMediator;
        }
        
        public async Task<int> Handle(CreateGesellschaftCommand command, CancellationToken cancellationToken)
        {
            if(!(_currentUserService.IsAdmin || _currentUserService.IsBearbeiter))
                throw new UnauthorizedAccessException();

            if (_insuranceDbContext.GesellschaftSet.Any(gs => gs.Name == command.Name))
                throw new BadRequestException("Gesellschaft mit dem Namen existiert schon.");
            
            //MaxVergütung, MinVergütung, MaxLaufzeit und MinLaufzeit werden automatisch gesetzt
            var gesellschaftToAdd = new Gesellschaft
            {
                Name = command.Name
            };

            await _insuranceDbContext.GesellschaftSet.AddAsync(gesellschaftToAdd, cancellationToken);
            
            await _insuranceDbContext.SaveChangesAsync(cancellationToken);
            
            _hangfireOverMediator.Enqueue("CreateVemittlerGesellschaftForAllVermittler", 
                new CreateVemittlerGesellschaftForAllVermittler()
            {
                NeueGesellschaftsId = gesellschaftToAdd.Id
            });
            
            return gesellschaftToAdd.Id;
        }
    }
}