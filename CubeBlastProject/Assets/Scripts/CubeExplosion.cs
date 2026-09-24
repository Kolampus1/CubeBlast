using UnityEngine;

public class CubeExplosion : MonoBehaviour
{
    public void Explode(CubeDecision cube, Vector3 explosionCenter, float explosionForce, float explosionRadius, float upwardsModifier)
    {
        if (cube.TryGetComponent(out Rigidbody cubeRigidbody))
        {
            cubeRigidbody.AddExplosionForce(explosionForce, explosionCenter, explosionRadius, upwardsModifier, ForceMode.Impulse);
        }
    }
}