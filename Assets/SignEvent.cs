using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignEvent : MonoBehaviour
{
    public static event Action<int> Event;

    public GameObject Text;

    public int id;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Text.SetActive(true);
        Event?.Invoke(this.id);
    }
}
