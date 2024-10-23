using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance {get; private set;}
    
    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(this);
    }
}
