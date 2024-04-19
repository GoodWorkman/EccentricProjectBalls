using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _sensetivity = 20f;
    [SerializeField] private float _maxOffsetX = 3.5f;

    private float _xPosition;
    private float _oldMouseX;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _oldMouseX = Input.mousePosition.x;
        }

        if (Input.GetMouseButton(0))
        {
            float deltaX = Input.mousePosition.x - _oldMouseX;
            _oldMouseX = Input.mousePosition.x;
            _xPosition += deltaX * _sensetivity / Screen.width; // компенсация разрешений
            _xPosition = Mathf.Clamp(_xPosition, -_maxOffsetX, _maxOffsetX);

            transform.position = new Vector3(_xPosition, transform.position.y, transform.position.z);
        }
    }
}
