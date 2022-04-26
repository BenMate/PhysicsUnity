using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    bool firstfloor = true;

    Animation a;

    void Start()
    {
        a = GetComponent<Animation>();
    }

    public void MoveElevator()
    {
        //if the animation is playing, return
        //if (a.isPlaying)
        //    return;

        //changes the value of the bool each time.
        firstfloor ^= true;
        
        //plays the animation
        a.Play(firstfloor ? "GoUp" : "GoDown");
        
    }
}
