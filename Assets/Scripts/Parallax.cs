using UnityEngine;
public class Parallax : MonoBehaviour
{
    private float length = 0, startPos = 0;//Dos variables: una recibe la longitud y otra recibe la posici�n inicial
    public GameObject cam;//Objeto c�mara
    public float parallexEffect;//Variable tipo float para determinar el efecto del parallax
    void Start()
    {
        startPos = transform.position.x;//Recibe la posici�n inicial
        length = GetComponent<SpriteRenderer>().bounds.size.x;//Recibe el tama�o del borde de la imagen
    }
    void FixedUpdate()
    {
        float temp = (cam.transform.position.x * (1 - parallexEffect));//Variable que recibe la posici�n x de la c�mara restando 1 al efecto parallax
        float dist = (cam.transform.position.x * parallexEffect);//Variable que recibe la distancia a la que se mover� el parallax
        //Transforma la posici�n a la posici�n inicial + la distancia en el eje x.
        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);
        /*Los dos ifs de abajo controlan si el parallax se sale de los bordes de la c�mara, y para arreglarlo mueve la posici�n de inicio hacia
         la derecha o hacia la izquierda, pero haciendo que se mueva para que nunca se llegue a salir del todo de la pantalla*/
        if (temp > startPos + length)
        {
            startPos += length;
        }
        else if (temp < startPos - length)
        {
            startPos -= length;
        }
    }
}