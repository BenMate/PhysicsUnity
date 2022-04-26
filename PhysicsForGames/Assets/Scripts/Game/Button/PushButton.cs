using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushButton : MonoBehaviour
{
    public float range = 5;

    Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;

            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
            {
                Button button = hit.collider.GetComponent<Button>();

                if (button == null)
                    return;

                button.ButtonPressed();
            }
        }
    }

}
