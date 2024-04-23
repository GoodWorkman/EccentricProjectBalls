using System.Collections;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Level _level;
    [SerializeField] private ScoreElement[] _scoreElementsPrefab;
    [SerializeField] private ScoreElement[] _createdElements;
    [SerializeField] private Transform _container;
    [SerializeField] private Camera _camera;

    public static ScoreManager Instance;

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
        _createdElements = new ScoreElement[_level.Tasks.Length];

        for (int tasksIndex = 0; tasksIndex < _level.Tasks.Length; tasksIndex++)
        {
            Task task = _level.Tasks[tasksIndex];
            ItemType itemType = task.ItemType;

            for (int i = 0; i < _scoreElementsPrefab.Length; i++)
            {
                if (itemType == _scoreElementsPrefab[i].ItemType)
                {
                    ScoreElement scoreElement = Instantiate(_scoreElementsPrefab[i], _container);
                    scoreElement.Setup(task);
                    _createdElements[tasksIndex] = scoreElement;
                }
            }
        }
    }

    public bool AddScore(ItemType itemType, Vector3 position, int level = 0)
    {
        for (int i = 0; i < _createdElements.Length; i++)
        {
            if (_createdElements[i].ItemType != itemType) continue;
            if (_createdElements[i].RemainingScore == 0) continue;
            if (_createdElements[i].Level != level) continue;

            StartCoroutine(AddScoreAnimation(_createdElements[i], position));
            return true;
        }

        return false;
    }

    private IEnumerator AddScoreAnimation(ScoreElement createdElement, Vector3 position)
    {
        GameObject icon = Instantiate(createdElement.FlyingIconPrefab, position, Quaternion.identity);
        Vector3 screenPos = new Vector3(createdElement.IconTransform.position.x,
            createdElement.IconTransform.position.y, -_camera.transform.position.z);

        Vector3 a = position;
        Vector3 b = position + Vector3.back * 5f + Vector3.down * 5f;
        Vector3 d = _camera.ScreenToWorldPoint(screenPos);
        Vector3 c = d + Vector3.back * 6f;

        for (float i = 0; i < 1f; i += Time.deltaTime)
        {
            icon.transform.position = Bezier.GetPoint(a, b, c, d, i);
            yield return null;
        }

        Destroy(icon.gameObject);
        createdElement.AddItem();
    }

    public void CheckWin()
    {
        for (int i = 0; i < _createdElements.Length; i++)
        {
            if (_createdElements[i].RemainingScore != 0) return;
        }

        GameManager.Instance.Win();
    }
}