// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Text;

Console.InputEncoding = Encoding.UTF8;
Console.OutputEncoding = Encoding.UTF8;

Console.WriteLine("-- Працюємо із двійковими файлами :) --");

Console.WriteLine("Вкажіть назву папки:");
string folderName = Console.ReadLine();
Directory.CreateDirectory(folderName);

// Лічильник успішно завантажених файлів
int successCount = 0;

do
{
    Console.WriteLine("Вкажіть адресу url (або натисніть Enter для завершення):");
    string fileUrl = Console.ReadLine();
    if (string.IsNullOrEmpty(fileUrl))
        break;

    try
    {
        HttpClient client = new HttpClient();

        // Вимірюємо час завантаження за допомогою Stopwatch
        Stopwatch stopwatch = Stopwatch.StartNew();
        byte[] buffer = client.GetByteArrayAsync(fileUrl).Result;
        stopwatch.Stop();

        // Генеруємо унікальну назву та формуємо шлях
        string fileName = Guid.NewGuid().ToString() + ".webp";
        string path = Path.Combine(folderName, fileName);

        // Зберігаємо файл
        File.WriteAllBytes(path, buffer);
        successCount++;

        // 1. Звіт по часу завантаження поточного файлу
        Console.WriteLine($"[Успіх] Файл збережено як {fileName}. Час завантаження: {stopwatch.ElapsedMilliseconds} мс.");
    }
    catch (Exception ex)
    {
        Console.WriteLine("Помилка читання файлу: " + ex.Message);
    }
} while (true);

// 2. Виводимо загальну кількість успішно завантажених файлів
Console.WriteLine($"\n Загалом успішно завантажено файлів: {successCount}");

// 3. Показуємо вміст папки та розмір кожного файлу у Кілобайтах
Console.WriteLine($"\n Вміст папки '{folderName}':");
string[] files = Directory.GetFiles(folderName);

if (files.Length == 0)
{
    Console.WriteLine("Папка порожня.");
}
else
{
    foreach (string filePath in files)
    {
        FileInfo fileInfo = new FileInfo(filePath);
        // Переводимо байти у Кілобайти (ділимо на 1024.0 для отримання дробового значення)
        double sizeInKb = fileInfo.Length / 1024.0;

        Console.WriteLine($"- {fileInfo.Name} | Розмір: {sizeInKb:F2} КБ");
    }
}