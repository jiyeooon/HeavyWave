using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCanvas : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void OnClickNewGame()
    {

    }
    
    
    public void OnClickQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    } 
}
