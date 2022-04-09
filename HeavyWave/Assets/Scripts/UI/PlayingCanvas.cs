using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PlayingCanvas : MonoBehaviour
{
    public void OnClickSetup()
    {
        StartCoroutine(DoFade());
    }
    IEnumerator DoFade()
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        while (canvasGroup.alpha > 0)
        {
            canvasGroup.alpha -= 2 * Time.deltaTime;
            yield return null;

        }
        canvasGroup.interactable = false;
        yield return null;

        GameManager.instance.menu_Active();
    }


}
