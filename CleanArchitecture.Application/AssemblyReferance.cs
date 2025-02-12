using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application
{
    public static class AssemblyReferance
    {
        public static readonly Assembly assembly = typeof(Assembly).Assembly;
    }
}
