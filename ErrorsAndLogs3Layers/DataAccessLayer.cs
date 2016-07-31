using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace ErrorsAndLogs3Layers.Core
{
    class DataAccessLayer
    {
        const string semicolon = ";";

        public DataAccessLayer()
        {
            
        }

        // Получить данные о папках из файла.
        public string GetFoldersFromFile()
        {
            string readFoldersTxt;

            try
            {
                // Чтение папок из файла Folders.txt.
                StreamReader fl1 = File.OpenText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Folders.txt"));
                readFoldersTxt = fl1.ReadToEnd();
                fl1.Close();
            }
            catch
            {
                return null; // Ошибка получения папок.
            }

            return readFoldersTxt;
        }

        // Получить данные о файлах из файла.
        public string GetFilesFromFile()
        {
            string readFilesTxt;

            try
            {
                // Чтение файлов из файла Files.txt.
                StreamReader fl2 = File.OpenText(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files.txt"));
                readFilesTxt = fl2.ReadToEnd();
                fl2.Close();

            }
            catch
            {
                return null; // Ошибка получения файлов.
            }

            return readFilesTxt;
        }

        // Записать данные о папках в файл.
        public int PutFoldersIntoFile(List<Folders> folderList)
        {
            try
            {
                FileInfo f3 = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Folders.txt"));
                StreamWriter w1 = f3.CreateText();
                foreach (Folders item in folderList)
                {
                    w1.Write("{0};", item.Name);
                    w1.Write("{0};", item.LocationFolder);
                }
                w1.Close();
            }
            catch
            {
                return 1; // Ошибка записи в файл.
            }
            
            return 0;
        }

        // Записать данные о файлах в файл.
        public int PutFilesIntoFile(List<Files> fileList)
        {
            try
            {
                // Запись в Files.txt.
                FileInfo f4 = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files.txt"));
                StreamWriter w2 = f4.CreateText();
                foreach (Files item in fileList)
                {
                    w2.Write("{0};", item.Name);
                    w2.Write("{0};", item.LocationFolder);
                }
                w2.Close();
            }
            catch
            {
                return 1; // Ошибка записи в файл.
            }

            return 0;
        }
        
    }
}
