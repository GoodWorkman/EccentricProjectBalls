using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private int _currentLevelIndex;

    public event Action OnWin;
    public event Action OnLose;

    [SerializeField] private GameObject _winWindow;
    [SerializeField] private GameObject _loseWindow;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
    }

    public void Win()
    {
        _winWindow.SetActive(true);
        OnWin?.Invoke();

        Progress.Instance.SetLevel( _currentLevelIndex + 1);
        Progress.Instance.AddCoins(Level.Instance.RewardedCoins);
    }
    
    public void Lose()
    {
        OnLose?.Invoke();
        _loseWindow.SetActive(true);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(Progress.Instance.Level);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(_currentLevelIndex);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);

    }
}
