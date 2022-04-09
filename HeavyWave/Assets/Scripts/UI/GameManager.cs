using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public Canvas playingCanvas;
    public Canvas menuCanvas;

    public CanvasGroup playing;
    public CanvasGroup menu;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);


        playing.alpha = 1;
        playingCanvas.enabled = true;

        menu.alpha = 0;
        menuCanvas.enabled = false;
    }

    public void playing_Active()
    {
        playing.alpha = 1;
        playing.interactable = true;
        playingCanvas.enabled = true;
        menuCanvas.enabled = false;

    }

    public void menu_Active()
    {
        menu.alpha = 1;
        menu.interactable = true;
        playingCanvas.enabled = false;
        menuCanvas.enabled = true;
    }
 

}
