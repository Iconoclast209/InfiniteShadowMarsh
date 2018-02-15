using UnityEngine;
using UnityEngine.EventSystems;

public class Button_Quit : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Singleton.ExitGame();
    }
}
