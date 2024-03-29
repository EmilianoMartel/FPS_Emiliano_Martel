using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFaceCamera : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private bool _matchYaxis;

    private void Start()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
        Vector3 pos = _camera.transform.position;
        if (_matchYaxis)
        {
            pos.y = transform.position.y;
        }
        transform.LookAt(pos,Vector3.up);
    }
}
