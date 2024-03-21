using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public static class JsonAyuda
{
    public static string AJson<Persona>(Persona[] array)
    {
 
        PerGanadores<Persona> data = new PerGanadores<Persona>();
        data.Items = array;
        return JsonUtility.ToJson(data);
    }


    public static Persona[] DeJson<Persona>(string json)
    {
       
        PerGanadores<Persona> data = JsonUtility.FromJson<PerGanadores<Persona>>(json);
        return data.Items;
    }
    
   

    [System.Serializable]
    public class PerGanadores<Persona>
    {
        public Persona[] Items;
    }

    
}