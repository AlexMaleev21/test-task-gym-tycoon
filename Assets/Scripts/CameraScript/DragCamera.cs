using UnityEngine;

/* Camera manipulation script
 * Blocked when menus are on screen
 */
public class CameraDragAndZoom : MonoBehaviour
{
    [Header("Mouse movement")]
    public float dragSpeed = 0.1f;
    private Vector3 dragOrigin;

    [Header("Mouse zoom")]
    public float zoomSpeed = 10f;
    public float minZoom = 10f;
    public float maxZoom = 50f;

    private float fixedY;

    public static bool IsInputBlocked = false;

    void Start()
    {
        fixedY = transform.position.y;
    }

    void Update()
    {
        if (IsInputBlocked) return;

        HandleDrag();
        HandleZoom();
    }

    /* Camera Movement */
    private void HandleDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = Input.mousePosition;
            return;
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 diff = Input.mousePosition - dragOrigin;
            Vector3 move = new Vector3(-diff.x, 0, -diff.y) * dragSpeed;

            transform.position += transform.TransformDirection(move);
            transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);

            dragOrigin = Input.mousePosition;
        }
    }

    /* Camera zoom */
    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) > 0.01f)
        {
            Vector3 oldPos = transform.position;
            transform.position += transform.forward * scroll * zoomSpeed;

            if (transform.position.y < minZoom || transform.position.y > maxZoom)
                transform.position = oldPos;

            fixedY = transform.position.y;
        }
    }
}
