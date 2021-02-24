using System;
using System.Linq;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services
{
    public class VermittlerNoGenerator : IVermittlerNoGenerator
    {
        private const string VermittlerNoPrefix = "NP-";
        
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IRandomStringGenerator _randomStringGenerator;
        private readonly ILogger<VermittlerNoGenerator> _logger;

        public VermittlerNoGenerator(
            IInsuranceDbContext insuranceDbContext,
            IRandomStringGenerator randomStringGenerator,
            ILogger<VermittlerNoGenerator> logger)
        {
            _insuranceDbContext = insuranceDbContext;
            _randomStringGenerator = randomStringGenerator;
            _logger = logger;
        }

        /// <summary>
        /// Gets all VermittlerNo from DB and generates a random 6 character string.
        /// When a unique string is found returns that VermittlerNr as string
        /// </summary>
        /// <returns>VermittlerNo</returns>
        public async Task<string> GenerateVermittlerNoAsync()
        {
            //Get a list of all VermittlerNo on DB
            var vermittlerNoList = await _insuranceDbContext.Vermittler
                .Select(v => v.VermittlerNo).ToListAsync();

            while (true)
            {
                string vermittlerNoToCheck = VermittlerNoPrefix;

                vermittlerNoToCheck += _randomStringGenerator
                    .GenerateRandomStringOfLength(6, true, false, true);

                if (vermittlerNoList.All(v => v != vermittlerNoToCheck))
                {
                    return vermittlerNoToCheck;
                }
            }
        }
    }
}