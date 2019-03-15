using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] public int hp = 100;

    void ReceiveHit()
    {
        hp -= 10;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
    }
}
