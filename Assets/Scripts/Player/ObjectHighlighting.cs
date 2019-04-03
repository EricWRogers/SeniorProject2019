using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlighting : MonoBehaviour
{
    public string message;
    public Material material;

    HUD hud;
    Renderer renderer;
    Material tempMaterial;

    void Start()
    {
        hud = GameObject.FindObjectOfType<HUD>();
        renderer = GetComponent<Renderer>();
    }

    void OnMouseEnter()
    {
        tempMaterial = renderer.material;
    }

    void OnMouseOver()
    {
        renderer.material = material;
        hud.MessageForPlayer(message);
    }

    void OnMouseExit()
    {
        renderer.material = tempMaterial;
        hud.MessageForPlayer();
    }
}
