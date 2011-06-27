using System.IO;

namespace JSBuild.Utility
{
    public static class PathExtensions
    {
        public static string ScriptName(this string fullPath)
        {
            return Path.GetFileNameWithoutExtension(fullPath);
        }
    }
}
