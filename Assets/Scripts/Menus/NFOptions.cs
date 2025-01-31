using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NFOptions : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
