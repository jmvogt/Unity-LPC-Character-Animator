using UnityEngine;

[ExecuteInEditMode]
public class CameraPixelFix : MonoBehaviour
{
    private Camera _camera;

    [Range(1, 4)]
    public int pixelScale = 1;

    private void Update()
    {
        if (_camera == null)
        {
            _camera = GetComponent<Camera>();
            _camera.orthographic = true;
        }

        _camera.orthographicSize = Screen.height * (0.005f / pixelScale);
    }
}
