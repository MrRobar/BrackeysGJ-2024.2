using System;
using UnityEngine;

public abstract class Rule : ScriptableObject
{
    public event Action Completed;
    public bool IsCompleted;

    public void Complete()
    {
        IsCompleted = true;
        Completed?.Invoke();
    }
    
    public virtual void Init() { }
}
