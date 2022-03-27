using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [SerializeField] private int _currentLevel;
    [SerializeField] private int totalNumberOfLevels;

    public int CoinsAmount;

    // Start is called before the first frame 
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


    // Example of level computation
    //   | mod 3 | res |
    // 0 | 0     | 0   | 0
    // 1 | 1     | 1   | 1
    // 2 | 2     | 2   | 2
    // 3 | 0     | 0   | 3
    // 4 | 1     | 1   | 1
    // 5 | 2     | 2   | 2
    // 6 | 0     | 0   | 3
    // 7 | 1     | 1   | 1
    // 8 | 2     | 2   | 2
    // 9 | 0     | 0   | 3
    public int GetNextLevelNumberAndUpdateLevel()
    {
        if(_currentLevel < totalNumberOfLevels)
        {
            return ++_currentLevel;
        }
        else
        {
            int levelMod = (++_currentLevel % totalNumberOfLevels);
            return levelMod == 0 ? totalNumberOfLevels : levelMod;
        }
    }


    // Session data persistent
    [System.Serializable]
    class Save{
        public int coins;
        public int level;
    }

    public void SaveData()
    {
        PlayerPrefs.SetInt("Coins", CoinsAmount);
        PlayerPrefs.SetInt("Level", _currentLevel);

        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        CoinsAmount = PlayerPrefs.GetInt("Coins");
        _currentLevel = PlayerPrefs.GetInt("Level");
    }

    public void SetTotalNumberOfLevels(int levels)
    {
        totalNumberOfLevels = levels - 1;
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("Coins", 0);
        PlayerPrefs.SetInt("Level", 0);

        CoinsAmount = 0;
        _currentLevel = 0;

        PlayerPrefs.Save();
    }
}
