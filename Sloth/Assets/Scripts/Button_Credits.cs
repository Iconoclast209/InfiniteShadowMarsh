using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Credits : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Singleton.MenuSelect();
        UIManager.Singleton.ShowCredits();
    }
}
