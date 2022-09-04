using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float dragDistance = 50.0f;
    private Vector3 touchStart;
    private Vector3 touchEnd;

    private Movement movement;

    private bool IsMoved;

    private void Awake()
    {
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (Application.isMobilePlatform)
        {
            OnMobilePlatform();
        }
        else
        {
            OnPCPlatform();
        }
    }

    private void OnMobilePlatform()
    {
        if (Input.touchCount == 0) return;

        Touch touch = Input.GetTouch(0);

        if (touch.phase == TouchPhase.Began)
        {
            touchStart = touch.position;
            IsMoved = false;
        }
        else if (IsMoved == false && touch.phase == TouchPhase.Moved)
        {
            touchEnd = touch.position;

            OnDragXY();
        }
    }

    private void OnPCPlatform()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
            IsMoved = false;
        }
        else if (IsMoved == false && Input.GetMouseButton(0))
        {
            touchEnd = Input.mousePosition;

            OnDragXY();
        }
    }

    private void OnDragXY()
    {
        // if (Mathf.Abs(touchEnd.x - touchStart.x) >= dragDistance && touchEnd.y - touchStart.y >= dragDistance)
        // {
            
        //     if (Mathf.Abs(touchEnd.x - touchStart.x) >= touchEnd.y - touchStart.y)
        //     {
        //         movement.MoveToX((int)Mathf.Sign(touchEnd.x - touchStart.x));
        //         return;
        //     }
        //     else
        //     {
        //         movement.MoveToY();
        //         return;
        //     }
        // }
        // else if (Mathf.Abs(touchEnd.x - touchStart.x) >= dragDistance)
        // {
        //     movement.MoveToX((int)Mathf.Sign(touchEnd.x - touchStart.x));
        //     return;
        // }
        // else if (touchEnd.y - touchStart.y >= dragDistance)
        // {
        //     movement.MoveToY();
        //     return;
        // }

        if (Mathf.Abs(touchEnd.x - touchStart.x) >= dragDistance)
        {
            movement.MoveToX((int)Mathf.Sign(touchEnd.x - touchStart.x));
            IsMoved = true;
            return;
        }
    }
}
