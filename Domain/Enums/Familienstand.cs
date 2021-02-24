using System;

// ReSharper disable InconsistentNaming
// ReSharper disable StringLiteralTypo

namespace Domain.Enums
{
    public enum Familienstand
    {
        LD = 1,
        VH = 2,
        VW = 3,
        GS = 4,
        EA = 5,
        LP = 6,
        LV = 7,
        LA = 8,
        LE = 9,
        NB = 10
    }

    static class FamilienstandExtension
    {
        public static String GetString(this Familienstand familienstand)
        {
            switch (familienstand)
            {
                case Familienstand.LD:
                    return "Ledig";
                case Familienstand.VH:
                    return "Verheiratet";
                case Familienstand.VW:
                    return "Verwitwet";
                case Familienstand.GS:
                    return "Geschieden";
                case Familienstand.EA:
                    return "Ehe aufgehoben";
                case Familienstand.LP:
                    return "In eingetragener Lebenspartnerschaft";
                case Familienstand.LV:
                    return "Durch Tod aufgelöste Lebenspartnerschaft";
                case Familienstand.LA:
                    return "Aufgehobene Lebenspartnerschaft";
                case Familienstand.LE:
                    return "Durch Todeserklärung aufgelöste Lebenspartnerschaft";
                case Familienstand.NB:
                    return "Nicht bekannt";
                default:
                    return "";
            }
        }
    }
}