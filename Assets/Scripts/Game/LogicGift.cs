using UnityEngine;
using DG.Tweening;
using System.Collections;
using PathCreation;
using UnityEngine.UIElements;

public class LogicGift : MonoBehaviour
{
    public PathCreator creator;
    public float speed = 0.2f;
    float distacne;

    private void Update()
    {
        distacne += speed * Time.deltaTime;
        transform.position = creator.path.GetPointAtDistance(distacne);

        Camera mainCamera = Camera.main;

        if (IsObjectOutOfScreen(mainCamera, transform.position))
        {
            Debug.Log("Object is out of screen!");
        }
    }

    bool IsObjectOutOfScreen(Camera camera, Vector3 position)
    {
        Vector3 screenPoint = camera.WorldToScreenPoint(position);

        // Lấy kích thước của màn hình
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        // Kiểm tra xem đối tượng có nằm ngoài màn hình không
        return (screenPoint.y + 5f < 0 || screenPoint.x + 2f < 0);
    }
}
