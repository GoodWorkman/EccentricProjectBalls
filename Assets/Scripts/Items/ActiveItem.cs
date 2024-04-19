using System.Collections;
using TMPro;
using UnityEngine;

public class ActiveItem : Item
{
    [Header("BaseClass")] [SerializeField] protected TextMeshProUGUI _levelText;
    [SerializeField] protected Projection _projection;
    [SerializeField] protected SphereCollider _collider;
    [SerializeField] protected SphereCollider _trigger;
    [SerializeField] protected Animator _animator;

    [SerializeField] private Rigidbody _rigidbody;

    private int _degreesedNumber = 2;
    private int _maxLevel = 10;
    private float _downAccseleration = 1f;

    [field: SerializeField] public int Level { get; private set; }
    [field: SerializeField] public float Radius { get; protected set; }
    [field: SerializeField] public bool IsActive { get; private set; }
    [field: SerializeField] public bool IsIncreased { get; private set; }

    public Rigidbody Rigidbody => _rigidbody;
    public int MaxLevel => _maxLevel;

    protected virtual void Start()
    {
        IsActive = true;
        IsIncreased = false;
        _projection.Hide();
    }

    private void OnTriggerStay(Collider other)
    {
        if (!IsActive) return;

        ActiveItem otherItem = other.GetComponentInParent<ActiveItem>();

        if (otherItem)
        {
            if (Level == otherItem.Level && otherItem.IsActive)
            {
                CollapseManager.Instance.Collapse(this, otherItem);
            }
        }
    }

    public virtual void DoEffect()
    {
    }

    public void IncreaseLevel()
    {
        if (Level < _maxLevel)
        {
            _animator.SetTrigger("LevelUp");
            Level++;
            SetLevel(Level);
        }
    }

    public void IncreasedByStar()
    {
        IsIncreased = true;
        StartCoroutine(TurnOffIncreasedFlag());
    }

    private IEnumerator TurnOffIncreasedFlag()
    {
        yield return new WaitForSeconds(.5f);
        IsIncreased = false;
    }

    public virtual void SetLevel(int level)
    {
        Level = level;

        ChangeText(level);
    }

    public void SetToTube()
    {
        TurnOffInteraction();
        _rigidbody.interpolation = RigidbodyInterpolation.None;
    }

    public void Drop()
    {
        _trigger.enabled = true;
        _collider.enabled = true;
        _rigidbody.isKinematic = false;
        _rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
        transform.parent = null;
        _rigidbody.velocity = Vector3.down * _downAccseleration;
    }

    public void SetParent(Transform parent)
    {
        transform.parent = parent;
    }

    private void TurnOffInteraction()
    {
        _trigger.enabled = false;
        _collider.enabled = false;
        _rigidbody.isKinematic = true;
    }

    private void ChangeText(int level)
    {
        int number = (int)Mathf.Pow(_degreesedNumber, level + 1);
        _levelText.text = number.ToString();
    }

    public void Disable()
    {
        TurnOffInteraction();
        IsActive = false;
    }

    public void Die()
    {
        Destroy(gameObject);
    }

    public void ShowProjection()
    {
        _projection.Show();
    }

    public void HideProjection()
    {
        _projection.Hide();
    }

    public void SetProjectionPosition(Vector3 pos)
    {
        _projection.SetPosition(pos);
    }
}