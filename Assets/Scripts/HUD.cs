using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [SerializeField]
    Image image;//Y una imagen
    [SerializeField]
    Sprite[] sprite = new Sprite[4];//Entra cada estado del cubo de agua
    [SerializeField]
    Text peopleSaved;
    public static int people;
    private void Awake()
    {
        people = 0;
    }
    private void Update()
    {
        image.sprite = sprite[WaterTank.Capacity];//Actualiza constantemente la imagen
        peopleSaved.text = "People saved: " + people;
    }
}
