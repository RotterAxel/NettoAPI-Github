using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IVermittlerNoGenerator
    {
        Task<string> GenerateVermittlerNoAsync();
    }
}