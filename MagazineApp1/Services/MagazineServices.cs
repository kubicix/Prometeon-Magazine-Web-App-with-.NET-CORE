using MagazineApp1.Models;

namespace MagazineApp1.Services
{
    public class MagazineServices
    {
        private readonly INTERNContext _dbContext;

        

        public MagazineServices(INTERNContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<MagazineTable> GetMagazines()
        {
            // Dergi verilerini veritabanından çekmek için uygun sorguyu yazın ve listeyi döndürün
            return _dbContext.MagazineTables.ToList();
        }
    }
}
