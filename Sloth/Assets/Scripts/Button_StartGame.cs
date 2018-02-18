using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button_StartGame : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Singleton.MenuSelect();
        StartCoroutine(StartGameWrapper());
    }

    private IEnumerator StartGameWrapper()
    {
        AudioManager.Singleton.MenuStartGame();
        yield return new WaitForSeconds(AudioManager.Singleton.menuStartClips[0].length);
        UIManager.Singleton.StartNewGame();
    }
}