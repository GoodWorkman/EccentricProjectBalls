using UnityEngine;

[System.Serializable]
public struct Task
{
    public ItemType ItemType;
    public int NumberToCollect;
    public int Level;
}
public class Level : MonoBehaviour
{
    public static Level Instance;
    
    [SerializeField] private int _numberOfBalls = 50;
    [SerializeField] private int _maxCreatedBallLevel = 1;
    [SerializeField] private int _rewardedCoins = 10;
    [SerializeField] private Task[] _tasks;

    public int NumberOfBalls => _numberOfBalls;
    public int MaxCreatedBallLevel => _maxCreatedBallLevel;
    public int RewardedCoins => _rewardedCoins;
    public Task[] Tasks => _tasks;

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
}
