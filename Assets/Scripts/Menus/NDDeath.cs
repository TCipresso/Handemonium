using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NDDeath : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
