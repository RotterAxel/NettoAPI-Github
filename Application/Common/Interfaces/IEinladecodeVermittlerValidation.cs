namespace Application.Common.Interfaces
{
    public interface IEinladecodeVermittlerValidation
    {
        /// <summary>
        /// Unsere Priorität ist, die Daten des Vermittlers zu speichern unabhängig von der Validität der Einladecodes.
        /// Existiert der Einladende Vermittler noch?
        /// Ist der Einladecode richtig?
        /// Kann der Einladecode Entziffert werden?
        /// </summary>
        /// <param name="einladecode"></param>
        /// <returns>bool</returns>
        bool Validate(string einladecode);
    }
}