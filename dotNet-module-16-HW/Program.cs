using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace dotNet_module_16_HW
{
    class Program
    {
        static void Main()
        {
            string logFilePath = "log.txt";

            while (true)
            {
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Просмотр содержимого директории");
                Console.WriteLine("2. Создание файла/директории");
                Console.WriteLine("3. Удаление файла/директории");
                Console.WriteLine("4. Копирование файла/директории");
                Console.WriteLine("5. Перемещение файла/директории");
                Console.WriteLine("6. Чтение из файла");
                Console.WriteLine("7. Запись в файл");
                Console.WriteLine("0. Выход");

                if (!int.TryParse(Console.ReadLine(), out int choice))
                {
                    Console.WriteLine("Некорректный ввод. Повторите попытку.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        ListDirectoryContents();
                        break;
                    case 2:
                        CreateFileOrDirectory();
                        break;
                    case 3:
                        DeleteFileOrDirectory();
                        break;
                    case 4:
                        CopyFileOrDirectory();
                        break;
                    case 5:
                        MoveFileOrDirectory();
                        break;
                    case 6:
                        ReadFromFile();
                        break;
                    case 7:
                        WriteToFile();
                        break;
                    case 0:
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Повторите попытку.");
                        break;
                }
            }
        }

        static void ListDirectoryContents()
        {
            Console.WriteLine("Введите путь к директории:");
            string path = Console.ReadLine();

            try
            {
                string[] files = Directory.GetFiles(path);
                string[] directories = Directory.GetDirectories(path);

                Console.WriteLine("Содержимое директории:");
                Console.WriteLine("Файлы:");
                foreach (var file in files)
                {
                    Console.WriteLine(Path.GetFileName(file));
                }

                Console.WriteLine("\nДиректории:");
                foreach (var directory in directories)
                {
                    Console.WriteLine(Path.GetFileName(directory));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CreateFileOrDirectory()
        {
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Создать файл");
            Console.WriteLine("2. Создать директорию");

            if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Некорректный ввод. Повторите попытку.");
                return;
            }

            Console.WriteLine("Введите путь к директории, в которой нужно создать файл/директорию:");
            string path = Console.ReadLine();

            try
            {
                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Введите имя файла:");
                        string fileName = Console.ReadLine();
                        File.Create(Path.Combine(path, fileName));
                        Console.WriteLine("Файл успешно создан.");
                        break;
                    case 2:
                        Console.WriteLine("Введите имя директории:");
                        string directoryName = Console.ReadLine();
                        Directory.CreateDirectory(Path.Combine(path, directoryName));
                        Console.WriteLine("Директория успешно создана.");
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор. Повторите попытку.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
        // ... (предыдущий код)

        static void DeleteFileOrDirectory()
        {
            Console.WriteLine("Введите путь к файлу/директории для удаления:");
            string pathToDelete = Console.ReadLine();

            try
            {
                if (File.Exists(pathToDelete))
                {
                    File.Delete(pathToDelete);
                    Console.WriteLine("Файл успешно удален.");
                }
                else if (Directory.Exists(pathToDelete))
                {
                    Directory.Delete(pathToDelete, true);
                    Console.WriteLine("Директория успешно удалена.");
                }
                else
                {
                    Console.WriteLine("Файл/директория не найдены.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void CopyFileOrDirectory()
        {
            Console.WriteLine("Введите путь к файлу/директории для копирования:");
            string sourcePath = Console.ReadLine();

            Console.WriteLine("Введите путь, куда нужно скопировать файл/директорию:");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Copy(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Файл успешно скопирован.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.CreateDirectory(Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    foreach (var file in Directory.GetFiles(sourcePath))
                    {
                        File.Copy(file, Path.Combine(destinationPath, Path.GetFileName(file)));
                    }
                    foreach (var dir in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
                    {
                        Directory.CreateDirectory(Path.Combine(destinationPath, Path.GetFileName(dir)));
                    }
                    Console.WriteLine("Директория успешно скопирована.");
                }
                else
                {
                    Console.WriteLine("Файл/директория не найдены.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void MoveFileOrDirectory()
        {
            Console.WriteLine("Введите путь к файлу/директории для перемещения:");
            string sourcePath = Console.ReadLine();

            Console.WriteLine("Введите путь, куда нужно переместить файл/директорию:");
            string destinationPath = Console.ReadLine();

            try
            {
                if (File.Exists(sourcePath))
                {
                    File.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Файл успешно перемещен.");
                }
                else if (Directory.Exists(sourcePath))
                {
                    Directory.Move(sourcePath, Path.Combine(destinationPath, Path.GetFileName(sourcePath)));
                    Console.WriteLine("Директория успешно перемещена.");
                }
                else
                {
                    Console.WriteLine("Файл/директория не найдены.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void ReadFromFile()
        {
            Console.WriteLine("Введите путь к файлу для чтения:");
            string filePath = Console.ReadLine();

            try
            {
                if (File.Exists(filePath))
                {
                    string content = File.ReadAllText(filePath);
                    Console.WriteLine("Содержимое файла:");
                    Console.WriteLine(content);
                }
                else
                {
                    Console.WriteLine("Файл не найден.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

        static void WriteToFile()
        {
            Console.WriteLine("Введите путь к файлу для записи:");
            string filePath = Console.ReadLine();

            Console.WriteLine("Введите текст для записи:");
            string content = Console.ReadLine();

            try
            {
                File.WriteAllText(filePath, content);
                Console.WriteLine("Текст успешно записан в файл.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }

    }

}
