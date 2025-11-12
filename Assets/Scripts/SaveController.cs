using System.IO;
using UnityEngine;

public class SaveController : MonoBehaviour
{
    private string saveLocation;
    private InventoryController inventoryController;
    void Start()
    {
        saveLocation = Path.Combine(Application.persistentDataPath, "saveData.json");
        Debug.Log("Save file path: " + saveLocation);
        inventoryController = FindFirstObjectByType<InventoryController>();
        LoadGame();

    }

    // Update is called once per frame
    public void SaveGame()
    {

        // Create save data
        SaveData saveData = new SaveData
        {
            playerPosition = GameObject.FindGameObjectWithTag("Player").transform.position,
            // inventorySaveData = inventoryController.GetInventoryItems()
        };

        // Convert to JSON and save to file
        File.WriteAllText(saveLocation, JsonUtility.ToJson(saveData));

        Debug.Log("Game saved to " + saveLocation);

    }

    public void LoadGame()
    {
        if (File.Exists(saveLocation))
        {
            SaveData saveData = JsonUtility.FromJson<SaveData>(File.ReadAllText(saveLocation));
            GameObject.FindGameObjectWithTag("Player").transform.position = saveData.playerPosition;

            // ✅ Only set inventory if data actually exists
            if (saveData.inventorySaveData != null && saveData.inventorySaveData.Count > 0)
            {
                inventoryController.SetInventoryItems(saveData.inventorySaveData);
            }
        }
        else
        {
            SaveGame();
        }
    }
}
