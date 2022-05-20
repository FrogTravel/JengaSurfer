using UnityEngine;


public class DataManager : MonoBehaviour
{
    public static DataManager Instance;

    [SerializeField] private int _currentLevel;
    [SerializeField] private int _totalNumberOfLevels;
    private int _maxLevel;

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
        _maxLevel = _totalNumberOfLevels - 1;

        ResetData();
    }

    public int GetCurrentLevelNumber()
    {
        Debug.Log(_currentLevel + " " + _maxLevel);
        if (_currentLevel < _maxLevel)
        {
            return _currentLevel;
        }
        else
        {
            int levelMod = (_currentLevel % _maxLevel);
            return levelMod == 0 ? _maxLevel : levelMod;
        }
    }

    // Update level number and save it, because the scene will be recreated
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
        _currentLevel++;
        SaveData();

        return GetCurrentLevelNumber();
    }

    public void UpdateLevelByOne()
    {
        _currentLevel++;
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
        _totalNumberOfLevels = levels;
        _maxLevel = _totalNumberOfLevels - 1;
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
