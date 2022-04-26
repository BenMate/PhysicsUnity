using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSway : MonoBehaviour
{
    public float swayAmount = 0.01f;
    public float smoothAmount = 5.0f;

    Vector3 startPosition;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {

        float moveX = -Input.GetAxis("Mouse X") * swayAmount;
        float moveY = -Input.GetAxis("Mouse Y") * swayAmount;

        Vector3 newPosition = new Vector3(moveX, moveY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, newPosition + startPosition , Time.deltaTime * smoothAmount);

    }
}
