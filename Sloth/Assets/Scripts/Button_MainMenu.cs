using UnityEngine;
using UnityEngine.EventSystems;

public class Button_MainMenu : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Singleton.LoadMainMenu();
    }
}