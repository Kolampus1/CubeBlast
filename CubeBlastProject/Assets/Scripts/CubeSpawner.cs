using UnityEngine;

[RequireComponent(typeof(CubeDecision))]
[RequireComponent(typeof(Rigidbody))]
public class CubeSpawner : MonoBehaviour
{
    private const int InclusiveUpperBoundOffset = 1;

    [SerializeField, Min(1)] private int _minCubeCount = 2;
    [SerializeField, Min(1)] private int _maxCubeCount = 6;

    [SerializeField, Min(0f)] private float _explosionForce = 5f;
    [SerializeField, Min(0f)] private float _explosionRadius = 3f;
    [SerializeField, Min(0f)] private float _explosionUpwardsModifier = 0.5f;
    [SerializeField, Min(0f)] private float _spawnOffset = 0.2f;

    [SerializeField, Range(0f, 1f)] private float _childScaleMultiplier = 0.5f;
    [SerializeField, Range(0f, 1f)] private float _childChanceMultiplier = 0.5f;

    private CubeDecision _cubeDecision;

    private void Awake()
    {
        _cubeDecision = GetComponent<CubeDecision>();
    }

    private void OnEnable()
    {
        _cubeDecision.SplitRequested += CreateChildren;
    }

    private void OnDisable()
    {
        if (_cubeDecision != null)
        {
            _cubeDecision.SplitRequested -= CreateChildren;
        }
    }

    private void CreateChildren()
    {
        int cubesCount = Random.Range(_minCubeCount,_maxCubeCount + InclusiveUpperBoundOffset);

        Vector3 explosionCenter = transform.position;
        Vector3 childScale = transform.localScale * _childScaleMultiplier;

        float childChance =_cubeDecision.SplitChance * _childChanceMultiplier;

        for (int i = 0; i < cubesCount; i++)
        {
            Vector3 offset = Random.insideUnitSphere * _spawnOffset;

            GameObject childObject = Instantiate(gameObject, explosionCenter + offset, Random.rotation);

            CubeDecision childDecision = childObject.GetComponent<CubeDecision>();

            childDecision.Initialize(childChance);

            childObject.transform.localScale = childScale;

            Renderer childRenderer = childObject.GetComponent<Renderer>();

            childRenderer.material.color = Random.ColorHSV();

            Rigidbody childRigidbody = childObject.GetComponent<Rigidbody>();

            childRigidbody.AddExplosionForce(_explosionForce, explosionCenter, _explosionRadius, _explosionUpwardsModifier, ForceMode.Impulse);
        }

        _cubeDecision.RequestDestroy();
    }
}