using UnityEngine;

public class Stone : PassiveItem
{
    [Header("Stone")]
    [SerializeField] private GameObject _dieEffectPrefab;
    [Range(0, 2)] [SerializeField] private int _level = 2;
    [SerializeField] private Transform _visualTransform;
    [SerializeField] private Stone _stonePrefab;

    private int _createdValue = 2;

    public override void OnAffect()
    {
        base.OnAffect();
        if (_level > 0)
        {
            for (int i = 0; i < _createdValue; i++)
            {
                CreateChildRock(_level - 1);
            }
        }
        
        Die();
    }

    private void CreateChildRock(int level)
    {
        Stone newStone = Instantiate(_stonePrefab, transform.position, Quaternion.identity);
        newStone.SetLevel(level);
    }

    private void SetLevel(int level)
    {
        _level = level;
        float scale = 1f;

        if (level == 2)
        {
            scale = 1f;
        }
        else if(level == 1)
        {
            scale = 0.8f;
        }
        else if(level == 0)
        {
            scale = 0.7f;
        }

        _visualTransform.localScale = Vector3.one * scale;
    }

    private void Die()
    {
        Instantiate(_dieEffectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
