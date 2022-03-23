using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [SerializeField] private int _currentLevel;
    [SerializeField] public int CoinsAmount;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null)
        {
            return;
        }

        Instance = this;
        DontDestroyOnLoad(Instance);
        LoadData();

        SceneManager.LoadScene(_currentLevel);
    }

    public int GetCurrentLevelNumber()
    {
        return _currentLevel;
    }

    public void UpdateLevelByOne()
    {
        _currentLevel++;
    }

    public int GetNextLevelNumberAndUpdateLevel()
    {
        if (_currentLevel > 3) // Level looping
        {
            _currentLevel = 1; // So we will start from second level
        }
        return _currentLevel;
    }


    // Session data persistent
    [System.Serializable]
    class Save{
        public int coins;
        public int level;
    }

    public void SaveData()
    {
        string path = Application.persistentDataPath + "/savefile28.json";

        Save save = new();
        save.coins = CoinsAmount;
        save.level = _currentLevel;

        string json = JsonUtility.ToJson(save);
        File.WriteAllText(path, json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile28.json";
        try
        {
            string json = File.ReadAllText(path);
            Save save = JsonUtility.FromJson<Save>(json);

            _currentLevel = save.level;
            CoinsAmount = save.coins;
        }
        catch (FileNotFoundException)
        {
            _currentLevel = 0;
            CoinsAmount = 0;
        }
        
    }
}
