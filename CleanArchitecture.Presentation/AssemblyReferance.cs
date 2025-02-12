using System.Reflection;

namespace CleanArchitecture.Presentation
{
    public static class AssemblyReferance
    {
        public static readonly Assembly assembly = typeof(Assembly).Assembly;
        // yukarıdaki kod parçası sayesinde CleanArchitecture.
        // Presentation katmanı içerisindeki tüm class'ları
        // program.cs içinde yapacağımız assembly referansı ile çağırabiliriz.

    }
}
