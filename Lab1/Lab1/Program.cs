using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        StorageManager storageManager = StorageManager.GetInstance();

        storageManager.SetUserStorage("user local", "local");
        Storage user1 = storageManager.GetUserStorage("user local");
        user1.UploadFile("some file.png");

        storageManager.SetUserStorage("user amazon", "s3");
        Storage user2 = storageManager.GetUserStorage("user amazon");
        user2.UploadFile("another file.txt");

        user1.DownloadFile("some file.png");
        user2.DownloadFile("another file.txt");
    }
}

public class StorageManager
{
    private static StorageManager storageInstance;
    private Dictionary<string, Storage> storageDict;

    private StorageManager()
    {
        storageDict = new Dictionary<string, Storage>();
    }

    public static StorageManager GetInstance()
    {
        if (storageInstance == null) { storageInstance = new StorageManager(); }
        return storageInstance;
    }

    public void SetUserStorage(string user, string storageType)
    {
        Storage storage = GetStorage(storageType);
        storageDict[user] = storage;
    }

    public Storage GetUserStorage(string user)
    {
        return storageDict[user];
    }
    private Storage GetStorage(string storageType)
    {
        switch (storageType)
        {
            case "local":
                return new LocalDiskStorage();
            case "s3":
                return new AmazonS3Storage();
            default:
                throw new ArgumentException("not a storage type");
        }
    }
}

public interface Storage
{
    void UploadFile(string file);
    void DownloadFile(string file);
}

public class LocalDiskStorage : Storage
{
    public void UploadFile(string file) { Console.WriteLine($"file '{file}' uploaded to local storage"); }

    public void DownloadFile(string file) { Console.WriteLine($"file '{file}' downloaded from local storage"); }
}

public class AmazonS3Storage : Storage
{
    public void UploadFile(string file) { Console.WriteLine($"file '{file}' uploaded to local amazon s3"); }

    public void DownloadFile(string file) { Console.WriteLine($"file '{file}' downloaded from local storage"); }
}