
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{
    private Camera _cam;
    private Vector3 _lastPosition = Vector3.zero;
    private float _cameraToWorldRatio = 0.0f;

    void Awake()
    {
        _cam = GetComponent<Camera>();//Get the camera component.
    }

    void Start()
    {
        //Calculate the current camera to world ratio to convert the camera's movements into word units.
        Vector3 left = _cam.ScreenToWorldPoint(Vector3.zero);
        Vector3 right = _cam.ScreenToWorldPoint(new Vector3(Screen.width, 0, 0));
        float width = Vector3.Distance(left, right);
        _cameraToWorldRatio = width / Screen.width;
    }

    void Update()
    {
        if (Input.touchCount > 0)
        {
            //Get touch info
            Touch touch = Input.GetTouch(0);//Get the touch info of the first finger.
            Vector3 touchPos = new Vector3(touch.position.x, touch.position.y, 0f);

            if (touch.phase == TouchPhase.Moved)
            {
                Vector3 moveDelta = (_lastPosition - touchPos) * _cameraToWorldRatio;
                transform.Translate(moveDelta);
            }
            _lastPosition = touchPos;
        }
        else if (Input.mousePresent)//Might as well add mouse support.
        {
            if (Input.GetMouseButton(0))
            {
                Vector3 moveDelta = (_lastPosition - Input.mousePosition) * _cameraToWorldRatio;
                transform.Translate(moveDelta);
                Debug.Log("Touch");
            }

            _lastPosition = Input.mousePosition;
        }
    }
}