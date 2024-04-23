using Unity.Mathematics;
using UnityEngine;

public class Box : PassiveItem
{
    [Header("Box")]
    [Range(0, 2)] [SerializeField] private int _health;
    [SerializeField] private GameObject[] _levels;
    [SerializeField] private GameObject _dieEffectPrefab;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        SetHealth(_health);
    }

    public override void OnAffect()
    {
        base.OnAffect();
        _health -= 1;

        Instantiate(_dieEffectPrefab, transform.position, quaternion.Euler(-90f, 0f, 0f));
        _animator.SetTrigger("Shake");

        if (_health < 0)
        {
            Die();
        }
        else
        {
            SetHealth(_health);
        }
    }

    private void SetHealth(int health)
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            _levels[i].SetActive(i <= health);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        ScoreManager.Instance.AddScore(ItemType, transform.position);
    }
}