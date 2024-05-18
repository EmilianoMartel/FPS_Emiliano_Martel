using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshChanger : MonoBehaviour
{
    [SerializeField] private MeshFilter _meshFiltrer;
    [SerializeField] private List<Mesh> _meshesList = new List<Mesh>();
    private int _index = 0;

    private void Awake()
    {
        if (_meshesList.Count == 0)
        {
            Debug.LogError($"{name}: List is 0.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
        if (!_meshFiltrer) 
        {
            Debug.LogError($"{name}: MeshFilter is null.\nCheck and assigned one.\nDisabling component.");
            enabled = false;
            return;
        }
    }

    [ContextMenu("ChangeMesh")]
    private void Change()
    {
        if (_index >= _meshesList.Count)
        {
            _index = 0;
        }
        if(_meshesList[_index] != null)
        {
            _meshFiltrer.mesh = _meshesList[_index];
        }
        _index++;
    }
}