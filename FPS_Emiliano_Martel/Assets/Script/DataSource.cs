using UnityEngine;

public abstract class DataSource<T> : ScriptableObject
{
    [SerializeField] protected T _reference;
    [SerializeField] private bool logEnabled = true;

    public T Reference
    {
        get => _reference;
        set
        {
            if (value != null && logEnabled)
                Debug.Log($"{name}: Changed value to {value}");
            _reference = value;
        }
    }
}