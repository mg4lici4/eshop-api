namespace EShop.Application.Helpers
{
    public static class FechaHelper
    {
        public static DateTime ActualUTC()
        {
            return DateTime.UtcNow;
        }
        public static DateTime ActualUTC(double minutos)
        {
            return DateTime.UtcNow.AddMinutes(minutos);
        }
    }
}
