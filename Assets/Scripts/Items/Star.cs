using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : ActiveItem
{
    [Header("Star")] [SerializeField] private float _effectRadius = 2f;
    [SerializeField] private GameObject _areaVisual;
    [SerializeField] private GameObject _effectPrefab;

    protected override void Start()
    {
        base.Start();

        _areaVisual.SetActive(false);
    }

    public override void DoEffect()
    {
        StartCoroutine(EffectProcess());
    }

    private void OnValidate()
    {
        _areaVisual.transform.localScale = Vector3.one * _effectRadius * 2f;
    }

    private IEnumerator EffectProcess()
    {
        _areaVisual.SetActive(true);
        _animator.enabled = true;

        yield return new WaitForSeconds(1f);

        Collider[] colliders = Physics.OverlapSphere(transform.position, _effectRadius);

        for (int i = 0; i < colliders.Length; i++)
        {
            ActiveItem activeItem = colliders[i].GetComponentInParent<ActiveItem>();

            if (activeItem && activeItem.IsIncreased == false)
            {
                activeItem.IncreaseLevel();
                activeItem.IncreasedByStar();
            }
        }

        Instantiate(_effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}