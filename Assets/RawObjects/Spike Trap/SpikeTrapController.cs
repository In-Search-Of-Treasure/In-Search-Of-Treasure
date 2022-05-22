using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapController : MonoBehaviour
{

    public GameObject tilemapBg;

    void Start()
    {
        Physics2D.IgnoreCollision(tilemapBg.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

}
