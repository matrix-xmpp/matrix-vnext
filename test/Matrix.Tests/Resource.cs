using System.IO;
using System.Reflection;

namespace Matrix.Tests
{
    public static class Resource
    {
        public static string Get(string path)
        {
            Assembly assembly = typeof(Resource).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream(assembly.GetName().Name + $".{path}");
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}
