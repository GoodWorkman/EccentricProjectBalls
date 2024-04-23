using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreElementBall : ScoreElement
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private RawImage _image;
    [SerializeField] private BallSettings _ballSettingsSO;
    
    private int _degreesedNumber = 2;


    public override void Setup(Task task)
    {
        base.Setup(task);
        
        int number = (int)Mathf.Pow(_degreesedNumber, task.Level + 1);
        _levelText.text = number.ToString();
        _image.color = _ballSettingsSO.GetNormalMaterial(task.Level).color;
        
        SetLevel(task.Level);
    }
}
