﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sanityWhispers : MonoBehaviour
{
    GameManager gameManager;
    AudioSource insanity;

    void Awake()
    {
        gameManager = (GameManager)FindObjectOfType(typeof(GameManager));
        insanity = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        InsanitySound();
    }

    private void InsanitySound()
    {
        float dist = Vector3.Distance(transform.position, gameManager.EntityGO.transform.position);
        if (dist <= 400f)
            insanity.volume += .5f / dist;
        else
            insanity.volume -= .5f / dist;
        if (insanity.volume <= 0)
        {
            insanity.volume = 0;
        }
        if (insanity.volume >= 1)
        {
            insanity.volume = 1;
        }
    }
}