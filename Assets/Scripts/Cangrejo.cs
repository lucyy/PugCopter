using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cangrejo : MonoBehaviour
{
    float velocidad=1;
    Vector3 dimensionPantalla;

    void Update()
    {

        transform.position = transform.position + new Vector3(1,0,0)*Time.deltaTime*velocidad;
   
        if (transform.position.x>=13)
        {
            int[] posicionesX = new int[] { -13, -15, -17, -19};

            transform.position = new Vector3(posicionesX[Random.Range(0, 4)], transform.position.y, transform.position.z);
        }
    }
}
