using UnityEngine;
using UnityEngine.SceneManagement;

public class Progress : MonoBehaviour
{
    public static Progress Instance;
    public Vector3 SaverTestPosition;
    public bool SaverTestIsActive;

    private int _sceneCount;
    private int _level;
    private int _coins;
    public int Coins => _coins;
    public int Level => _level;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        _sceneCount = SceneManager.sceneCountInBuildSettings;
        Load();
    }

    public void SetLevel(int level)
    {
        if (level > _sceneCount)
        {
            level = _sceneCount;
        }

        _level = level;
        Save();
    }

    public void AddCoins(int value)
    {
        _coins = value;
        Save();
    }

    [ContextMenu("Save")]

    public void Save()
    {
        SaveSystem.Save(this);
    }

    [ContextMenu("Load")]

    public void Load()
    {
        ProgressData progressData = SaveSystem.Load();

        if (progressData == null)
        {
            _coins = 0;
            _level = 1;
            SaverTestIsActive = true;
            SaverTestPosition = Vector3.one;
        }
        else
        {
            _coins = progressData.Coins;
            _level = progressData.Level;
            SaverTestIsActive = progressData.IsActive;

            SaverTestPosition = new Vector3();
            SaverTestPosition.x = progressData.Position[0];
            SaverTestPosition.y = progressData.Position[1];
            SaverTestPosition.z = progressData.Position[2];
        }
    }

    [ContextMenu("DeleteSaveFile")]
    public void DeleteSaveFile()
    {
        SaveSystem.DeleteFile();
    }
}