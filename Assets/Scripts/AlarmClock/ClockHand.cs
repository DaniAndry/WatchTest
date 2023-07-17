using UnityEngine;
using UnityEngine.EventSystems;

public class ClockHand : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private bool isRotating = false;
    private Vector3 mouseStartPosition;
    private float rotationOffset;

    public void OnPointerDown(PointerEventData eventData)
    {
        mouseStartPosition = Camera.main.ScreenToWorldPoint(eventData.position);
        Vector3 dir = mouseStartPosition - transform.position;
        rotationOffset = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - transform.eulerAngles.z;
        isRotating = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (isRotating)
        {
            Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(eventData.position);
            Vector3 dir = currentMousePosition - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle - rotationOffset));
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isRotating = false;
    }
}
