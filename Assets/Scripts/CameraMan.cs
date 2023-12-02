using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMan : MonoBehaviour
{
    [SerializeField]
    public Transform player;//para transformar la posición de la cámara
    void Update()
    {
        transform.position = new Vector3(player.position.x, player.position.y + 2.5f, -10);
        //cada frame está actualizando su posición a la misma de la del personaje
    }
}
