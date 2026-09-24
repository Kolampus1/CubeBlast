using UnityEngine;

public class CubeClickDetector : MonoBehaviour
{
    [SerializeField] private MouseInput _mouseInput;
    [SerializeField] private Camera _camera;

    private void OnEnable()
    {
        _mouseInput.Clicked += FindClickedCube;
    }

    private void OnDisable()
    {
        _mouseInput.Clicked -= FindClickedCube;
    }

    private void FindClickedCube()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent(out CubeDecision cube))
            {
                cube.HandleClick();
            }
        }
    }
}