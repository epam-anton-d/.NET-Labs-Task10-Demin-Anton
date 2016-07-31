using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ErrorsAndLogs3Layers.Core
{
    class BusinessLogicLayer
    {
        DataAccessLayer dataAccessLayer;
        //public List<Folders> foldersList;
        //public List<Files> filesList;
        string semicolon = ";";

        public BusinessLogicLayer()
        {
            dataAccessLayer = new DataAccessLayer();
            List<Folders> folderList = FillFoldersCollection();
            List<Files> fileList = FillFilesCollection();
        }

        public List<Files> FillFilesCollection()
        {
            string readFilesTxt = dataAccessLayer.GetFilesFromFile();
            List<Files> fileList = new List<Files>();

            try
            {
                // Заполнение коллекции файлов.
                String[] fileElements = Regex.Split(readFilesTxt, semicolon);
                for (int i = 0; i < fileElements.Length; i++)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        fileList.Add(new Files(fileElements[i - 1], fileElements[i]));
                    }
                }
            }
            catch
            {
                return null;
            }

            return fileList;
        }

        public List<Folders> FillFoldersCollection()
        {
            string readFoldersTxt = dataAccessLayer.GetFoldersFromFile();
            List<Folders> folderList = new List<Folders>();

            try
            {    
                // Заполнение коллекции папок.
                String[] folderElements = Regex.Split(readFoldersTxt, semicolon);
                for (int i = 0; i < folderElements.Length; i++)
                {
                    if ((i + 1) % 2 == 0)
                    {
                        folderList.Add(new Folders(folderElements[i - 1], folderElements[i]));
                    }
                }
            }
            catch
            {
                return null;
            }

            return folderList;
        }

        public void SaveFolderChanges(List<Folders> folderList)
        {
            dataAccessLayer.PutFoldersIntoFile(folderList);
        }

        public void SaveFileChanges(List<Files> fileList)
        {
            dataAccessLayer.PutFilesIntoFile(fileList);
        }
        
    }
}
