using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreElement : MonoBehaviour
{
    public ItemType ItemType;

    [SerializeField] private int _remainingScore;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Transform _iconTransform;
    [SerializeField] private AnimationCurve _scaleCurve;
    [SerializeField] private int _level;
    [SerializeField] private GameObject _flyingIconPrefab;

    public int RemainingScore => _remainingScore;
    public int Level => _level;
    public GameObject FlyingIconPrefab => _flyingIconPrefab;
    public Transform IconTransform => _iconTransform;

    public virtual void Setup(Task task)
    {
        _remainingScore = task.NumberToCollect;
        _text.text = task.NumberToCollect.ToString();
    }

    public void SetLevel(int level)
    {
        _level = level;
    }

    public void AddItem()
    {
        _remainingScore--;

        if (_remainingScore < 0)
        {
            _remainingScore = 0;
        }

        _text.text = _remainingScore.ToString();

        StartCoroutine(PlayAnimation());
       ScoreManager.Instance.CheckWin();
    }

    private IEnumerator PlayAnimation()
    {
        for (float i = 0; i < 1f; i+=Time.deltaTime * 2f)
        {
            float scale = _scaleCurve.Evaluate(i);
            _iconTransform.localScale = Vector3.one * scale;
            yield return null;
        }
        
        _iconTransform.localScale = Vector3.one;
    }
}
