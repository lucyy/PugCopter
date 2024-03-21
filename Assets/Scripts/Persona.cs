using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Persona 
{
    public string nombreP;
    public float puntajeP;

    public Persona(string nombre, float puntaje)
    {
        nombreP = nombre;
        puntajeP = puntaje;
    }
}
