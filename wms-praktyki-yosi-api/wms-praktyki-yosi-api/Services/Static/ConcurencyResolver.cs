using Microsoft.EntityFrameworkCore;
using wms_praktyki_yosi_api.Enitities;

namespace wms_praktyki_yosi_api.Services.Static
{
    public static class ConcurencyResolver
    {
        public static readonly int MAX_TRIES = 50;
        public static readonly int MAX_TIME_SECONDS = 10;

        public static void ResolveConcurency(MagazinesDbContext context)
        {
                int tries = 0;
                while (tries < MAX_TRIES)
                {
                    try
                    {
                        context.SaveChanges();
                        return;
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        tries++;
                        Thread.Sleep(MAX_TIME_SECONDS / MAX_TRIES * 1000);
                    }
                }
        }
        public static void SafeSave(MagazinesDbContext context)
        {
            try
            {
                context.SaveChanges();
                return;
            }
            catch (DbUpdateConcurrencyException)
            {
                ResolveConcurency(context);
            }
        }
    }
}
