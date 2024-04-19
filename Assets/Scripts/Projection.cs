using TMPro;
using UnityEngine;

public class Projection : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    [SerializeField] private TextMeshProUGUI _text;
    [SerializeField] private Transform _visualTransform;

    public void Setup(Material material, string number, float radius)
    {
        _renderer.material = material;
        _text.text = number;
        _visualTransform.localScale = Vector3.one * (radius * 2f);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
