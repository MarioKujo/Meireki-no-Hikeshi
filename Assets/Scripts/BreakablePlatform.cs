using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player" && WaterTank.Capacity == 3)
        {
            Destroy(gameObject);//Si la plataforma quebradiza entra en contacto con el jugador y la capacidad del tanque es de 3, se rompe
        }
    }
}
