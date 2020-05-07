using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float Speed = 1f;
    public Vector3 Acceleration = Vector3.zero;

    private Camera _camera;
    private bool _isMouseLocked;
    private Vector3 _oldMousePosition;
    private Rigidbody _rigidBody;
    private Vector2 _cameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        _camera = gameObject.GetComponentInChildren<Camera>();
        _rigidBody = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // Manage input modes
        if (Input.GetMouseButtonDown(0))
        {
            _isMouseLocked = !_isMouseLocked;
            //Cursor.lockState = _isMouseLocked ? CursorLockMode.Locked : CursorLockMode.None;
            _oldMousePosition = Input.mousePosition;
        }
        if (_isMouseLocked)
        {
            var mousePosition = Input.mousePosition;
            var mouseDelta = mousePosition - _oldMousePosition;
            _oldMousePosition = mousePosition;
            _cameraRotation += new Vector2(-mouseDelta.y, mouseDelta.x);
            _camera.transform.localRotation = Quaternion.Euler(_cameraRotation.x, 0, 0);
        }
        if (Input.GetKey(KeyCode.W))
        {
            Debug.DrawLine(transform.position, transform.position + transform.forward);
            _rigidBody.MovePosition(transform.position + transform.TransformDirection(transform.forward * Speed * Time.deltaTime));
        }
    }

    /// <summary>
    /// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
    /// </summary>
    void FixedUpdate()
    {
        // Gravity
        _rigidBody.AddForce(-transform.up * 0.1f, ForceMode.Acceleration);
        _rigidBody.MoveRotation(Quaternion.Euler(0, _cameraRotation.y, 0));
    }
}
