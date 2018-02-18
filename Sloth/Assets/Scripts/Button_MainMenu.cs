using UnityEngine;
using UnityEngine.EventSystems;

public class Button_MainMenu : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Singleton.MenuSelect();
        UIManager.Singleton.LoadMainMenu();
    }
}