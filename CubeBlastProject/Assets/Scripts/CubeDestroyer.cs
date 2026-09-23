using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    private CubeDecision _cubeDecision;

    private void Awake()
    {
        _cubeDecision = GetComponent<CubeDecision>();
    }

    private void OnEnable()
    {
        _cubeDecision.DestroyRequested += DestroyCube;
    }

    private void OnDisable()
    {
        if(_cubeDecision != null)
        {
            _cubeDecision.DestroyRequested -= DestroyCube;
        }
    }

    private void DestroyCube()
    {
        Destroy(gameObject);
    }
}
