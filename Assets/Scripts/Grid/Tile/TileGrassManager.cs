using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGrassManager : MonoBehaviour
{

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private List<Sprite> grass;

    void Awake()
    {
        spriteRenderer.sprite = grass.GetRandom();
    }

}
