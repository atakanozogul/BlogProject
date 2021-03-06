using BlogProject.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.CORE.Service
{
    public interface ICoreService<T> where T : CoreEntity
    {
        bool Add(T item);
        bool Add(List<T> item);
        bool Update(T item);
        bool Remove(T item);
        bool Remove(Guid id);

        //Linq ifadesi gönderiyoruz ve işlemi gerçekleştirirken
        //T tipindeki işlem için hepsini gerçekleştirsin
        //bool tipinde geri dönsün dedik çünkü tip vermemiz gerekiyor
        bool RemoveAll(Expression<Func<T,bool>>exp); //belirli aralıktaki kısımları silme
        
        //Guid id olarak tek bir eleman getirsin istiyorsak böyle yazarız
        T GetByID(Guid id);

        T GetByDefault(Expression<Func<T, bool>> exp);
        List<T> GetDefault(Expression<Func<T, bool>> exp);
        List<T> GetActive(); //aktif olanları bulma
        List<T> GetAll(); //hepsini bulma
        bool Activate(Guid id); //id ye göre aktif edildi mi durumu
        bool Any(Expression<Func<T, bool>> exp); //linq sorgusuna göre hiç var mı yok mu ona bakabiliriz
        int Save(); //kayıt sırasında kaç satır etkilendiğini belirttiğimiz kısım
    }
}
