using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{

    public UnityEvent pressbutton;


    public void ButtonPressed()
    {
        pressbutton?.Invoke();
    }


}
