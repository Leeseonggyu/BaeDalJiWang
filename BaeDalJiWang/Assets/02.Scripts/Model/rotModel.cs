using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotModel : MonoBehaviour
{
    private float r;
    [SerializeField] public Transform tr;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;

        r = 200;
        tr = GetComponent<Transform>();
    }
    
    void FixedUpdate()
    {
        if (r < 360)
            r += 1;
        else
            r = 0;
        tr.transform.rotation = Quaternion.Euler(0, r, 0);
    }
}
