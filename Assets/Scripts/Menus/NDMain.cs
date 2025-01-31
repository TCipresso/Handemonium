using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NDMain : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
