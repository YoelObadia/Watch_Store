using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    public  interface ICrud<T> where T : struct
    {
        public  void Add(T t);
        public  void Update(int Id1,int Id2);
        public T Get(int Id1,int Id2);
        public void Delete(int Id1, int Id2);

        // here we have defindes an optional parameter for a predicate withe the signature of the fumc<>
        public IEnumerable<T?> GetList(Func<T?, bool>? predicate=null);
    }
}
