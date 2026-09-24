using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    public void SetRandomColor(CubeDecision cube)
    {
        if (cube.TryGetComponent(out Renderer cubeRenderer))
        {
            cubeRenderer.material.color = Random.ColorHSV();
        }
    }
}