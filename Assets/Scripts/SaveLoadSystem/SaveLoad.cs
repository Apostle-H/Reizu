using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;

public static class SaveLoad
{
    private static string inventoryPath = Application.persistentDataPath + "InventorySave";

    public static event Void onSaved;

    public static void WriteJson(Inventory inventory)
    {
        JsonSerializer serializer = new JsonSerializer();
        serializer.Formatting = Formatting.Indented;

        using (StreamWriter sw = new StreamWriter(inventoryPath))
        using (JsonWriter jsonWriter = new JsonTextWriter(sw))
        {
            Dictionary<string, Item[]> items = new Dictionary<string, Item[]>();
            items.Add("equip", inventory.equip);
            items.Add("items", inventory.items);
            serializer.Serialize(jsonWriter, items);
        }

        onSaved?.Invoke();
    }

    public static Dictionary<string, Item[]> ReadJson()
    {
        if (!File.Exists(inventoryPath))
            return null;

        using (StreamReader file = File.OpenText(inventoryPath))
        {
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize(file, typeof(Dictionary<string, Item[]>)) as Dictionary<string, Item[]>;
        }
    }
}
