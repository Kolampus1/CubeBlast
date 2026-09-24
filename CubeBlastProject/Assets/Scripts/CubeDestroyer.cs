using UnityEngine;

public class CubeDestroyer : MonoBehaviour
{
    public void DestroyCube(CubeDecision cube)
    {
        Destroy(cube.gameObject);
    }
}