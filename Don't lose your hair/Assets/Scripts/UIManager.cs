using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UIManager : MonoBehaviour
{
    #region Variables
    public float plushReserve = 2;
    private float plushRefillRate = 0.2f;

    public static UIManager instance;
    public Slider plushBar;
    #endregion

    private void Awake() 
    {
        if (instance != null) // make singleton pattern
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        

    }


    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        plushBar.value = plushReserve;

        if (plushReserve < 2)
        {
            plushReserve += plushRefillRate * Time.deltaTime;
        }

    }

}
