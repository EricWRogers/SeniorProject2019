using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlighting : MonoBehaviour
{
    public string message;
    public Material material;

    HUD hud;
    Renderer rend;
    Material tempMaterial;

    void Start()
    {
        hud = GameObject.FindObjectOfType<HUD>();
        rend = GetComponent<Renderer>();
    }

    void OnMouseEnter()
    {
        tempMaterial = rend.material;
    }

    void OnMouseOver()
    {
        rend.material = material;
        hud.MessageForPlayer(message);
    }

    void OnMouseExit()
    {
        rend.material = tempMaterial;
        hud.MessageForPlayer();
    }
}
