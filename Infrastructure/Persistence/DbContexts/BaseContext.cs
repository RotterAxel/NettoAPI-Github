using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.DbContexts
{
    public class BaseContext : DbContext
    {
        private readonly IDateTime _dateTimeService;
        private readonly ICurrentUserService _currentUserService;

        public BaseContext(DbContextOptions options, 
            IDateTime dateTimeService,
            ICurrentUserService currentUserService)
            : base(options)
        {
            _dateTimeService = dateTimeService;
            _currentUserService = currentUserService;
        }

        public BaseContext(DbContextOptions options)
            : base(options)
        {
        }
        
        public override int SaveChanges()
        {
            SetAuditableDataOnAuditableEntities();

            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            SetAuditableDataOnAuditableEntities();

            return base.SaveChangesAsync(cancellationToken);
        }
        
        private void SetAuditableDataOnAuditableEntities()
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTimeService.UtcNow;
                        entry.Entity.RowVersion = _dateTimeService.UtcNow;
                        entry.Entity.CreatedBy = _currentUserService.KeycloakUserId;
                        break;
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = _currentUserService.KeycloakUserId;
                        entry.Entity.RowVersion = _dateTimeService.UtcNow;
                        break;
                }
            }
        }
    }
}