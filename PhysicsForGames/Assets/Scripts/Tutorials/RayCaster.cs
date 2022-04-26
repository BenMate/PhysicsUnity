using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCaster : MonoBehaviour
{
    public Text output;

    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitinfo;
        if(Physics.Raycast(ray, out hitinfo, 500) == true)
        {
            output.text = hitinfo.transform.gameObject.name;
        }
    }
}
