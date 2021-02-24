using System.Linq;
using System.Security.Cryptography;
using Application.Common.Interfaces;
using Microsoft.Extensions.Logging;

namespace Application.Common.Validators
{
    public class EinladecodeVermittlerValidation : IEinladecodeVermittlerValidation
    {
        private readonly IInsuranceDbContext _insuranceDbContext;
        private readonly IAESCryptographyService _iAesCryptographyService;
        private readonly ILogger<EinladecodeVermittlerValidation> _logger;

        public EinladecodeVermittlerValidation(IInsuranceDbContext insuranceDbContext,
            IAESCryptographyService iAesCryptographyService,
            ILogger<EinladecodeVermittlerValidation> logger)
        {
            _insuranceDbContext = insuranceDbContext;
            _iAesCryptographyService = iAesCryptographyService;
            _logger = logger;
        }

        /// <summary>
        /// Unsere Priorität ist, die Daten des Vermittlers zu speichern unabhängig von der Validität der Einladecodes.
        /// Existiert der Einladende Vermittler noch?
        /// Ist der Einladecode richtig?
        /// Kann der Einladecode Entziffert werden?
        /// </summary>
        /// <param name="einladecode"></param>
        /// <returns>bool</returns>
        public bool Validate(string einladecode)
        {
            int einladenderVermittlerId = -1;

            //Decrypt the einladecode if one is available
            //It must be decryptable and parseable to an int
            if (!string.IsNullOrEmpty(einladecode))
            {
                try
                {
                    var decryptedString = _iAesCryptographyService.DecryptString(
                        einladecode);

                    if (!int.TryParse(decryptedString, out einladenderVermittlerId))
                        return false;
                }
                catch (CryptographicException e)
                {
                    var request = nameof(_iAesCryptographyService);
                    _logger.LogError(e, $"CryptographicException for Request: {request}");
                    return false;
                }
            }

            //Einladender vermittler must exist
            if (_insuranceDbContext.Vermittler.Any(v => v.Id == einladenderVermittlerId))
                return true;

            return false;
        }
    }
}