using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities.Insurance;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Application.BackgroundTasks.Commands
{
    public class CreateVemittlerGesellschaftForAllVermittler : IRequest
    {
        public int NeueGesellschaftsId { get; set; }
    }

    public class CreateVemittlerGesellschaftForAllVermittlerHandler : 
        IRequestHandler<CreateVemittlerGesellschaftForAllVermittler>
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly ILogger<CreateVemittlerGesellschaftForAllVermittlerHandler> _logger;

        public CreateVemittlerGesellschaftForAllVermittlerHandler(IInsuranceDbContext insuranceDbContext,
            ILogger<CreateVemittlerGesellschaftForAllVermittlerHandler> logger)
        {
            _insuranceDbContext = insuranceDbContext;
            _logger = logger;
        }
        
        public async Task<Unit> Handle(CreateVemittlerGesellschaftForAllVermittler command, CancellationToken cancellationToken)
        {
            
            var gesellschaft = await _insuranceDbContext.GesellschaftSet
                .FirstOrDefaultAsync(g => g.Id == command.NeueGesellschaftsId, cancellationToken);

            if (gesellschaft == null)
            {
                throw new NotFoundException("Gesellschaft", command.NeueGesellschaftsId);
            }

            var vermittlerListe = await _insuranceDbContext.Vermittler
                .Include(v => v.VermittlerGesellschafften)
                .ToListAsync();

            foreach (var vermittler in vermittlerListe)
            {
                if(vermittler.VermittlerGesellschafften.Any(vg => vg.GesellschaftId == gesellschaft.Id))
                    continue;
                
                vermittler.VermittlerGesellschafften.Add(new VermittlerGesellschafft
                {
                    VermittlerId = vermittler.Id,
                    GesellschaftId = gesellschaft.Id,
                    VermittlerNo = null,
                    Abschlussvergütung = 0.08,
                    Bestandsvergütung = 0.08,
                    MaxLaufzeitVergütung = 40
                });

                await _insuranceDbContext.SaveChangesAsync(cancellationToken);
            }
            
            return Unit.Value;
        }
    }
}