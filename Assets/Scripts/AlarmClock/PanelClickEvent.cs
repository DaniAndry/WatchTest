using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events; 

public class PanelClickEvent : MonoBehaviour, IPointerClickHandler
{
    public UnityEvent OnPanelClick;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnPanelClick.Invoke();
    }
}
