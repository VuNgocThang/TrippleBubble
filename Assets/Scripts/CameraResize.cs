using Cinemachine;
using UnityEngine;

public class CameraResize : MonoBehaviour
{
    public CinemachineFreeLook freeLock;

    public Camera Cam;
   
    const float iPhanRatio = 2340f / 1080;

    [SerializeField] float minSize = 540;
    [SerializeField] float maxSize = 720;
    [SerializeField] bool Orthographic = true;

    void Start()
    {
        CheckCamera();
    }

    void CheckCamera()
    {
        if (Orthographic)
        {
            if (Cam.aspect <= 15f / 9) Cam.orthographicSize = maxSize;
            else if (Cam.aspect >= iPhanRatio) Cam.orthographicSize = minSize;
            else Cam.orthographicSize = minSize * (iPhanRatio / Cam.aspect);
        }
        else
        {
            Debug.Log(Cam.aspect + ".....");
            if (Cam.aspect < (9 / 16f))
            {
                Debug.Log(Cam.aspect );
                freeLock.m_Lens.FieldOfView = 80 * Cam.aspect / (9 / 16f);
            }
        }
    }

#if UNITY_EDITOR
    void OnValidate()
    {
        CheckCamera();
    }
#endif
}

