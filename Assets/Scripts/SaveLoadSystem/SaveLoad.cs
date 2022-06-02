using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

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
            Dictionary<string, (Rarity, string, bool)[]> items = new Dictionary<string, (Rarity, string, bool)[]>();
            items.Add("equip", inventory.equip.Select(item => item == null ? (Rarity.myth, null, false) : (item.Rarity, item.Title, item.Type == ItemType.summon)).ToArray());
            items.Add("items", inventory.items.Select(item => item == null ? (Rarity.myth, null, false) : (item.Rarity, item.Title, item.Type == ItemType.summon)).ToArray());
            serializer.Serialize(jsonWriter, items);
        }

        onSaved?.Invoke();
    }

    public static Dictionary<string, (Rarity, string, bool)[]> ReadJson()
    {
        if (!File.Exists(inventoryPath))
            return null;

        using (StreamReader file = File.OpenText(inventoryPath))
        {
            JsonSerializer serializer = new JsonSerializer();
            return serializer.Deserialize(file, typeof(Dictionary<string, (Rarity, string, bool)[]>)) as Dictionary<string, (Rarity, string, bool)[]>;
        }
    }
}
