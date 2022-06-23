using BlogProject.CORE.Entity;
using BlogProject.CORE.Entity.Enums;
using BlogProject.CORE.Service;
using BlogProject.MODEL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace BlogProject.SERVICE.Base
{
    public class BaseService<T> : ICoreService<T> where T : CoreEntity
    {
        //İlk olarak veritabanı bağlantımızı gerçekleştiriyoruz
        private readonly BlogContext context;
        public BaseService(BlogContext _context)
        {
            //private olarak belirlediğimiz context ile dışarıdan gelen contexti eşitliyoruz.
            this.context = _context;
        }

        public bool Activate(Guid id)
        {
            //GetById metoduna giderek seçtiğimiz nesneyi id sine göre bulma yapıyoruz
            T activated = GetByID(id);
            activated.Status = Status.Active;
            //status işlemini active çektikten sonra update metoduna gönderiyoruz
            return Update(activated);
        }

        public bool Add(T item)
        {
            try
            {
                //Add metoduna gelen nesneyi ekliyoruz
                context.Set<T>().Add(item);
                //Save metodunu çağırarak save işlemini yapıyoruz.
                return Save() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Add(List<T> item)
        {
            try
            {
                //T sql komutlarımızda olan akış içerisindeki işlemlerimiz gerçekleşebiliyor ve örnek bir durum olarak dersek; 4 işlemi gerçekleştirip 5. hata oluşursa transaction işlemi devreye girer ve önceki 4 işlemi eklemeyi engeller. Son eleman için de işlem başarılı olursa o zaman ekleme yapar. (commit roleback olayı?)
                using (TransactionScope ts = new TransactionScope())
                {
                    context.Set<T>().AddRange(item);
                    ts.Complete(); //tüm işlemler başarılı ise bu metodu çağırırız.
                    return Save() > 0;
                }
            }
            catch (Exception)
            {
                //Herhangi bir hata durumunda false dönsün dedik.
                return false;
            }
        }

        public bool Any(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().Any(exp);
        }
        //--------------------------------------------------
        //Yukarıdaki işlemin aynısı
        //public bool Any(Expression<Func<T, bool>> exp) => context.Set<T>().Any(exp);
        //Olarak da yazılabilir
        //--------------------------------------------------

        public List<T> GetActive()
        {
            //Where şartı koyarak deleted olmayanları listelettik
            return context.Set<T>().Where(x => x.Status != Status.Deleted).ToList();
        }

        public List<T> GetAll()
        {
            //Tamamını almak için
            return context.Set<T>().ToList();
        }

        public T GetByDefault(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().FirstOrDefault(exp);
        }

        public T GetByID(Guid id)
        {
            //context.Categories.Find(id);
            //context.Users.Find(id);
            //context.Posts.Find(id);
            //yukarıdakilerden herhangi biri olabilir. O anda kullanılan entity type ına göre ayarlaması için Set<T>() metodu kullanılabilir.
            return context.Set<T>().Find(id);
        }

        public List<T> GetDefault(Expression<Func<T, bool>> exp)
        {
            return context.Set<T>().Where(exp).ToList();
        }

        public bool Remove(T item)
        {
            item.Status = Status.Deleted;
            return Update(item);
        }

        public bool Remove(Guid id)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    T removedItem = GetByID(id);
                    removedItem.Status = Status.Deleted;
                    ts.Complete();
                    return Update(removedItem);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveAll(Expression<Func<T, bool>> exp)
        {
            try
            {
                using (TransactionScope ts = new TransactionScope())
                {
                    var collection = GetDefault(exp); //default olarak herşeyi bulsun ve collection içerisine atsın
                    int count = 0; //sayım yapmak için count tutuyoruz

                    foreach (var item in collection)
                    {
                        item.Status = Status.Deleted; //collection içerisindeki itemları tek tek sildik
                        bool opResult = Update(item); //sildikten sonra güncelleme işlemlerini yaptık
                        if (opResult) count++; //update işlemi true dönerse countumuzu 1 artırarak devam eder.
                    }
                    if (collection.Count == count)
                    {
                        ts.Complete();
                    }
                    else
                    {
                        return false;
                    }
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Save()
        {
            //Son olarak save metodu gelen değerleri save ediyor ve bize int olarak etkilenen satırları dönmüş oluyor.
            return context.SaveChanges();
        }

        public bool Update(T item)
        {
            try
            {
                //Update metoduna gelen nesne update ediliyor fakat save edilmiyor bu yüzden save metoduna gönderiyoruz
                context.Set<T>().Update(item);
                return Save() > 0;
            }
            catch (Exception)
            {
                //Update işlemi catche düşerse false dönsün diyoruz.
                return false;
            }
        }
    }
}
