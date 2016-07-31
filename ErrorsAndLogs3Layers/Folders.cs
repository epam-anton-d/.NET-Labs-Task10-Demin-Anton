using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorsAndLogs3Layers.Core
{
    internal class Folders : FileSystem
    {
        public Folders(string name, string locationFolder)
        {
            this.Name = name;
            this.LocationFolder = locationFolder;
        }

        public override int Create(string locationPath, List<Files> fileList, List<Folders> folderList)
        {
            char[] slash = new char[] { '\\' };

            string[] folderDir = locationPath.Split(slash);
            string path = "";
            for (int i = 0; i < folderDir.Length - 1; i++)
            {
                path += folderDir[i];
                if (i != folderDir.Length - 2)
                    path += "\\";
            }
            string endPath = folderDir[folderDir.Length - 1];

            if (!folderList.Exists(x => x.LocationFolder + "\\" + x.Name == path) && path != "c:")
            {
                return 1; // Несуществующий путь.
            }

            if (folderList.Exists(x => x.LocationFolder == path + "\\" + endPath))
            {
                return 2; // Папка в заданной директории уже существует.
            }

            try
            {
                folderList.Add(new Folders(endPath, path));
                return 0;
            }
            catch
            {
                return 3; // Неизвестная ошибка.
            }
        }

        public override int Copy(string sourcePath, string destinationPath, List<Files> fileList, List<Folders> folderList)
        {
            char[] slash = new char[] { '\\' };

            string[] folderDir = sourcePath.Split(slash);
            string path = "";
            for (int i = 0; i < folderDir.Length - 1; i++)
            {
                path += folderDir[i];
                if (i != folderDir.Length - 2)
                    path += "\\";
            }
            string endPath = folderDir[folderDir.Length - 1];
            string[] folderDir2;

            if (!folderList.Exists(x => x.LocationFolder + "\\" + x.Name == path + "\\" + endPath))
            {
                return 4; // Несуществующий путь.
            }

            if (folderList.Exists(x => x.LocationFolder == destinationPath))
            {
                return 5; // Путь уже существует.
            }

            List<Folders> findFolders = folderList.FindAll(x => x.LocationFolder.Contains(sourcePath));
            foreach (var folder in findFolders)
            {
                folderDir2 = folder.LocationFolder.Split(slash);
                string path2 = destinationPath + "\\";
                for (int i = folderDir.Length - 1; i < folderDir2.Length; i++)
                {
                    path2 += folderDir2[i] + "\\";
                }
                path2 = path2.TrimEnd(slash);
                folderList.Add(new Folders(folder.Name, path2));
            }

            List<Files> findFiles = fileList.FindAll(x => x.LocationFolder.Contains(sourcePath));
            foreach (var file in findFiles)
            {
                folderDir2 = file.LocationFolder.Split(slash);
                string path2 = destinationPath + "\\";
                for (int i = folderDir.Length - 1; i < folderDir2.Length; i++)
                {
                    path2 += folderDir2[i] + "\\";
                }
                path2 = path2.TrimEnd(slash);
                fileList.Add(new Files(file.Name, path2));
            }

            try
            {
                folderList.Add(new Folders(endPath, destinationPath));
                return 0;
            }
            catch
            {
                return 3; // Неизвестная ошибка.
            }
        }

        public override int Move(string sourcePath, string destinationPath, List<Files> fileList, List<Folders> folderList)
        {
            try
            {
                Copy(sourcePath, destinationPath, fileList, folderList);
            }
            catch
            {
                return 6; // Ошибка копирования.
            }
            try
            {
                Delete(sourcePath, fileList, folderList);
            }
            catch
            {
                return 7; // Ошибка удаления.
            }
            return 0;
        }


        // Delete.
        public override int Delete(string sourcePath, List<Files> fileList, List<Folders> folderList)
        {
            char[] slash = new char[] { '\\' };

            string[] folderDir = sourcePath.Split(slash);
            string path = "";
            for (int i = 0; i < folderDir.Length - 1; i++)
            {
                path += folderDir[i];
                if (i != folderDir.Length - 2)
                    path += "\\";
            }
            string endPath = folderDir[folderDir.Length - 1];

            if (!folderList.Exists(x => x.LocationFolder + "\\" + x.Name == path + "\\" + endPath))
            {
                return 4; // Несуществующий путь.
            }
            try
            {
                folderList.RemoveAll(x => x.LocationFolder.Contains(sourcePath));
                fileList.RemoveAll(x => x.LocationFolder.Contains(sourcePath));
                folderList.RemoveAll(x => x.LocationFolder == path && x.Name == endPath);
            }
            catch
            {
                return 5; // Не удалось удалить папку по неизвесной причине.
            }
            return 0;
        }

        public override int Tree(string sourcePath, List<Files> fileList, List<Folders> folderList)
        {
            char[] slash = new char[] { '\\' };

            string[] folderDir = sourcePath.Split(slash);
            string path = "";
            for (int i = 0; i < folderDir.Length - 1; i++)
            {
                path += folderDir[i];
                if (i != folderDir.Length - 1)
                    path += "\\";
            }

            List<Folders> treeResult = folderList.FindAll(x => x.LocationFolder.Contains(sourcePath));
            Console.WriteLine();

            List<string[]> treeArrayList = new List<string[]>() { new string[] { "1" } };//null; ;
            foreach (var treeItem in treeResult)
            {
                string[] folderDir3 = treeItem.LocationFolder.Split(slash);
                if (folderDir3 == folderDir)
                {
                    continue;
                }
                string[] folderDir2 = new string[folderDir3.Length - folderDir.Length + 1];
                for (int i = folderDir.Length, j = 0; i < folderDir3.Length; i++, j++)
                {
                    folderDir2[j] = folderDir3[i];
                }
                treeArrayList.Add(folderDir2);
            }

            treeArrayList.RemoveAt(0);

            foreach (var item in treeArrayList)
            {
                foreach (var folder in item)
                {
                    Console.Write("\\ {0} ", folder);
                }
                Console.WriteLine();
            }

            return 0;
        }

    }
}
