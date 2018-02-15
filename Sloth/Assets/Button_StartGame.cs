using UnityEngine;
using UnityEngine.EventSystems;

public class Button_StartGame : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        UIManager.Singleton.StartNewGame();
    }
}