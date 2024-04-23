using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PD.LL.Common.Ftp
{
      public abstract class FtpServerBase
    {
        protected const string Slash = "/";
        protected const string yyyyMMdd = "yyyy-MM-dd";
        protected const string RecycleBin = "/_RecycleBin";

        protected static DateTime? lastRelease;
        protected static readonly object syncObj = new object();
        protected static readonly Regex regexFolder = new Regex(@"^\d{4}-\d{2}-\d{2}$");


        public FtpServerBase(string host, int port, string user, string password)
        {
            this.Host = host;
            this.Port = port;
            this.User = user;
            this.Password = password;
        }
        public int Port { get; }
        public string Host { get; }
        public string User { get; }
        public string Password { get; }

        public virtual async Task<FtpFile> Upload(byte[] data, string directory, string name)
        {
            return await Task.FromResult(default(FtpFile));
        }


        public virtual async Task<bool> Delete(string directory, string name)
        {
            return await Task.FromResult(default(bool));
        }

        public virtual async Task<Stream> Download(string directory, string name)
        {
            return await Task.FromResult(default(Stream));
        }


        public static string RemoveStartAndEndSlash(string path)
        {
            if (path == null)
                path = string.Empty;

            path = path.Trim();

            if (path.StartsWith(Slash))
                path = path.Remove(0, 1);

            if (path.EndsWith(Slash))
                path = path.Remove(path.Length - 1, 1);

            return path;
        }

        public static string PathCombine(params string[] paths)
        {
            return Slash + (paths == null || paths.Length == 0 ? string.Empty : string.Join(Slash, paths.Select(RemoveStartAndEndSlash)));
        }

        public static string PathCombineFolder(string folder, params string[] paths)
        {
            folder = Slash + RemoveStartAndEndSlash(folder);

            var directory = PathCombine(paths);
            var path = directory.TrimStart(Slash.ToCharArray());
            if (path.ToLower().StartsWith("http://"))
            {
                return folder + Slash + "http" + new Uri(path).AbsolutePath;
            }
            else if (path.ToLower().StartsWith("https://"))
            {
                return folder + Slash + "https" + new Uri(path).AbsolutePath;
            }
            else if (path.ToLower().StartsWith("ftp://"))
            {
                return folder + Slash + "ftp" + new Uri(path).AbsolutePath;
            }
            else if (path.ToLower().StartsWith(@"\\"))
            {
                var tempPath = path.Substring(2, path.Length - 2);
                var index = tempPath.IndexOf('\\');
                return folder + Slash + tempPath.Substring(index + 1, tempPath.Length - index - 1);
            }
            else
            {
                return directory.StartsWith(folder, StringComparison.OrdinalIgnoreCase) ? directory : folder + directory;
            }
        }

    }
}