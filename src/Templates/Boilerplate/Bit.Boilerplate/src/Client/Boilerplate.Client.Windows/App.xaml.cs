﻿using System.IO.IsolatedStorage;
using System.IO;
using System.Windows;
using System.Text.Json;
using System.Collections;

namespace Boilerplate.Client.Windows;

public partial class App
{
    const string WindowsStorageFilename = "windows.storage.json";

    private void App_Startup(object sender, StartupEventArgs e)
    {
        // Restore application-scope property from isolated storage
        using IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
        try
        {
            using IsolatedStorageFileStream stream = new IsolatedStorageFileStream(WindowsStorageFilename, FileMode.Open, storage);
            foreach (DictionaryEntry item in JsonSerializer.Deserialize<IDictionary>(stream)!)
            {
                Properties.Add(item.Key, item.Value);
            }
        }
        catch (IsolatedStorageException exp) when (exp.InnerException is FileNotFoundException) { }
    }

    private void App_Exit(object sender, ExitEventArgs e)
    {
        // Persist application-scope property to isolated storage
        IsolatedStorageFile storage = IsolatedStorageFile.GetUserStoreForDomain();
        using IsolatedStorageFileStream stream = new IsolatedStorageFileStream(WindowsStorageFilename, FileMode.Create, storage);
        using StreamWriter writer = new StreamWriter(stream);
        writer.Write(JsonSerializer.Serialize(Properties));
    }
}

