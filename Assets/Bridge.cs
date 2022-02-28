using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge : MonoBehaviour
{
    public int id = 0;

    void Start()
    {
        SignEvent.Event += Enable;
        this.gameObject.SetActive(false);
    }

    public void Enable(int id)
    {
        if(this.id == id)
        {
            this.gameObject.SetActive(true);
            SignEvent.Event -= Enable;
        }
        return;
    }
}
