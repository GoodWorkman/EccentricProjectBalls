[System.Serializable]

public class ProgressData
{
    private int _coins;
    private int _level;
    private bool _isActive;
    private float[] _position;
    
    public int Coins => _coins;
    public int Level => _level;
    public bool IsActive => _isActive;
    public float[] Position => _position;

    public ProgressData(Progress progress)
    {
        _coins = progress.Coins;
        _level = progress.Level;
        _isActive = progress.SaverTestIsActive;
        
        _position = new float[3];
        _position[0] = progress.SaverTestPosition.x;
        _position[1] = progress.SaverTestPosition.y;
        _position[2] = progress.SaverTestPosition.z;
    }
}


