using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _zoomSpeed;

    private float _xPos;
    private float _zPos;

    // Start is called before the first frame update
    void Start()
    {
        _xPos = transform.position.x;
        _zPos = transform.position.z;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = new Vector3(_xPos, transform.position.y, _zPos);

        if (Input.GetKey(KeyCode.W))
        {
            _zPos += _moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            _zPos -= _moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            _xPos += _moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            _xPos -= _moveSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            _camera.fieldOfView += _zoomSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.E))
        {
            _camera.fieldOfView -= _zoomSpeed * Time.deltaTime;
        }
    }
}
