using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSequence : MonoBehaviour
{
    [SerializeField] Canvas GameOver;
    public void Start()
    {
        GameOver.enabled = false;
    }
    public void DeathHandel()
    {
        GameOver.enabled = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
}
