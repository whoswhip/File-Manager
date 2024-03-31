using System;
using System.Diagnostics;
using System.Security.Cryptography;

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
            Console.WriteLine("[4] Credits");
            Console.WriteLine("[5] Exit");
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
                    Credits.PrintCredits();
                    break;
                case "5":
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
}
