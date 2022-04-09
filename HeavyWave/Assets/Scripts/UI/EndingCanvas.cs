using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingCanvas : MonoBehaviour
{
    public void OnClickExit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
