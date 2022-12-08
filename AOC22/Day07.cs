using System.Text.RegularExpressions;

namespace AOC22
{
    public class Day07 : BaseDay
    {
        private const string CommandChangeToChildren = @"\$ cd (\w+)$";
        private const string CommandChangeToHead = @"\$ cd (/)$";
        private const string CommandChangeToParent = @"\$ cd ([\.\.]{2})";
        private const string CommandList = @"\$ ls";
        private const string ShowFileNameAndSize = @"(\d+) ([^\s]+)";
        private const string ShowDirname = @"dir ([^\s]+)";

        private const int Threshold = 100000;
        private const int Update = 30000000;
        private const int Diskspace = 70000000;



        protected override void DoRunPart01()
        {
            var localInput = new List<string>(InputData);
            localInput.RemoveAt(0);
            var fileSystem = new Directory("/");
            fileSystem.IsHead = true;
            var res = ParseInputFile(fileSystem, localInput);
            var list = new List<int>();
            IterateOverTree(res.GetHeadDirectory(), list);

            Console.WriteLine(list.Where(x => x <= Threshold).Sum());
        }

        protected override void DoRunPart02()
        {
            var localInput = new List<string>(InputData);
            localInput.RemoveAt(0);
            var fileSystem = new Directory("/");
            fileSystem.IsHead = true;
            var res = ParseInputFile(fileSystem, localInput);
            var listOfAllDirectories = new List<int>();
            IterateOverTree(res.GetHeadDirectory(), listOfAllDirectories);
            var totalUsedDiskSpace = CalculateSizeOfDirectory(res.GetHeadDirectory());
            var requiredSpaceForUpdate = Update - (Diskspace - totalUsedDiskSpace);
            var smallestPossibleDirectoryForUpdate = listOfAllDirectories.Where(x => x >= requiredSpaceForUpdate).ToList().OrderBy(x => x).FirstOrDefault();
            Console.WriteLine(smallestPossibleDirectoryForUpdate);

        }

        public void IterateOverTree(Directory dir, List<int> list)
        {
            list.Add(CalculateSizeOfDirectory(dir));
            dir.ChildDirectories.ToList().ForEach(x => IterateOverTree(x, list));
        }

        public int CalculateSizeOfDirectory(Directory dir)
        {
            return dir.GetSize() + (dir.ChildDirectories.ToList().Select(CalculateSizeOfDirectory).Sum());
        }
        public Directory ParseInputFile(Directory currentDirectory, List<string> commands)
        {
            if (!commands.Any())
            {
                return currentDirectory;
            }
            var command = commands[0];
            commands.RemoveAt(0);


            //Match command with predefined Regex Patterns
            var matchChangeToParent = Regex.Match(command, CommandChangeToParent);
            var matchChangeToChildren = Regex.Match(command, CommandChangeToChildren);
            var matchCommandList = Regex.Match(command, CommandList);
            var matchFileNameAndSize = Regex.Match(command, ShowFileNameAndSize);
            var matchDirName = Regex.Match(command, ShowDirname);
            var matchChangeToHead = Regex.Match(command, CommandChangeToHead);

            if (matchChangeToHead.Success)
            {
                ParseInputFile(currentDirectory.GetHeadDirectory(), commands);
            }

            else if (matchChangeToParent.Success)
            {
                ParseInputFile(currentDirectory.ParentDirectory, commands);
            }
            
            else if (matchChangeToChildren.Success)
            {
                var childDir = new Directory(matchChangeToChildren.Groups[1].Value)
                {
                    ParentDirectory = currentDirectory
                };
                if (currentDirectory.ChildDirectories.Contains(childDir))
                {
                    ParseInputFile(childDir, commands);
                }
                else
                {
                    currentDirectory.ChildDirectories.Add(childDir);
                    ParseInputFile(childDir, commands);
                }
            }

            else if (matchCommandList.Success)
            {
                ParseInputFile(currentDirectory, commands);
            }

            else if (matchFileNameAndSize.Success)
            {
                var file = new File(int.Parse(matchFileNameAndSize.Groups[1].Value), matchFileNameAndSize.Groups[2].Value);
                currentDirectory.Files.Add(file);
                ParseInputFile(currentDirectory, commands);
            }

            else if (matchDirName.Success)
            {
                var childDir = new Directory(matchDirName.Groups[1].Value)
                {
                    ParentDirectory = currentDirectory
                };
                if (currentDirectory.ChildDirectories.Contains(childDir))
                {
                    ParseInputFile(currentDirectory, commands);
                }
                else
                {
                    currentDirectory.ChildDirectories.Add(childDir);
                    ParseInputFile(currentDirectory, commands);
                }
            }
            return currentDirectory;
        }

        public class Directory
        {
            public string Name { get; set; }
            public bool IsHead { get; set; }
            public HashSet<File> Files { get; set; } 
            public HashSet<Directory> ChildDirectories { get; set; } 
            public Directory? ParentDirectory { get; set; }
            public Directory(string name)
            {
                Name = name;
                Files = new HashSet<File>();
                ChildDirectories = new HashSet<Directory>();
            }
            
            public Directory GetHeadDirectory()
            {
                return IsHead ? this : GetHeadDirectoryHelper(ParentDirectory);
            }
            public Directory GetHeadDirectoryHelper(Directory dir)
            {
                return IsHead ? dir : GetHeadDirectoryHelper(ParentDirectory);
            }

            public int GetSize()
            {
                return Files.Select(x => x.Size).Sum();
            }

        }

        public class File
        {
            public File(int size, string name)
            {
                Size = size;
                Name = name;
            }
            public int Size { get; set; }
            private string Name { get; set; }
        }
}


}
