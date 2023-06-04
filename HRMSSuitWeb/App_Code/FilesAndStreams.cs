using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SIMUtils
{
    public class FilesAndStreams
    {
        private const long BUFFER_SIZE = 4096;

        public static void SaveStreamAsFile(Stream InputStream, string FilePath)
        {
            using (Stream file = File.OpenWrite(FilePath))
            {
                CopyStream(InputStream, file);
            }
        }

        public static string GetFileNameExtension(string FileName)
        {
            return FileName.LastIndexOf('.') > -1 ?
                    FileName.Substring(FileName.LastIndexOf('.'), FileName.Length - FileName.LastIndexOf('.'))
                    : "";
        }

        public static string GetFileType(string FileName)
        {
            if (FileName.IndexOf(".gif") != -1)
            {
                return "Image";
            }
            if (FileName.IndexOf(".jpg") != -1)
            {
                return "Image";
            }
            if (FileName.IndexOf(".doc") != -1)
            {
                return "Word Document";
            }
            if (FileName.IndexOf(".pdf") != -1)
            {
                return "PDF";
            }
            if (FileName.IndexOf(".xls") != -1)
            {
                return "Excel Spreadsheet";
            }
            if (FileName.IndexOf(".txt") != -1)
            {
                return "Text File";
            }
            if (FileName.IndexOf(".rar") != -1)
            {
                return "Rar File";
            }
            if (FileName.IndexOf(".zip") != -1)
            {
                return "Zip File";
            }
            return "";
        }

        public static void CopyStream(Stream input, Stream output)
        {
            byte[] buffer = new byte[BUFFER_SIZE];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
            {
                output.Write(buffer, 0, len);
            }
        }

        public static void CopyStream(System.IO.FileStream inputStream, System.IO.Stream outputStream)
        {
            long bufferSize = inputStream.Length < BUFFER_SIZE ? inputStream.Length : BUFFER_SIZE;
            byte[] buffer = new byte[bufferSize];
            int bytesRead = 0;
            long bytesWritten = 0;
            while ((bytesRead = inputStream.Read(buffer, 0, buffer.Length)) != 0)
            {
                outputStream.Write(buffer, 0, bytesRead);
                bytesWritten += bufferSize;
            }
        }

        public static string PrepareToBeFileName(string s)
        {
            // \/:|<>*?"
            return s
                .Replace('/', '-')
                .Replace('\\', '-')
                .Replace(':', '-')
                .Replace('?', '-')
                .Replace('"', '-')
                .Replace('|', '-')
                .Replace('<', '-')
                .Replace('>', '-')
                .Replace('*', '-');
        }

    
    }
}
