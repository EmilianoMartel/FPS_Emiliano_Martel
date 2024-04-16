using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "BoolData", fileName = "BoolData")]
public class BoolDataSO : ScriptableObject
{
    private bool _boolData;

    public bool boolData { get { return _boolData; } set { _boolData = value; } }
}
