using UnityEngine;

public class UDI_Control : MonoBehaviour
{
    public bool RelativeRotation = true;
    private Quaternion Q_RelativeRotation;

    // Start is called before the first frame update
    void Start()
    {
        Q_RelativeRotation = transform.parent.localRotation;
    }

    // Update is called once per frame
    void Update()
    {//회전값이 부모의 회전을 따라감
        if (RelativeRotation)
            transform.rotation = Q_RelativeRotation;
    }
}
