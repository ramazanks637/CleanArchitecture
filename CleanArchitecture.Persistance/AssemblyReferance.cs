using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Persistance
{
    public static class AssemblyReferance
    {
        public static readonly Assembly assembly = typeof(Assembly).Assembly;
        // bu kod sayesinde AppDbContext sınıfı içerisindeki
        // OnModelCreating metodu içerisinde tüm entity'lerin konfigürasyonlarını yapabiliriz.
    }
}
