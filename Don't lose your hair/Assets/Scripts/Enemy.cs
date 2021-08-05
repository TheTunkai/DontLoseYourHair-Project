using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{

    protected int hearts;
    protected int speed;

    public PlayerController player;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public virtual void Move() // moves enemies
    {

    }
}
