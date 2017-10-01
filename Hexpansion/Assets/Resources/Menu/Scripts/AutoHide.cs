using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoHide : MonoBehaviour
{
    public void HideMe() {
        gameObject.SetActive(false);
    }

    public void DisplayMe()
    {
        gameObject.SetActive(true);
    }
}