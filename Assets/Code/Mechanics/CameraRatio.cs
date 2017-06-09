using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraRatio: MonoBehaviour
{
    public Vector2 keepRatio = new Vector2(16, 9);

    void Awake()
    {
        var camera = GetComponent<Camera>();
        var w = camera.pixelWidth;
        var h = camera.pixelHeight;
        
        var cameraRatio = (float)w / h;
        var expectedRatio = keepRatio.x / keepRatio.y;

        var dr = expectedRatio - cameraRatio;
        if (dr > 0)
        {
            var size = camera.orthographicSize;
            camera.orthographicSize = size * (1 + dr);
        }
    }
}