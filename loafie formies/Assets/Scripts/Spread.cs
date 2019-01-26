using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spread : MonoBehaviour
{
    int rate;
    int density;
    Sprite sprite;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
        sr.sprite = sprite;
    }

    // Update is called once per frame
    void Update()
    {
      sr.sprite = sprite;
    }
}
