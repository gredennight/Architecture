using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        IDownloader fileDownloader = new CacheSimpleDownloader(new SimpleDownloader());
        string file1 = fileDownloader.Download("some_file");
        string file2 = fileDownloader.Download("some_file");
        string file3 = fileDownloader.Download("some_other_file");
        string file4 = fileDownloader.Download("some_other_file");
    }
}

public interface IDownloader
{
    public string Download(string file);
}
public class SimpleDownloader : IDownloader
{
    public string Download(string file)
    {
        return $"C:\\User\\Download\\{file}";
    }
}

public class CacheSimpleDownloader : IDownloader
{
    private Dictionary<string, string> cacheDictionary;
    private SimpleDownloader simpleDownloader;

    public CacheSimpleDownloader(SimpleDownloader simpleDownloader)
    {
        this.simpleDownloader = simpleDownloader;
        this.cacheDictionary = new Dictionary<string, string>();
    }

    public string Download(string file)
    {
        if (cacheDictionary.ContainsKey(file))
        {
            Console.WriteLine($"File {file} found in cache, fetching...");
            return cacheDictionary[file];
        }
        Console.WriteLine($"Download of the file {file} started...");
        string output = simpleDownloader.Download(file);
        cacheDictionary[file] = output;
        return output;
    }
}