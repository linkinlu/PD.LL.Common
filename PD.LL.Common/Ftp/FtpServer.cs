using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentFTP;

namespace PD.LL.Common.Ftp
{
    public class FtpServer : FtpServerBase
    {
        public FtpServer(string host, int port, string user, string password) : base(host, port, user, password)
        {
            
        }
        static async Task Recycle(IFtpClient ftp, string oldDirectory, string oldName)
        {
            var newDirectory = PathCombine(RecycleBin, DateTime.Now.ToString(yyyyMMdd), oldDirectory);

            if (! ftp.DirectoryExists(newDirectory))
                 ftp.CreateDirectory(newDirectory);

            var oldPath = PathCombine(oldDirectory, oldName);
            var newPath = newDirectory + Slash + RemoveStartAndEndSlash(oldName);

             ftp.MoveFile(oldPath, newPath);
        }

        void ReleaseRecycleBin()
        {
            Task.Run(ReleaseRecycleBinCore);
        }

        void ReleaseRecycleBinCore()
        {
            var now = DateTime.Now.Date;
            if (lastRelease != null && lastRelease >= now)
                return;

            lock (syncObj)
            {
                if (lastRelease != null && lastRelease >= now)
                    return;

                var timeline = now.AddMonths(-1).ToString(yyyyMMdd);

                try
                {
                    using (var ftp = new FtpClient(this.Host, this.Port))
                    {
                        ftp.Connect();

                        if (!ftp.DirectoryExists(RecycleBin))
                            return;

                        var all = ftp.GetNameListing(RecycleBin);
                        var deleteDirs = all.Where(w =>
                        {
                            var folder = w.Split('/').Last();
                            return regexFolder.IsMatch(folder) && string.Compare(folder, timeline, StringComparison.OrdinalIgnoreCase) < 0;
                        }).ToArray();

                        foreach (var dir in deleteDirs)
                        {
                            DeleteRecursively(ftp, dir);
                        }

                        lastRelease = now;
                    }
                }
                catch (Exception ex) { }
            }
        }

        void DeleteRecursively(IFtpClient ftp, string path)
        {
            foreach (var item in ftp.GetListing(path))
            {
                switch (item.Type)
                {
                    case FtpObjectType.File:
                        ftp.DeleteFile(item.FullName);
                        break;

                    case FtpObjectType.Directory:
                        DeleteRecursively(ftp, item.FullName);
                        break;
                }
            }

            ftp.DeleteDirectory(path);
        }

        public override async Task<FtpFile> Upload(byte[] data, string directory, string name)
        {
            ReleaseRecycleBin();

            using (var ftp = new FtpClient(this.Host, this.Port))
            {
                 ftp.Connect();

                var path = PathCombineFolder(directory);

                if (! ftp.DirectoryExists(path))
                     ftp.CreateDirectory(path);

                 ftp.SetWorkingDirectory(path);

                name = RemoveStartAndEndSlash(name);

                ftp.UploadBytes(data, name);

                return new FtpFile
                {
                    Directory = path,
                    Name = name
                };
            }
        }

        public override async Task<bool> Delete( string directory, string name)
        {
            ReleaseRecycleBin();

            using (var ftp = new FtpClient(this.Host, this.Port))
            {
                 ftp.Connect();

                var path = PathCombine(directory, name);
                if ( ftp.FileExists(path))
                {
                     Recycle(ftp, directory, name);
                    return true;
                }

                path = PathCombineFolder( directory, name);
                if ( ftp.FileExists(path))
                {
                     Recycle(ftp, PathCombine( directory), name);
                    return true;
                }

                return true;
            }
        }

        public override async Task<Stream> Download(string directory, string name)
        {
            using (var ftp = new FtpClient(this.Host, this.Port))
            {
                 ftp.Connect();

                var data = new MemoryStream();

                do
                {
                    var path = PathCombine(directory, name);
                    if (ftp.FileExists(path))
                    {
                        ftp.DownloadStream(data, path);
                        break;
                    }

                    path = PathCombineFolder(directory, name);
                    if ( ftp.FileExists(path))
                    {
                         ftp.DownloadStream(data, path);
                        break;
                    }
                } while (false);

                data.Position = 0;
                return data;
            }
        }
    }
}