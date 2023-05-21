using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{
    [SerializeField] private float sensitivity = 2f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float pointerDistance = 1f;

    private float _rotationX;
    private float _rotationY;
    private Texture2D _pointerTexture;
    private Vector2 _pointerPosition;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _pointerTexture = new Texture2D(1, 1);
        _pointerTexture.SetPixel(0, 0, Color.white);
        _pointerTexture.Apply();
    }

    private void Update()
    {
        _rotationX += Input.GetAxis("Mouse X") * sensitivity;
        _rotationY += Input.GetAxis("Mouse Y") * sensitivity;
        _rotationY = Mathf.Clamp(_rotationY, -90f, 90f);

        transform.localRotation = Quaternion.Euler(-_rotationY, _rotationX, 0f);

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 velocity = direction * speed;

        transform.Translate(velocity * Time.deltaTime);

        _pointerPosition = new Vector2(Screen.width / 2f, Screen.height / 2f);
    }

    private void OnGUI()
    {
        GUI.DrawTexture(new Rect(_pointerPosition.x - 5f, _pointerPosition.y - 5f, 10f, 10f), _pointerTexture);
    }
}
