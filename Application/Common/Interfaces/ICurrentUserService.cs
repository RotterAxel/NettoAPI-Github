namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        string KeycloakUserId { get; }
        bool IstVermittler { get; }
        bool IsAdmin { get; }
        bool IsBearbeiter { get; }
        string Email { get; }
        
        int? ApiUserId { get; set; }
    }
}