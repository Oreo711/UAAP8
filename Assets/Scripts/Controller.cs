using System;
using UnityEngine;

public abstract class Controller
{
    private bool _isEnabled;

    public virtual void Enable()
    {
        _isEnabled = true;
    }

    public virtual void Disable()
    {
        _isEnabled = false;
    }

    public virtual void UpdateTarget (Vector3 target){}

    public void Update ()
    {
        if (!_isEnabled)
        {
            return;
        }

        UpdateInternal();
    }

    protected abstract void UpdateInternal ();
}
