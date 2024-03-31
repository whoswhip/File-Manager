using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Reflection;
using PasswordGen;
using File_Managing;


namespace File_Managing
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "File Managing";
            Console.WriteLine("___________.__.__              _____                                             \r\n\\_   _____/|__|  |   ____     /     \\ _____    ____ _____     ____   ___________ \r\n |    __)  |  |  | _/ __ \\   /  \\ /  \\\\__  \\  /    \\\\__  \\   / ___\\_/ __ \\_  __ \\\r\n |     \\   |  |  |_\\  ___/  /    Y    \\/ __ \\|   |  \\/ __ \\_/ /_/  >  ___/|  | \\/\r\n \\___  /   |__|____/\\___  > \\____|__  (____  /___|  (____  /\\___  / \\___  >__|   \r\n     \\/                 \\/          \\/     \\/     \\/     \\//_____/      \\/       ");
            Console.WriteLine("Choose an option:");
            Console.WriteLine("[1] File Renamer");
            Console.WriteLine("[2] Duplicate File Detector");
            Console.WriteLine("[3] Multiple Gallery-dl's");
            Console.WriteLine("[4] Files to Webhook");
            Console.WriteLine("[5] Generators");
            Console.WriteLine("[6] Credits");
            Console.WriteLine("[7] Exit");
            Console.Write("Option: ");
            string option = Console.ReadLine();

            switch (option)
            {
                case "1":
                    FileRenamer.Run(args);
                    break;
                case "2":
                    DupeDTC.Run(args);
                    break;
                case "3":
                    gallerydl.run(args);
                    break;
                case "4":
                    FilestoWebhook.Run(args);
                    break;
                case "5":
                    Generators.Run(args);
                    break;
                case "6":
                    Credits.PrintCredits();
                    break;
                case "7":
                    Console.WriteLine("Exiting...");
                    Thread.Sleep(250);
                    break;
                default:
                    Console.WriteLine("Invalid option");
                    break;
            }
        }
        public static void Restart(string[] args)
        {
            Main(args);
        }
    }
    class Credits
    {
        public static void PrintCredits()
        {
            Console.Title = "Credits";
            Console.Clear();
            Console.WriteLine("_________                    .___.__  __          \r\n\\_   ___ \\_______   ____   __| _/|__|/  |_  ______\r\n/    \\  \\/\\_  __ \\_/ __ \\ / __ | |  \\   __\\/  ___/\r\n\\     \\____|  | \\/\\  ___// /_/ | |  ||  |  \\___ \\ \r\n \\______  /|__|    \\___  >____ | |__||__| /____  >\r\n        \\/             \\/     \\/               \\/ ");
            Console.WriteLine("File Renamer & Multiple Gallery-dl's: Whoswhip");
            Console.WriteLine("Duplicate File Detector: Themida");
            Console.WriteLine("Gallery-dl: Mike Fährmann https://github.com/mikf/gallery-dl");
            Console.WriteLine("[1] Go Back");
            Console.Write("Option: ");
            string choice = Console.ReadLine();
            if (choice == "1")
            {
                Console.Clear();
                Program.Restart(null);
            }
            else
            {
                Console.WriteLine("Invalid option");
            }

        }
    }
    class FileRenamer
    {
        public static void Run(string[] args)
        {
            bool continueRenaming = true;

            while (continueRenaming)
            {
                Console.Title = "File Renamer";
                Console.Clear();
                Console.WriteLine("___________.__ .__            __________                                                  \r\n\\_   _____/|__||  |    ____   \\______   \\  ____    ____  _____     _____    ____  _______ \r\n |    __)  |  ||  |  _/ __ \\   |       _/_/ __ \\  /    \\ \\__  \\   /     \\ _/ __ \\ \\_  __ \\\r\n |     \\   |  ||  |__\\  ___/   |    |   \\\\  ___/ |   |  \\ / __ \\_|  Y Y  \\\\  ___/  |  | \\/\r\n \\___  /   |__||____/ \\___  >  |____|_  / \\___  >|___|  /(____  /|__|_|  / \\___  > |__|   \r\n     \\/                   \\/          \\/      \\/      \\/      \\/       \\/      \\/         ");

                Console.Write("Enter the folder path: ");
                string? folderPath = Console.ReadLine();

                if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
                {
                    Console.WriteLine("Folder does not exist!");
                    continue;
                }

                Console.WriteLine("Choose the renaming method:");
                Console.WriteLine("[1] Randomize file names");
                Console.WriteLine("[2] Incremental file names");
                Console.WriteLine("[3] Randomize and then Incremental");
                Console.WriteLine("[4] Prefixed Incremental");
                Console.WriteLine("[5] Prefixed Randomized");
                Console.WriteLine("[6] Prefixed Randomized and then Incremental");
                Console.Write("Option: ");
                string? choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        RandomizeFileNames(folderPath);
                        break;
                    case "2":
                        IncrementalFileNames(folderPath);
                        break;
                    case "3":
                        RandomizeFileNames(folderPath);
                        IncrementalFileNames(folderPath);
                        break;
                    case "4":
                        PrefixedIncrementalFileNames(folderPath);
                        break;
                    case "5":
                        PrefixedRandomizedFileNames(folderPath);
                        break;
                    case "6":
                        PrefixedRandomandIncremental(folderPath);
                        break;
                    default:
                        Console.WriteLine("Invalid choice!");
                        Thread.Sleep(250);
                        Run(null);
                        break;
                }

                Console.WriteLine("File renaming completed!");

                Console.Write("Do you want to rename more files? (Y/N): ");
                string? continueChoice = Console.ReadLine();
                continueRenaming = (continueChoice?.ToUpper() == "Y");
                Console.Clear();
                if (!continueRenaming)
                {
                    Console.Clear();
                    Program.Restart(args);
                }
            }
        }

        static void RandomizeFileNames(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);

            Random random = new Random();
            int randomizedCount = 0; 

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string fileExtension = Path.GetExtension(file);
                string randomName = Path.GetRandomFileName();
                string newFileName = randomName.Replace(".", "") + fileExtension;
                string newFilePath = Path.Combine(folderPath, newFileName);
                File.Move(file, newFilePath);
                randomizedCount++; 
            }

            Console.WriteLine($"Randomized names for {randomizedCount} files.");
        }

        static void IncrementalFileNames(string folderPath)
        {
            string[] files = Directory.GetFiles(folderPath);

            int count = 1;
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string fileExtension = Path.GetExtension(file);
                string newFileName = $"{count}{fileExtension}";
                string newFilePath = Path.Combine(folderPath, newFileName);

                // Check if the new file already exists
                if (File.Exists(newFilePath))
                {
                    Console.WriteLine($"File '{newFileName}' already exists. Skipping...");
                    continue;
                }

                File.Move(file, newFilePath);
                count++;
            }

            Console.WriteLine($"Renamed {count - 1} files.");
        }
        
        static void PrefixedIncrementalFileNames(string folderPath)
        {
            Console.Write("Enter the prefix: ");
            string prefix = Console.ReadLine();

            string[] files = Directory.GetFiles(folderPath);

            int count = 1;
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string fileExtension = Path.GetExtension(file);
                string newFileName = $"{prefix}{count}{fileExtension}";
                string newFilePath = Path.Combine(folderPath, newFileName);

                // Check if the new file already exists
                if (File.Exists(newFilePath))
                {
                    Console.WriteLine($"File '{newFileName}' already exists. Skipping...");
                    continue;
                }

                File.Move(file, newFilePath);
                count++;
            }

            Console.WriteLine($"Renamed {count - 1} files.");
        }   

        static void PrefixedRandomizedFileNames(string folderPath)
        {
            Console.Write("Enter the prefix: ");
            string prefix = Console.ReadLine();

            string[] files = Directory.GetFiles(folderPath);

            Random random = new Random();
            int randomizedCount = 0; 

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string fileExtension = Path.GetExtension(file);
                string randomName = Path.GetRandomFileName();
                string newFileName = $"{prefix}{randomName.Replace(".", "")}{fileExtension}";
                string newFilePath = Path.Combine(folderPath, newFileName);
                File.Move(file, newFilePath);
                randomizedCount++; 
            }

            Console.WriteLine($"Randomized names for {randomizedCount} files.");
        }   

        static void PrefixedRandomandIncremental(string folderPath)
        {
            Console.Write("Enter the prefix: ");
            string prefix = Console.ReadLine();

            string[] files = Directory.GetFiles(folderPath);

            Random random = new Random();
            int randomizedCount = 0; 

            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string fileExtension = Path.GetExtension(file);
                string randomName = Path.GetRandomFileName();
                string newFileName = $"{prefix}{randomName.Replace(".", "")}{fileExtension}";
                string newFilePath = Path.Combine(folderPath, newFileName);
                File.Move(file, newFilePath);
                randomizedCount++; 
            }

            Console.WriteLine($"Randomized names for {randomizedCount} files.");
            files = Directory.GetFiles(folderPath);
            int count = 1;
            foreach (string file in files)
            {
                string fileName = Path.GetFileName(file);
                string fileExtension = Path.GetExtension(file);
                string newFileName = $"{prefix}{count}{fileExtension}";
                string newFilePath = Path.Combine(folderPath, newFileName);

                if (File.Exists(newFilePath))
                {
                    Console.WriteLine($"File '{newFileName}' already exists. Skipping...");
                    continue;
                }

                File.Move(file, newFilePath);
                count++;
            }

            Console.WriteLine($"Renamed {count - 1} files.");
        }   
    }

    class DupeDTC
    {
        private static string CalculateSha256(string filePath)
        {
            using (var sha256 = SHA256.Create())
            {
                using (var fileStream = File.OpenRead(filePath))
                {
                    var hash = sha256.ComputeHash(fileStream);
                    return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
                }
            }
        }

        private static List<Tuple<string, string>> FindDuplicateFiles(string directory)
        {
            var fileHashMap = new Dictionary<string, string>();
            var duplicateFiles = new List<Tuple<string, string>>();

            foreach (var filePath in Directory.GetFiles(directory, "*", SearchOption.AllDirectories))
            {
                var fileHash = CalculateSha256(filePath);
                if (fileHashMap.ContainsKey(fileHash))
                {
                    duplicateFiles.Add(new Tuple<string, string>(filePath, fileHashMap[fileHash]));
                }
                else
                {
                    fileHashMap[fileHash] = filePath;
                }
            }

            return duplicateFiles;
        }

        private static void DeleteDuplicateFiles(List<Tuple<string, string>> duplicateFiles)
        {
            foreach (var filePair in duplicateFiles)
            {
                try
                {
                    Console.WriteLine($"Deleting {filePair.Item2}");
                    File.Delete(filePair.Item2);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error deleting file {filePair.Item2}: {e.Message}");
                }
            }
        }

        public static void Run(string[] args)
        {
            Console.Title = "Duplicate File Detector";
            Console.Clear();
            Console.WriteLine("________                        ___________.__.__           ________          __                 __                 \r\n\\______ \\  __ ________   ____   \\_   _____/|__|  |   ____   \\______ \\   _____/  |_  ____   _____/  |_  ___________  \r\n |    |  \\|  |  \\____ \\_/ __ \\   |    __)  |  |  | _/ __ \\   |    |  \\_/ __ \\   __\\/ __ \\_/ ___\\   __\\/  _ \\_  __ \\ \r\n |    `   \\  |  /  |_> >  ___/   |     \\   |  |  |_\\  ___/   |    `   \\  ___/|  | \\  ___/\\  \\___|  | (  <_> )  | \\/ \r\n/_______  /____/|   __/ \\___  >  \\___  /   |__|____/\\___  > /_______  /\\___  >__|  \\___  >\\___  >__|  \\____/|__|    \r\n        \\/      |__|        \\/       \\/                 \\/          \\/     \\/          \\/     \\/                    ");
            Console.WriteLine("Enter the folder path: ");
            var directory = Console.ReadLine();
            CancellationTokenSource cts = new CancellationTokenSource();
            var loading = new Thread(() =>
            {
                var chars = new[] { '/', '-', '\\', '|' };
                int x = 0;
                while (!cts.Token.IsCancellationRequested)
                {
                    Console.Write("\r{0} Finding duplicate files...", chars[x++ % chars.Length]);
                    Thread.Sleep(150);
                }
            })
            { IsBackground = true };
            loading.Start();

            var duplicateFiles = FindDuplicateFiles(directory);

            cts.Cancel();
            int counter = 0;


            if (duplicateFiles.Count > 0)
            {
                Console.WriteLine("Duplicate files found:");
                foreach (var filePair in duplicateFiles)
                {
                    Console.WriteLine($"{filePair.Item1} and {filePair.Item2}");
                    counter++;
                }

                Console.Write(" \r\nDo you want to delete " + counter + " duplicate files? (y/n): ");
                var userConfirmation = Console.ReadLine()?.ToLower();
                if (userConfirmation == "y")
                {
                    DeleteDuplicateFiles(duplicateFiles);
                    Console.WriteLine("Duplicate files deleted.");
                    if (counter == 1)
                    {
                        Console.WriteLine($"{counter} file deleted.");
                    }
                    else
                    {
                        Console.WriteLine($"{counter} files deleted.");
                    }
                }
                else
                {

                    Console.WriteLine("No files deleted.");
                }
            }
            else
            {

                Console.WriteLine("No duplicate files found.");
            }

            Console.Write("Do you want to go again? (y/n): ");
            var goAgain = Console.ReadLine()?.ToLower();
            if (goAgain == "y")
            {
                Console.Clear();
                Run(args);
            }
            else
            {
                Console.Clear();
                Program.Restart(args);
            }
        }
    }

    class gallerydl
    {
        static int count;

        public static void run(string[] args)
        {
            bool loop = true;
            while (loop) {
                Console.Clear();
                Console.Title = "Multiple Gallery-DL's";
                Console.WriteLine("  ________       .__  .__                                ________  .____     \r\n /  _____/_____  |  | |  |   ___________ ___.__.         \\______ \\ |    |    \r\n/   \\  ___\\__  \\ |  | |  | _/ __ \\_  __ <   |  |  ______  |    |  \\|    |    \r\n\\    \\_\\  \\/ __ \\|  |_|  |_\\  ___/|  | \\/\\___  | /_____/  |    `   \\    |___ \r\n \\______  (____  /____/____/\\___  >__|   / ____|         /_______  /_______ \\\r\n        \\/     \\/               \\/       \\/                      \\/        \\/");
                Console.Write("Enter the directory path: ");
                string directoryPath = Console.ReadLine() ?? string.Empty;
                Console.WriteLine("How many images/videos should be downloaded? (0=Until all are downloaded): ");
                int amount = int.Parse(Console.ReadLine() ?? "0");
                Console.Write("Enter the number of urls you want to scrape: ");
                int numCommands = int.Parse(Console.ReadLine() ?? "0");

                List<string> urls = new List<string>();

                for (int i = 0; i < numCommands; i++)
                {
                    Console.WriteLine($"Enter the URL #{i + 1}: ");
                    string url = Console.ReadLine() ?? string.Empty;
                }

                List<Task> tasks = new List<Task>();
                if (amount == 0)
                {
                    foreach (string url in urls)
                    {
                        string command = "gallery-dl " + '"' + url + '"';
                        Task task = Task.Run(() => RunCommand(directoryPath, command));
                        tasks.Add(task);
                    }
                }
                else
                {
                    foreach (string url in urls)
                    {
                        string command = "gallery-dl " + '"' + url + '"';
                        Task task = Task.Run(() => RunCommandNumbered(directoryPath, command, amount));
                        tasks.Add(task);
                    }
                }


                Task.WaitAll(tasks.ToArray());

                count = count - numCommands;
                Console.WriteLine($" \r\n Total files downloaded: {count}");
                Console.Write("Do you want to go again? (y/n): ");
                var goAgain = Console.ReadLine()?.ToLower();
                if (goAgain == "y")
                {
                    Console.Clear();
                    loop = true;
                }
                else
                {
                    loop = false;
                    Console.Clear();
                    Program.Restart(args);
                }
            }
        }

        static void RunCommand(string directoryPath, string command)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = directoryPath
            };

            Process process = new Process { StartInfo = startInfo };
            process.Start();

            process.StandardInput.WriteLine(command);
            process.StandardInput.Close();
            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null && e.Data.Contains("gallery-dl"))
                {
                    count++;
                }
                Console.WriteLine(e.Data);
            };

            process.BeginOutputReadLine();
            process.WaitForExit();
        }
        static void RunCommandNumbered(string directoryPath, string command, int amount)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true,
                WorkingDirectory = directoryPath
            };

            Process process = new Process { StartInfo = startInfo };
            process.Start();

            process.StandardInput.WriteLine(command);
            process.StandardInput.Close();
            process.OutputDataReceived += (sender, e) =>
            {
                if (e.Data != null && e.Data.Contains("gallery-dl"))
                {
                    if (!e.Data.StartsWith("#")) 
                    {
                        if (count >= amount)
                        {
                            process.CloseMainWindow();
                            process.Close();
                        }
                        count++;
                    }

                }
                Console.WriteLine(e.Data);
            };

            process.BeginOutputReadLine();
            process.WaitForExit();
        }
    }

    class FilestoWebhook

    {
        public static void Run(string[] args) 
        { 
            Console.Clear();
            Console.Title = "Files to Webhook";
            Console.WriteLine("___________.__.__              _____                                             \r\n\\_   _____/|__|  |   ____     /     \\ _____    ____ _____     ____   ___________ \r\n |    __)  |  |  | _/ __ \\   /  \\ /  \\\\__  \\  /    \\\\__  \\   / ___\\_/ __ \\_  __ \\\r\n |     \\   |  |  |_\\  ___/  /    Y    \\/ __ \\|   |  \\/ __ \\_/ /_/  >  ___/|  | \\/\r\n \\___  /   |__|____/\\___  > \\____|__  (____  /___|  (____  /\\___  / \\___  >__|   \r\n     \\/                 \\/          \\/     \\/     \\/     \\//_____/      \\/       ");
            Console.Write("Enter the directory path: ");
            string directoryPath = Console.ReadLine();
            Console.Write("Enter the webhook URL: ");
            string webhookUrl = Console.ReadLine();
            SendFilesToWebhook(directoryPath, webhookUrl).Wait();
            Console.WriteLine("Files sent to webhook.");
            Console.Write("Do you want to go again? (y/n): ");
            var goAgain = Console.ReadLine()?.ToLower();
            if (goAgain == "y")
            {
                Console.Clear();
                Run(args);
            }
            else
            {
                Console.Clear();
                Program.Restart(args);
            }
        }
        public static async Task SendFilesToWebhook(string directoryPath, string webhookUrl)
        {
            string[] files = Directory.GetFiles(directoryPath);

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (fileInfo.Length <= 25 * 1024 * 1024) // Check if file size is 25MB or lower
                {
                    using (HttpClient client = new HttpClient())
                    {
                        using (MultipartFormDataContent formData = new MultipartFormDataContent())
                        {
                            using (FileStream fileStream = File.OpenRead(file))
                            {
                                Console.WriteLine($"Sending {fileInfo.Name} to webhook...");
                                formData.Add(new StreamContent(fileStream), "file", fileInfo.Name);
                                await client.PostAsync(webhookUrl, formData);
                            }
                        }
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"File {fileInfo.Name} is too large to send to webhook.");
                    Console.ResetColor();
                }
            }
        }
    }

    class Generators
    {
        public static void Run(string[] args)
        {
            Console.Title = "Generators";
            Console.Clear();
            Console.WriteLine("___________.__.__              _____                                             \r\n\\_   _____/|__|  |   ____     /     \\ _____    ____ _____     ____   ___________ \r\n |    __)  |  |  | _/ __ \\   /  \\ /  \\\\__  \\  /    \\\\__  \\   / ___\\_/ __ \\_  __ \\\r\n |     \\   |  |  |_\\  ___/  /    Y    \\/ __ \\|   |  \\/ __ \\_/ /_/  >  ___/|  | \\/\r\n \\___  /   |__|____/\\___  > \\____|__  (____  /___|  (____  /\\___  / \\___  >__|   \r\n     \\/                 \\/          \\/     \\/     \\/     \\//_____/      \\/       ");
            Console.WriteLine("[1] Password Generator");
            Console.WriteLine("[2] Username Generator");
            Console.WriteLine("[3] Go Back");

            Console.Write("Option: ");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    PasswordGen.Program.MainGen(args);
                    
                    break;
                case "2":
                    GenerateUsername();
                    break;
                case "3":
                    Console.Clear();
                    Program.Restart(null);
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    Thread.Sleep(250);
                    Console.Clear();
                    Run(null);
                    break;
            }
            
        }
        public static void PasswordGenerator()
        {
            bool continueGenerating = true;
            while (continueGenerating) { 
            Console.Write("Enter the length of the password: ");
            int length = int.Parse(Console.ReadLine());
            Console.Write("Enter the number of passwords to generate: ");
            int numPasswords = int.Parse(Console.ReadLine());
            Console.Write("Include uppercase letters? (y/n): ");
            bool includeUppercase = Console.ReadLine()?.ToLower() == "y";
            Console.Write("Include lowercase letters? (y/n): ");
            bool includeLowercase = Console.ReadLine()?.ToLower() == "y";
            Console.Write("Include numbers? (y/n): ");
            bool includeNumbers = Console.ReadLine()?.ToLower() == "y";
            Console.Write("Include special characters? (y/n): ");
            bool includeSpecial = Console.ReadLine()?.ToLower() == "y";

            string chars = "";
            if (includeUppercase)
            {
                chars += "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            }
            if (includeLowercase)
            {
                chars += "abcdefghijklmnopqrstuvwxyz";
            }
            if (includeNumbers)
            {
                chars += "0123456789";
            }
            if (includeSpecial)
            {
                chars += "!@#$%^&*_+-=?";
            }

            Random random = new Random();
            for (int i = 0; i < numPasswords; i++)
            {
                string password = new string(Enumerable.Repeat(chars, length)
                                     .Select(s => s[random.Next(s.Length)]).ToArray());
                Console.WriteLine(password);

            }
            Console.Write("Do you want to generate more passwords? (y/n): ");
                string choice = Console.ReadLine()?.ToLower();
                if (choice == "n")
                {
                    continueGenerating = false;
                    Console.Clear();
                    Generators.Run(null);
                }
            }

        }
        public static async Task GenerateUsername()
        {
            bool continueGenerating = true;
            while (continueGenerating) { 
            Console.Write("Enter the number of words in the username: ");
            int numWords = int.Parse(Console.ReadLine());

            Console.Write("Include prefix? (y/n): ");
            bool includePrefix = Console.ReadLine()?.ToLower() == "y";

            Console.Write("Include numbers at the end? (y/n): ");
            bool includeNumbers = Console.ReadLine()?.ToLower() == "y";

               
                var resourceName = "File_Managing.words.txt";

                var assembly = Assembly.GetExecutingAssembly();

                string[] words;
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                string result = reader.ReadToEnd();
                words = result.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            }

            Random random = new Random();
            string username = "";

            if (includePrefix)
            {
                string[] emotions = { "happy", "sad", "angry", "excited", "calm", "brave", "shy", "proud", "confused", "curious" };
                string prefix = emotions[random.Next(emotions.Length)];
                username += prefix;
            }

            for (int i = 0; i < numWords; i++)
            {
                string word = words[random.Next(words.Length)];
                username += word;
            }

            if (includeNumbers)
            {
                int randomNumber = random.Next(1000, 9999);
                username += randomNumber;
            }

            Console.WriteLine("Generated username: " + username);
            Console.Write("Do you want to generate another username? (y/n): ");
            string choice = Console.ReadLine()?.ToLower();
            if (choice == "n")
             {
                    continueGenerating = false;
                    Console.Clear();
                    Generators.Run(null);
             }
            }
        }


    }

}
namespace PasswordGen
{
    class PasswordGenerator
    {
        private const string LowercaseChars = "abcdefghijklmnopqrstuvwxyz";
        private const string UppercaseChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string NumberChars = "0123456789";
        private const string SymbolChars = "!@#$%^&*";

