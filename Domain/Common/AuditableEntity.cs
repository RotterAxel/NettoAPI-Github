using System;

namespace Domain.Common
{
    //Learn More: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/abstract
    //Abstract: Use the abstract modifier in a class declaration to indicate that
    //a class is intended only to be a base class of other classes, not instantiated
    //on its own.
    public abstract class AuditableEntity
    {
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime RowVersion { get; set; }
    }
}