﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrigger : MonoBehaviour
{
    public GameObject UIToShow;
    public bool UseText = false;
    public Text TextBox;
    public string TextToShow;

    private void Awake()
    {
        UIToShow.SetActive(false);
    }

    private void Update()
    {
        Vector3 objPos = Camera.main.WorldToScreenPoint(this.transform.position);
        TextBox.transform.position = objPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (UseText)
            {
                UIToShow.SetActive(true);
                TextBox.text = TextToShow;
            }
            else
                UIToShow.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if(UseText)
                TextBox.text = "";
            else
                UIToShow.SetActive(false);
        }
    }
}
