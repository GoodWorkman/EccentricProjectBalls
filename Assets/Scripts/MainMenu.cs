using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _coinsText;
    [SerializeField] private Button _buttonStart;

    private void Start()
    {
        _coinsText.text = Progress.Instance.Coins.ToString();
        _levelText.text = "Level " + Progress.Instance.Level;
        _buttonStart.onClick.AddListener(StartLevel);
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(Progress.Instance.Level);
    }
}
