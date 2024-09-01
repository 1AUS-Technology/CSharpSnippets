namespace CsharpRecap.FileAndStreamIO;

public class FileRunner
{
    public static void Run()
    {
        Console.WriteLine($"Current directory is '{Environment.CurrentDirectory}'");
        Console.WriteLine("Setting current directory to 'C:\\'");

        Directory.SetCurrentDirectory(@"C:\");
        string path = Path.GetFullPath(@"T:\Temp");

        Console.WriteLine(@$"T:\Temp is resolved to {path}");

        string relativePath = Path.GetFullPath("T:Temp");
        Console.WriteLine($"'T:Temp' resolves to {relativePath}");

        Console.WriteLine("Setting current directory to T:\\Users");

        Directory.SetCurrentDirectory("T:\\Users");

        
        path = Path.GetFullPath(@"T:\Temp");

        Console.WriteLine(@$"T:\Temp is resolved to {path}");

        relativePath = Path.GetFullPath("T:Temp");
        // this will print T:\\Users\Temp relative to the folder in the same disks
        // if the relative folder is on another disk such as C:\ then it will resolve to T:\\Temp
        Console.WriteLine($"'T:Temp' resolves to {relativePath}");  
    }
}