        public static string GeneratePassword(int length, bool includeSymbols, bool includeNumbers, bool includeLowercase, bool includeUppercase)
        {
            string validChars = "";

            if (includeSymbols)
                validChars += SymbolChars;
            if (includeNumbers)
                validChars += NumberChars;
            if (includeLowercase)
                validChars += LowercaseChars;
            if (includeUppercase)
                validChars += UppercaseChars;

            if (validChars.Length == 0)
            {
                throw new ArgumentException("At least one character type must be selected.");
            }

            using (var rng = new RNGCryptoServiceProvider())
            {
                byte[] randomBytes = new byte[length];
                rng.GetBytes(randomBytes);

                char[] password = new char[length];
                for (int i = 0; i < length; i++)
                {
                    password[i] = validChars[randomBytes[i] % validChars.Length];
                }

                return new string(password);
            }
        }
    }

    class Program
    {
        public static void MainGen(string[] args)
        {

            Console.Clear();
            Console.Title = "Password Generator";
            Console.WriteLine("___________.__.__              _____                                             \r\n\\_   _____/|__|  |   ____     /     \\ _____    ____ _____     ____   ___________ \r\n |    __)  |  |  | _/ __ \\   /  \\ /  \\\\__  \\  /    \\\\__  \\   / ___\\_/ __ \\_  __ \\\r\n |     \\   |  |  |_\\  ___/  /    Y    \\/ __ \\|   |  \\/ __ \\_/ /_/  >  ___/|  | \\/\r\n \\___  /   |__|____/\\___  > \\____|__  (____  /___|  (____  /\\___  / \\___  >__|   \r\n     \\/                 \\/          \\/     \\/     \\/     \\//_____/      \\/       ");
            Console.WriteLine("-------------------------");
            Console.WriteLine("[1] Normal");
            Console.WriteLine("[2] Randomized Length");
            Console.WriteLine("[3] Go Back");
            Console.Write("Option: ");
            string? choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Normal.Run(args);
                    break;
                case "2":
                    Randomlength.Run(args);
                    break;
                case "3":
                    File_Managing.Generators.Run(args);
                    break;
                default:
                    Console.WriteLine("Invalid choice!");
                    Thread.Sleep(250);
                    MainGen(args);
                    break;
            }
        }
        public static void Restart(string[] args)
        {
            MainGen(args);
        }
    }
    class Savetofile
    {
        public static void Run(string password)
        {
            string directory = Environment.CurrentDirectory;
            string fileName = "Generated_Password.txt";
            string filePath = Path.Combine(directory, fileName);
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.Write(password);
            }
            Console.WriteLine($"Saved to {filePath}");
            File.Open(filePath, FileMode.Open);
        }
    }
    class Normal
    {
        public static void Run(string[] args)
        {
            bool generateAnother = true;

            while (generateAnother)
            {
                Console.Clear();
                Console.Title = "Password Generator";
                Console.WriteLine("___________.__.__              _____                                             \r\n\\_   _____/|__|  |   ____     /     \\ _____    ____ _____     ____   ___________ \r\n |    __)  |  |  | _/ __ \\   /  \\ /  \\\\__  \\  /    \\\\__  \\   / ___\\_/ __ \\_  __ \\\r\n |     \\   |  |  |_\\  ___/  /    Y    \\/ __ \\|   |  \\/ __ \\_/ /_/  >  ___/|  | \\/\r\n \\___  /   |__|____/\\___  > \\____|__  (____  /___|  (____  /\\___  / \\___  >__|   \r\n     \\/                 \\/          \\/     \\/     \\/     \\//_____/      \\/       ");
                Console.WriteLine("-------------------------");
                Console.Write("Print out password? (y/n): ");
                bool quick = Console.ReadLine().ToLower() == "y";
                Console.WriteLine("-------------------------");
                Console.Write("Enter the length of the password: ");
                int length = int.Parse(Console.ReadLine());

                Console.Write("Include symbols? (y/n): ");
                bool includeSymbols = Console.ReadLine().ToLower() == "y";

                Console.Write("Include numbers? (y/n): ");
                bool includeNumbers = Console.ReadLine().ToLower() == "y";

                Console.Write("Include lowercase characters? (y/n): ");
                bool includeLowercase = Console.ReadLine().ToLower() == "y";

                Console.Write("Include uppercase characters? (y/n): ");
                bool includeUppercase = Console.ReadLine().ToLower() == "y";

                string password = PasswordGenerator.GeneratePassword(length, includeSymbols, includeNumbers, includeLowercase, includeUppercase);
                if (quick)
                {
                    Console.WriteLine($"Generated Password: {password}");
                }

                Console.Write("Save to file? (y/n): ");
                bool save = Console.ReadLine().ToLower() == "y";
                if (save)
                {
                    Savetofile.Run(password);
                }

                Console.Write("Generate another password? (y/n): ");
                generateAnother = Console.ReadLine().ToLower() == "y";
                if (!generateAnother)
                {
                    Console.Clear();
                    Program.Restart(args);
                }
            }
        }
    }
    class Randomlength
    {
        public static void Run(string[] args)
        {
            bool generateAnother2 = true;



            while (generateAnother2)
            {
                Console.Clear();
                Console.Title = "Password Generator";
                Console.WriteLine("___________.__.__              _____                                             \r\n\\_   _____/|__|  |   ____     /     \\ _____    ____ _____     ____   ___________ \r\n |    __)  |  |  | _/ __ \\   /  \\ /  \\\\__  \\  /    \\\\__  \\   / ___\\_/ __ \\_  __ \\\r\n |     \\   |  |  |_\\  ___/  /    Y    \\/ __ \\|   |  \\/ __ \\_/ /_/  >  ___/|  | \\/\r\n \\___  /   |__|____/\\___  > \\____|__  (____  /___|  (____  /\\___  / \\___  >__|   \r\n     \\/                 \\/          \\/     \\/     \\/     \\//_____/      \\/       ");
                Console.WriteLine("-------------------------");
                Random rand = new Random();
                int length = rand.Next(10, 10000);
                Console.WriteLine($"Length = {length}");
                Console.WriteLine("-------------------------");
                Console.Write("Print out password? (y/n): ");
                bool quick = Console.ReadLine().ToLower() == "y";
                Console.WriteLine("-------------------------");
                Console.Write("Include symbols? (y/n): ");
                bool includeSymbols = Console.ReadLine().ToLower() == "y";

                Console.Write("Include numbers? (y/n): ");
                bool includeNumbers = Console.ReadLine().ToLower() == "y";

                Console.Write("Include lowercase characters? (y/n): ");
                bool includeLowercase = Console.ReadLine().ToLower() == "y";

                Console.Write("Include uppercase characters? (y/n): ");
                bool includeUppercase = Console.ReadLine().ToLower() == "y";

                string password = PasswordGenerator.GeneratePassword(length, includeSymbols, includeNumbers, includeLowercase, includeUppercase);
                if (quick)
                {
                    Console.WriteLine($"Generated Password: {password}");
                }

                Console.Write("Save to file? (y/n): ");
                bool save = Console.ReadLine().ToLower() == "y";
                if (save)
                {
                    Savetofile.Run(password);
                }
                Console.Write("Generate another password? (y/n): ");
                generateAnother2 = Console.ReadLine().ToLower() == "y";
                if (!generateAnother2)
                {
                    Console.Clear();
                    Program.Restart(args);
                }
            }
        }
    }
}
