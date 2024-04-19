using UnityEngine;

public class Ball : ActiveItem
{
    [Header("Ball")] 
    [SerializeField] private BallSettings _ballSettings;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Transform _visual;
    
    
    private float _minRadius = 0.4f;
    private float _maxRadius = 0.7f;
    private float _triggerEnlarger = 0.15f;


    public override void SetLevel(int level)
    {
        base.SetLevel(level);

        _renderer.material = _ballSettings.GetNormalMaterial(level);
        
        ChangeRadius(level);
        
        _projection.Setup(_ballSettings.GetProjectionMaterial(level),_levelText.text, Radius);
    }
    
    private void ChangeRadius(int level)
    {
        Radius = Mathf.Lerp(_minRadius, _maxRadius, level / MaxLevel);

        Vector3 ballScale = Vector3.one * (Radius * 2f);
        _visual.localScale = ballScale;
        _collider.radius = Radius;
        _trigger.radius = Radius + _triggerEnlarger;
    }

    public override void DoEffect()
    {
        base.DoEffect();
        IncreaseLevel();
    }
}
