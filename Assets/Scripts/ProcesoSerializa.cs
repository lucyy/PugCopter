using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ProcesoSerializa : MonoBehaviour
{
    string ruta;
    string jsonString;
    bool huboRecalculo = true;

    Persona[] perDBGanadores = new Persona[3];

    public Text txtPrimeroNombre;
    public Text txtSegundoNombre;
    public Text txtTerceroNombre;

    public Text txtPrimeroPuntaje;
    public Text txtSegundoPuntaje;
    public Text txtTerceroPuntaje;

    public InputField ifNombre;

    public GameObject goifNombre;
    public GameObject gobtnIngresar;
    public GameObject gobtnContinuar;

    public GameObject goMenuGanadores;
    public GameObject goMenuGameOver;
    public GameObject goTxtFelicidades;
    public GameObject goTxtIngreseNombre;


    string nombre;
    float puntaje;

    Persona[] perGanadores;
    Persona[] ganadoresRecalculados;

    void Start()
    {

        ruta = Application.persistentDataPath + "/Ganador.json";

        if (!System.IO.File.Exists(ruta))
        {
            perDBGanadores[0] = new Persona("-", 0);
            perDBGanadores[1] = new Persona("-", 0);
            perDBGanadores[2] = new Persona("-", 0);

            //Write
            string jsonGanadores = JsonAyuda.AJson<Persona>(perDBGanadores);
            File.WriteAllText(ruta, jsonGanadores);

        }
        



        //Read
        jsonString = File.ReadAllText(ruta);
        perGanadores = JsonAyuda.DeJson<Persona>(jsonString);


       
        //Asignación inicial de nombres ganadores
        txtPrimeroNombre.text = perGanadores[0].nombreP;
        txtSegundoNombre.text = perGanadores[1].nombreP;
        txtTerceroNombre.text = perGanadores[2].nombreP;

        //Asignación inicial de puntajes ganadores
        txtPrimeroPuntaje.text = perGanadores[0].puntajeP.ToString();
        txtSegundoPuntaje.text = perGanadores[1].puntajeP.ToString();
        txtTerceroPuntaje.text = perGanadores[2].puntajeP.ToString();
     
       
    }

    public void IngresaNombre()
    {

        nombre = ifNombre.text;

        //Recálculo de ganadores
        perGanadores = GanadoresRecalculados(perGanadores, nombre, puntaje);

        //Write
        string jsonGanadores = JsonAyuda.AJson<Persona>(perGanadores);
        File.WriteAllText(ruta, jsonGanadores);

  
        //Asignación inicial de nombres ganadores
        txtPrimeroNombre.text = perGanadores[0].nombreP;
        txtSegundoNombre.text = perGanadores[1].nombreP;
        txtTerceroNombre.text = perGanadores[2].nombreP;

        //Asignación inicial de puntajes ganadores
        txtPrimeroPuntaje.text = perGanadores[0].puntajeP.ToString();
        txtSegundoPuntaje.text = perGanadores[1].puntajeP.ToString();
        txtTerceroPuntaje.text = perGanadores[2].puntajeP.ToString();

        gobtnContinuar.SetActive(true);
        gobtnIngresar.SetActive(false);
        goifNombre.SetActive(false);
        goTxtFelicidades.SetActive(false);
        goTxtIngreseNombre.SetActive(false);

    }

    private Persona[] GanadoresRecalculados(Persona[] Ganadores, string nuevoNombre, float nuevoPuntaje)
    {


        if (nuevoPuntaje >= Ganadores[0].puntajeP)
        {
            Ganadores[2].nombreP = Ganadores[1].nombreP;
            Ganadores[1].nombreP = Ganadores[0].nombreP;
            Ganadores[0].nombreP = nuevoNombre;

            Ganadores[2].puntajeP = Ganadores[1].puntajeP;
            Ganadores[1].puntajeP = Ganadores[0].puntajeP;
            Ganadores[0].puntajeP = nuevoPuntaje;
        }

        else if (nuevoPuntaje >= Ganadores[1].puntajeP && nuevoPuntaje < Ganadores[0].puntajeP)
        {
            Ganadores[2].nombreP = Ganadores[1].nombreP;
            Ganadores[1].nombreP = nuevoNombre;

            Ganadores[2].puntajeP = Ganadores[1].puntajeP;
            Ganadores[1].puntajeP = nuevoPuntaje;
        }

        else if (nuevoPuntaje >= Ganadores[2].puntajeP && nuevoPuntaje < Ganadores[1].puntajeP)
        {
            Ganadores[2].nombreP = nuevoNombre;

            Ganadores[2].puntajeP = nuevoPuntaje;
        }


        return Ganadores;
    }

   
    public void Continuar()
    {
        StartCoroutine(coRContinuar());
    }

    void HacerRecalculo(float nuevoPuntaje)
    {
        puntaje = nuevoPuntaje;

        if (puntaje < perGanadores[2].puntajeP)
        {

            goMenuGameOver.SetActive(true);
        }
        else
        {
            goMenuGanadores.SetActive(true);
        }

    }

    IEnumerator coRContinuar()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
