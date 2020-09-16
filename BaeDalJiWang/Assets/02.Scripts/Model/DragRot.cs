using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragRot : MonoBehaviour
{
    float rot_speed;
    // Start is called before the first frame update
    void Start()
    {
        rot_speed = 500.0f;
    }

    void OnMouseDrag()
    {
        float rot_x = Input.GetAxis("Mouse X") * rot_speed * Time.unscaledDeltaTime;
        transform.Rotate(0, -rot_x, 0, Space.World);
    }
}
