using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErrorsAndLogs3Layers.Core
{
    internal class Files : FileSystem
    {
        public Files(string name, string locationFolder)
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
                if (folderDir[i] != "")
                    path += folderDir[i];
                if (i != folderDir.Length - 2)
                    path += "\\";
            }
            string endPath = folderDir[folderDir.Length - 1];

            if (!folderList.Exists(x => x.LocationFolder + "\\" + x.Name == path) && path != "c:")
            {
                return 1; // Несуществующий путь.
            }

            if (fileList.Exists(x => x.LocationFolder + "\\" + x.Name == path + "\\" + endPath))
            {
                return 2;
            }

            try
            {
                fileList.Add(new Files(endPath, path));//folderDir[folderDir.Length - 2]));
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

            if (!fileList.Exists(x => x.LocationFolder + "\\" + x.Name == path + "\\" + endPath))
            {
                return 4; // Несуществующий путь.
            }

            if (!folderList.Exists(x => x.LocationFolder + "\\" + x.Name == destinationPath))
            {
                return 5; // Несуществующий путь.
            }

            try
            {
                fileList.Add(new Files(endPath, destinationPath));
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

            if (!fileList.Exists(x => x.LocationFolder + "\\" + x.Name == path + "\\" + endPath))
            {
                return 4; // Несуществующий путь.
            }
            try
            {
                fileList.RemoveAll(x => x.LocationFolder == path && x.Name == endPath);
            }
            catch
            {
                return 5; // Не удалось удалить папку по неизвесной причине.
            }

            return 0;
        }

        public override int Tree(string sourcePath, List<Files> fileList, List<Folders> folderList)
        {
            return 0;
        }
    }
}
