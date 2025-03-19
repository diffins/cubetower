using System.IO;
using _Source.SaveLoad;
using Newtonsoft.Json;
using UnityEngine;

public static class SaveLoadManager
{
    private static string filePath = Path.Combine(Application.persistentDataPath, "saveData.json");

    public static void SaveGameData(ItemTower tower)
    {
        var saveData = new SaveData();
        saveData.FillItems(tower);
        string json = JsonConvert.SerializeObject(saveData, Formatting.Indented);
        File.WriteAllText(filePath, json);
        Debug.Log("Player data saved to " + filePath);
    }

    public static SaveData LoadPlayerData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);
            Debug.Log("Player data loaded from " + filePath);
            return saveData;
        }
        else
        {
            Debug.LogWarning("Save file not found at " + filePath);
            return null;
        }
    }
}