using LinqExample.LinqDemo.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinqExample.LinqDemo.SetExample
{
    class PersonCompare : IEqualityComparer<Person>
{
    public bool Equals(Person x, Person y)
    {
        if (x == null || y == null) return false;
        //下面这里可以根据条件任意组合
        return x.Name.Equals(y.Name, StringComparison.OrdinalIgnoreCase);
            //return x.Name.Equals(y.Name, StringComparison.OrdinalIgnoreCase) && x.Age == y.Age;
        }

    public int GetHashCode(Person obj)
    {
        if (obj == null) return 0;
            return HashCode.Combine(obj.Name.ToLowerInvariant());
            //return HashCode.Combine(obj.Name.ToLowerInvariant(), obj.Age);
    }
}
}
