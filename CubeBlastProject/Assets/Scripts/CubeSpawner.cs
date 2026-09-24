using System;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private CubeDecision _cubePrefab;

    public event Action<CubeDecision> CubeSpawned;

    public CubeDecision Spawn(Vector3 position, Quaternion rotation)
    {
        CubeDecision cube = Instantiate(_cubePrefab, position, rotation);

        CubeSpawned?.Invoke(cube);

        return cube;
    }
}