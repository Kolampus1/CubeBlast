using UnityEngine;

public class CubeClickDetector : MonoBehaviour
{
    [SerializeField] private MouseInput _mouseInput;
    [SerializeField] private Camera _camera;

    private void OnEnable()
    {
        _mouseInput.Clicked += FindClickCube;
    }

    private void OnDisable()
    {
        _mouseInput.Clicked -= FindClickCube;
    }

    private void FindClickCube()
    {
        if(_camera == null)
            return;
        
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            CubeDecision cube = hit.collider.GetComponent<CubeDecision>();

            if(cube != null)
            {
                cube.HandleClick();
            }
        }
    }
}
