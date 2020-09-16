using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjData : MonoBehaviour
{
    public int id;
    public bool isBuilding, RingSpawnCount;
    public static ObjData instance;

    private void Awake()
    {
        RingSpawnCount = false;
        instance = this;

        if (instance.tag == "Building")
            isBuilding = true;
        else
            isBuilding = false;
    }
}
