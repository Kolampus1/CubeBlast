using System;
using UnityEngine;

public class CubeDecision : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float _splitChance = 1f;

    public event Action<CubeDecision> SplitRequested;
    public event Action<CubeDecision> DestroyRequested;

    public float SplitChance => _splitChance;

    private bool _wasClicked;

    public void HandleClick()
    {
        if (_wasClicked)
            return;

        _wasClicked = true;

        if (UnityEngine.Random.value <= _splitChance)
        {
            SplitRequested?.Invoke(this);
        }
        else
        {
            DestroyRequested?.Invoke(this);
        }
    }

    public void Initialize(float splitChance)
    {
        _splitChance = Mathf.Clamp01(splitChance);
        _wasClicked = false;
    }

    public void RequestDestroy()
    {
        DestroyRequested?.Invoke(this);
    }
}