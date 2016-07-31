using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ErrorsAndLogs3Layers.Core;
using System.Text.RegularExpressions;
using System.Data.SQLite;

namespace ErrorsAndLogs3Layers
{
    class Program
    {
        static void Main(string[] args)
        {            
            BusinessLogicLayer businessLogicLayer = new BusinessLogicLayer();
            List<Folders> folderList = businessLogicLayer.FillFoldersCollection();
            List<Files> fileList = businessLogicLayer.FillFilesCollection();

            string read;
            string patternMkdir = "mkdir";
            string patternPathFolder = @"\sc\:(\\[A-Za-z\-_0-9^\.]+)+";
            string patternPathFile = @"\sc\:(\\[A-Za-z\-_0-9]+)+\.[a-z0-9]+";
            string patternCreate = "create";
            string patternCopy = "copy";
            string patternDelete = "delete";
            string patternMove = "move";
            string patternTree = "tree";
            string patternExit = "exit";
            int error;
            string[] commands;
            char[] space = new char[] { ' ' };

            do
            {
                Console.Write((folderList[0].Name) + "\\>");
                read = Console.ReadLine();
                read = read.Trim();

                if (Regex.IsMatch(read, patternMkdir + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = folderList[0].Create(commands[1], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternCreate + patternPathFile))
                {
                    commands = read.Split(space);
                    error = fileList[0].Create(commands[1], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternCopy + patternPathFile + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = fileList[0].Copy(commands[1], commands[2], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternCopy + patternPathFolder + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = folderList[0].Copy(commands[1], commands[2], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternDelete + patternPathFile))
                {
                    commands = read.Split(space);
                    error = fileList[0].Delete(commands[1], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternDelete + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = folderList[0].Delete(commands[1], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternMove + patternPathFile + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = fileList[0].Move(commands[1], commands[2], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternMove + patternPathFolder + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = folderList[0].Move(commands[1], commands[2], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (Regex.IsMatch(read, patternTree + patternPathFolder))
                {
                    commands = read.Split(space);
                    error = folderList[0].Tree(commands[1], fileList, folderList);
                    if (error == 0)
                    {
                        Console.WriteLine("good job!!!");
                    }
                    else
                    {
                        Console.WriteLine(error);
                    }
                }
                else if (read == patternExit)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("\"{0}\" не является внутренней или внешней\nкомандой, исполняемой программой или внешним файлом", read);
                }
            } while (read != patternExit);

            // Здесь запись в файлы.
            businessLogicLayer.SaveFileChanges(fileList);
            businessLogicLayer.SaveFolderChanges(folderList);
        }
    }
}
