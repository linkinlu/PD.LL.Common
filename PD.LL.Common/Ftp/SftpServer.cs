using System.IO;
using System.Threading.Tasks;
using Renci.SshNet;
using Renci.SshNet.Common;
using Renci.SshNet.Sftp;

namespace PD.LL.Common.Ftp
{
     public class SftpServer : FtpServerBase
    {
        public SftpServer(string host, int port, string user, string password) : base(host, port, user, password)
        {

        }

         void CreateDirectoryRecursively(SftpClient client, string path)
        {
            string current = "";

            if (path[0] == '/')
            {
                path = path.Substring(1);
            }

            while (!string.IsNullOrEmpty(path))
            {
                int p = path.IndexOf('/');
                current += '/';
                if (p >= 0)
                {
                    current += path.Substring(0, p);
                    path = path.Substring(p + 1);
                }
                else
                {
                    current += path;
                    path = "";
                }

                try
                {
                    SftpFileAttributes attrs = client.GetAttributes(current);
                    if (!attrs.IsDirectory)
                    {
                    }
                }
                catch (SftpPathNotFoundException)
                {
                    client.CreateDirectory(current);
                }
            }
        }


        public override async Task<FtpFile> Upload(byte[] data,  string directory, string name)
        {
            using (var client = new SftpClient(this.Host, this.Port, this.User, this.Password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    var path = PathCombine(directory, name);
                    CreateDirectoryRecursively(client, directory);
                    name = RemoveStartAndEndSlash(name);

                    Stream stream = new MemoryStream(data);
                    client.UploadFile(stream, path, true);
                    return new FtpFile
                    {
                        Directory = path,
                        Name = name
                    };
                }
                return null;
            }
        }

        public override async Task<bool> Delete( string directory, string name)
        {
            using (var client = new SftpClient(this.Host, this.Port, this.User, this.Password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    var path = PathCombine(directory, name);
                    client.DeleteFile(path);
                    return true;
                }
                return false;
            }
        }

        public override async Task<Stream> Download( string directory, string name)
        {
            using (var client = new SftpClient(this.Host, this.Port, this.User, this.Password))
            {
                client.Connect();
                if (client.IsConnected)
                {
                    var path = PathCombine(directory, name);
                    var ms = new MemoryStream();
                    client.DownloadFile(path, ms);
                    ms.Position = 0;
                    return ms;
                }
                return null;
            }
        }
    }
}