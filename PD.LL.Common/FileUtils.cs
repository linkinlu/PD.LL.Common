﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PD.LYY.UtilityLib
{
    public static class FileUtils
    {

        #region ExistsByFile

        /// <summary>
        /// 判断文件是否存在
        /// </summary>
        /// <param name="path">文件绝对路径</param>
        public static bool ExistsByFile(string path)
        {
            return System.IO.File.Exists(path);
        }

        #endregion

        #region ExistsByDirectory

        /// <summary>
        /// 判断目录是否存在
        /// </summary>
        /// <param name="path">目录绝对路径</param>
        public static bool ExistsByDirectory(string path)
        {
            return Directory.Exists(path);
        }

        #endregion

        #region CreateDirectory

        /// <summary>
        /// 创建目录
        /// </summary>
        /// <param name="path">文件或目录绝对路径</param>
        public static void CreateDirectory(string path)
        {
            if (path.IsEmpty())
                return;
            var file = new FileInfo(path);
            var directoryPath = file.Directory?.FullName;
            if (Directory.Exists(directoryPath))
                return;
            Directory.CreateDirectory(directoryPath);
        }

        #endregion

        #region ToBytesAsync

        /// <summary>
        /// 流转换为字节数组
        /// </summary>
        /// <param name="stream">流</param>
        public static async Task<byte[]> ToBytesAsync(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[stream.Length];
            await stream.ReadAsync(buffer, 0, buffer.Length);
            return buffer;
        }

        #endregion

        #region ToBytes

        /// <summary>
        /// 流转换为字节数组
        /// </summary>
        /// <param name="stream">流</param>
        public static byte[] ToBytes(Stream stream)
        {
            stream.Seek(0, SeekOrigin.Begin);
            var buffer = new byte[stream.Length];
            stream.Read(buffer, 0, buffer.Length);
            return buffer;
        }

        /// <summary>
        /// 字符串转换成字节数组
        /// </summary>
        /// <param name="data">数据,默认字符编码utf-8</param>        
        public static byte[] ToBytes(string data)
        {
            return ToBytes(data, Encoding.UTF8);
        }

        /// <summary>
        /// 字符串转换成字节数组
        /// </summary>
        /// <param name="data">数据</param>
        /// <param name="encoding">字符编码</param>
        public static byte[] ToBytes(string data, Encoding encoding)
        {
            if (string.IsNullOrWhiteSpace(data))
                return new byte[] { };
            return encoding.GetBytes(data);
        }

        #endregion

        #region ReadToString

        /// <summary>
        /// 读取文件到字符串
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        public static string ReadToString(string filePath)
        {
            return ReadToString(filePath, Encoding.UTF8);
        }

        /// <summary>
        /// 读取文件到字符串
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="encoding">字符编码</param>
        public static string ReadToString(string filePath, Encoding encoding)
        {
            if (System.IO.File.Exists(filePath) == false)
                return string.Empty;
            using (StreamReader streamReader = new StreamReader(filePath, encoding))
            {
                return streamReader.ReadToEnd();
            }
            
        }

        #endregion

        #region ReadToStringAsync

        /// <summary>
        /// 读取文件到字符串
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        public static async Task<string> ReadToStringAsync(string filePath)
        {
            return await ReadToStringAsync(filePath, Encoding.UTF8);
        }

        /// <summary>
        /// 读取文件到字符串
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="encoding">字符编码</param>
        public static async Task<string> ReadToStringAsync(string filePath, Encoding encoding)
        {
            if (System.IO.File.Exists(filePath) == false)
                return string.Empty;
            using (var reader = new StreamReader(filePath, encoding))
                return await reader.ReadToEndAsync();
        }

        #endregion

        #region ReadFile

        /// <summary>
        /// 将文件读取到字节流中
        /// </summary>
        /// <param name="filePath">文件的绝对路径</param>
        public static byte[] ReadFile(string filePath)
        {
            if (!System.IO.File.Exists(filePath))
                return null;
            var fileInfo = new FileInfo(filePath);
            using (var reader = new BinaryReader(fileInfo.Open(FileMode.Open)))
                return reader.ReadBytes((int)fileInfo.Length);
        }

        #endregion

        #region Write

        /// <summary>
        /// 将字符串写入文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="content">内容</param>
        public static void Write(string filePath, string content)
        {
            Write(filePath, ConvertManager.To<string,byte[]>(content));
        }

        /// <summary>
        /// 将字节流写入文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        /// <param name="content">内容</param>
        public static void Write(string filePath, byte[] content)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;
            if (content == null)
                return;
            CreateDirectory(filePath);
            System.IO.File.WriteAllBytes(filePath, content);
        }

        #endregion

        #region WriteAsync

  
        #endregion

        #region Delete

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePaths">文件绝对路径集合</param>
        public static void Delete(IEnumerable<string> filePaths)
        {
            foreach (var filePath in filePaths)
                Delete(filePath);
        }

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="filePath">文件绝对路径</param>
        public static void Delete(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);
        }

        #endregion

        #region GetAllFiles

        /// <summary>
        /// 获取全部文件,包括所有子目录
        /// </summary>
        /// <param name="path">目录路径</param>
        /// <param name="searchPattern">搜索模式</param>
        public static List<FileInfo> GetAllFiles(string path, string searchPattern)
        {
            return Directory.GetFiles(path, searchPattern, SearchOption.AllDirectories)
                .Select(filePath => new FileInfo(filePath)).ToList();
        }

        #endregion
    }
}
