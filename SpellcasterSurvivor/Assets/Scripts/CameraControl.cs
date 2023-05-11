using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    /*
    [SerializeField] private float _sensitivity;
    [SerializeField] private int _minAngle = -30;
    [SerializeField] private int _maxAngle = 45;
    */

    private Camera _camera;

    private void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        
    }
}
