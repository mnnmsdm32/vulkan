

using System;
using System.IO;

Console.InputEncoding = System.Text.Encoding.UTF8;
Console.OutputEncoding = System.Text.Encoding.UTF8;


        Console.OutputEncoding = System.Text.Encoding.UTF8;


        Console.WriteLine("=== Завдання 1: Інформація про середовище ===");
        Console.WriteLine($"Ім'я комп'ютера: {Environment.MachineName}");
        Console.WriteLine($"Ім'я поточного користувача: {Environment.UserName}");
        Console.WriteLine($"Версія операційної системи: {Environment.OSVersion}");
        Console.WriteLine($"Системний каталог Windows: {Environment.SystemDirectory}");
        Console.WriteLine($"Поточний робочий каталог: {Environment.CurrentDirectory}");
        Console.WriteLine();


        Console.WriteLine("=== Завдання 2: Робота з каталогами ===");
        string rootFolder = "MyFolder";
        string documentsFolder = Path.Combine(rootFolder, "Documents");
        string imagesFolder = Path.Combine(rootFolder, "Images");

        // Створення каталогів, якщо вони ще не існують
        Directory.CreateDirectory(rootFolder);
        Directory.CreateDirectory(documentsFolder);
        Directory.CreateDirectory(imagesFolder);

        // Перевірка існування та виведення повідомлень
        Console.WriteLine($"Каталог '{rootFolder}' існує: {Directory.Exists(rootFolder)}");
        Console.WriteLine($"Каталог '{documentsFolder}' існує: {Directory.Exists(documentsFolder)}");
        Console.WriteLine($"Каталог '{imagesFolder}' існує: {Directory.Exists(imagesFolder)}");
        Console.WriteLine();

        Console.WriteLine("=== Завдання 3: Робота з файлами ===");
        string infoFilePath = Path.Combine(documentsFolder, "info.txt");

        string content = "Ім'я: Владислав\n" +
                         "Прізвище: Кравчук\n" +
                         "Навчальна група: КН-21\n" +
                         $"Дата створення: {DateTime.Now}";

        File.WriteAllText(infoFilePath, content);

        if (File.Exists(infoFilePath))
        {
            Console.WriteLine("Файл 'info.txt' успішно створено. Вміст файлу:");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine(File.ReadAllText(infoFilePath));
            Console.WriteLine("---------------------------------------------");
        }
        else
        {
            Console.WriteLine("Помилка: файл не створено.");
        }
        Console.WriteLine();


        Console.WriteLine("=== Завдання 4: Перегляд файлової системи ===");
        Console.Write("Введіть шлях до каталогу для перегляду (наприклад, MyFolder): ");
        string? userPath = Console.ReadLine();

        if (!string.IsNullOrEmpty(userPath) && Directory.Exists(userPath))
        {
            Console.WriteLine($"\nВміст каталогу '{userPath}':");

            Console.WriteLine("Підкаталоги:");
            foreach (var dir in Directory.GetDirectories(userPath))
            {
                Console.WriteLine($"  [Папка] {dir}");
            }

            Console.WriteLine("Файли:");
            foreach (var file in Directory.GetFiles(userPath))
            {
                Console.WriteLine($"  [Файл] {file}");
            }
        }
        else
        {
            Console.WriteLine("Помилка: вказаний каталог не існує або шлях порожній.");
        }
        Console.WriteLine();

        Console.WriteLine("=== Завдання 5: Копіювання та переміщення файлів ===");
        string targetCopyPath = Path.Combine(imagesFolder, "info_copy.txt");
        string finalMovedPath = Path.Combine(rootFolder, "info_copy.txt");

        if (File.Exists(infoFilePath) && Directory.Exists(imagesFolder))
        {
            // Копіюємо з перейменуванням
            File.Copy(infoFilePath, targetCopyPath, overwrite: true);
            Console.WriteLine("Файл 'info.txt' скопійовано та перейменовано на 'info_copy.txt' у пакy Images.");

            if (File.Exists(targetCopyPath))
            {
                // Переміщуємо копію до каталогу MyFolder
                File.Move(targetCopyPath, finalMovedPath, overwrite: true);
                Console.WriteLine("Файл 'info_copy.txt' успішно переміщено до каталогу MyFolder.");
            }
        }
        else
        {
            Console.WriteLine("Помилка: вихідний файл або каталог призначення відсутні.");
        }
        Console.WriteLine();

        Console.WriteLine("=== Завдання 6: Видалення файлів і каталогів ===");
        Console.Write("Введіть шлях до файлу або каталогу для видалення: ");
        string? deletePath = Console.ReadLine();

        if (!string.IsNullOrEmpty(deletePath))
        {
            if (File.Exists(deletePath))
            {
                Console.Write($"Ви дійсно хочете видалити файл '{deletePath}'? (y/n): ");
                if (Console.ReadLine()?.Trim().ToLower() == "y")
                {
                    File.Delete(deletePath);
                    Console.WriteLine("Файл успішно видалено.");
                }
            }
            else if (Directory.Exists(deletePath))
            {
                Console.Write($"Ви дійсно хочете видалити каталог '{deletePath}'? (y/n): ");
                if (Console.ReadLine()?.Trim().ToLower() == "y")
                {
              
                    string[] files = Directory.GetFiles(deletePath);
                    string[] subDirs = Directory.GetDirectories(deletePath);

                    if (files.Length == 0 && subDirs.Length == 0)
                    {
                        Directory.Delete(deletePath);
                        Console.WriteLine("Каталог успішно видалено.");
                    }
                    else
                    {
                        Console.WriteLine("Помилка: Каталог не порожній. Автоматичне видалення всього вмісту заблоковано в цілях безпеки.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Помилка: Об'єкт за вказаним шляхом не знайдено.");
            }
        }