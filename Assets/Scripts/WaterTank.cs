using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class WaterTank : MonoBehaviour
{
    [SerializeField]
    private LayerMask WaterLayer;//Máscara de capa para que solamente reciba los elementos de esa capa
    [SerializeField]
    private float[] MoveSpeed = new float[4];//array de velocidades de movimiento con serializefield para que se pueda editar desde unity
    [SerializeField]
    private float[] JumpSpeed = new float[4];//array de impulsos de salto con serializefield para que se pueda editar desde unity
    [SerializeField]
    float refuelTimer;
    public static bool isPlaying;
    bool IsWater;
    private float timer = 0;//Temporizador
    public static int Capacity = 0;//Capacidad
    private void Awake()
    {
        Capacity = 0;
    }
    void Update()
    {
        ReFuel();//Actúa como booleano, recibe los valores del collider y devuelve 0 si nada se ha chocado y 1 si el personaje se ha chocado

        if (Capacity < 3)//Solo hace esta acción si la capacidad del tanque no ha llegado al máximo
        {
            if (IsWater)
            {
                timer += Time.deltaTime;//si está dentro del agua, inicia un temporizador de medio segundo
            }
            else
            {
                timer = 0;//si deja de colisionar con el lago, se resetea el temporizador y tiene que volver a empezar
            }
            if (timer >= refuelTimer)//Si el temporizador llega al tiempo establecido, la capacidad aumenta en 1, el temporizador se resetea a 0
                                     //e imprime la capacidad actual
            {
                Capacity += 1;
                timer = 0;
            }
        }
        Switch();//Función para cambiar las velocidades de movimiento del jugador
        if (Input.GetButtonDown("Water") && !isPlaying)
        {
            Player.animator.SetTrigger("Throw");
        }
    }
    public void Switch()
    {
        switch (Capacity)//Cambia la velocidad de movimiento y el impulso de salto dependiendo de qué tan lleno esté el tanque
        {
            case 0:
                Player.moveSpeed = MoveSpeed[0];
                Player.jumpSpeed = JumpSpeed[0];
                Player.animator.SetFloat("Capacity", 0);
                break;
            case 1:
                Player.moveSpeed = MoveSpeed[1];
                Player.jumpSpeed = JumpSpeed[1];
                Player.animator.SetFloat("Capacity", 1);
                break;
            case 2:
                Player.moveSpeed = MoveSpeed[2];
                Player.jumpSpeed = JumpSpeed[2];
                Player.animator.SetFloat("Capacity", 2);
                break;
            case 3:
                Player.moveSpeed = MoveSpeed[3];
                Player.jumpSpeed = JumpSpeed[3];
                Player.animator.SetFloat("Capacity", 3);
                break;
        }
    }
    private void ReFuel()//Función para crear un cuadrado alrededor del objeto para revisar si el jugador lo ha tocado o no
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, Player.facingRight ? Vector2.right : Vector2.left, 1f, WaterLayer);
        if (hitInfo)
        {
            IsWater = true;
        }
        else
        {
            IsWater = false;
        }
    }
}
