using System.Collections.Generic;
using UnityEngine;

public class Handler : MonoBehaviour
{
    private const int InclusiveUpperBoundOffset = 1;

    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private CubeColorChanger _colorChanger;
    [SerializeField] private CubeExplosion _cubeExplosion;
    [SerializeField] private CubeDestroyer _cubeDestroyer;

    [SerializeField, Min(1)] private int _minCubeCount = 2;
    [SerializeField, Min(1)] private int _maxCubeCount = 6;

    [SerializeField, Min(0f)] private float _explosionForce = 5f;
    [SerializeField, Min(0f)] private float _explosionRadius = 3f;
    [SerializeField, Min(0f)] private float _explosionUpwardsModifier = 0.5f;
    [SerializeField, Min(0f)] private float _spawnOffset = 0.2f;

    [SerializeField, Range(0f, 1f)] private float _childScaleMultiplier = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _childChanceMultiplier = 0.5f;

    private readonly HashSet<CubeDecision> _subscribedCubes = new HashSet<CubeDecision>();

    private void OnEnable()
    {
        _cubeSpawner.CubeSpawned += SubscribeToCube;

        CubeDecision[] cubesOnScene = FindObjectsOfType<CubeDecision>();

        foreach (CubeDecision cube in cubesOnScene)
        {
            SubscribeToCube(cube);
        }
    }

    private void OnDisable()
    {
        _cubeSpawner.CubeSpawned -= SubscribeToCube;

        foreach (CubeDecision cube in _subscribedCubes)
        {
            if (cube == null)
                continue;

            cube.SplitRequested -= SplitCube;
            cube.DestroyRequested -= DestroyCube;
        }

        _subscribedCubes.Clear();
    }

    private void SubscribeToCube(CubeDecision cube)
    {
        if (cube == null || _subscribedCubes.Contains(cube))
            return;

        _subscribedCubes.Add(cube);

        cube.SplitRequested += SplitCube;
        cube.DestroyRequested += DestroyCube;
    }

    private void SplitCube(CubeDecision sourceCube)
    {
        int cubesCount = Random.Range(
            _minCubeCount,
            _maxCubeCount + InclusiveUpperBoundOffset
        );

        Vector3 explosionCenter = sourceCube.transform.position;

        Vector3 childScale = sourceCube.transform.localScale * _childScaleMultiplier;

        float childChance = sourceCube.SplitChance * _childChanceMultiplier;

        for (int i = 0; i < cubesCount; i++)
        {
            Vector3 offset = Random.insideUnitSphere * _spawnOffset;

            CubeDecision childCube = _cubeSpawner.Spawn(
                explosionCenter + offset,
                Random.rotation
            );

            childCube.Initialize(childChance);
            childCube.transform.localScale = childScale;

            _colorChanger.SetRandomColor(childCube);

            _cubeExplosion.Explode(childCube, explosionCenter, _explosionForce, _explosionRadius, _explosionUpwardsModifier);
        }

        sourceCube.RequestDestroy();
    }

    private void DestroyCube(CubeDecision cube)
    {
        cube.SplitRequested -= SplitCube;
        cube.DestroyRequested -= DestroyCube;

        _subscribedCubes.Remove(cube);

        _cubeDestroyer.DestroyCube(cube);
    }

    private void OnValidate()
    {
        if (_maxCubeCount < _minCubeCount)
        {
            _maxCubeCount = _minCubeCount;
        }
    }
}