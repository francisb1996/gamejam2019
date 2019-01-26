using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : MonoBehaviour
{
    int rate
    int density
    Sprite sprite
    Color color
    private SpriteRenderer sr

    void Awake()
    {
        sr = gameObject.AddComponent<SpriteRenderer>() as SpriteRenderer;
        sr.color = color
        sr.sprite = SpriteRenderer
    }

    // Update is called once per frame
    void Update()
    {
      
    }
}
