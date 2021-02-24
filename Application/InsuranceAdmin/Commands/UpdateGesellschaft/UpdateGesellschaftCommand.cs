using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.InsuranceAdmin.Commands.UpdateGesellschaft
{
    public class UpdateGesellschaftCommand : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class UpdateGesellschaftCommandHandler : IRequestHandler<UpdateGesellschaftCommand>
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IInsuranceDbContext _insuranceDbContext;

        public UpdateGesellschaftCommandHandler(
            ICurrentUserService currentUserService,
            IInsuranceDbContext insuranceDbContext)
        {
            _currentUserService = currentUserService;
            _insuranceDbContext = insuranceDbContext;
        }


        public async Task<Unit> Handle(UpdateGesellschaftCommand command, CancellationToken cancellationToken)
        {
            if(!(_currentUserService.IsAdmin || _currentUserService.IsBearbeiter))
                throw new UnauthorizedAccessException();

            var gesellschaftToUpdate = await _insuranceDbContext.GesellschaftSet
                .FirstOrDefaultAsync(gs => gs.Id == command.Id, cancellationToken);

            if (gesellschaftToUpdate == null)
                throw new NotFoundException($"Gesellschaft with id {command.Id}, does not Exist");

            gesellschaftToUpdate.Name = command.Name;

            await _insuranceDbContext.SaveChangesAsync(cancellationToken);
            
            return Unit.Value;
        }
    }
}