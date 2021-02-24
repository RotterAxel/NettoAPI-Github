namespace Domain.Enums
{
    public enum Bearbeitungsstatus
    {
        ZuPrüfen = 1,
        MussÜberarbeitetWerden = 2,
        Aktzeptiert = 3
    }
    
    // public static class BearbeitungsStatusExtesion
    // {
    //     public static string ToDescription(this Bearbeitungsstatus bearbeitungsstatus)
    //     {
    //         switch (bearbeitungsstatus)
    //         {
    //             case Bearbeitungsstatus.ZuPrüfen:
    //                 return "Zu Prüfen";
    //             case Bearbeitungsstatus.MussÜberarbeitetWerden:
    //                 return "Muss überarbeitet werden";
    //             case Bearbeitungsstatus.Aktzeptiert:
    //                 return "Aktzeptiert";
    //             default:
    //                 throw new ArgumentOutOfRangeException("Bearbeitunsstatus");
    //         }
    //     }
    // }
}