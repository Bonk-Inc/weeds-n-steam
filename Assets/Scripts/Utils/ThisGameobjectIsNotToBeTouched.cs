using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisGameobjectIsNotToBeTouched : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

}
