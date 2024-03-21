using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public  class Testigo : MonoBehaviour
{

    GameObject goMenu;
    GameObject goGameM;

    int contadorCarga = 0;
   public  int contadorCargaEscenaUP {  get { return contadorCarga; } set { contadorCarga = value; } }
    private static Testigo testigo;
    private void Start()
    {
        if (testigo != null)
        {
            Destroy(gameObject);
            return;
        }
        testigo = this;

        GameObject.DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += IncrementoEscenas;
    }

    private void Update()
    {
        contadorCarga=contadorCarga;

    }
    private void IncrementoEscenas(Scene scene, LoadSceneMode mode)
    {
        contadorCarga++;
        if (contadorCarga >= 1)
        { 
             goMenu = GameObject.FindGameObjectWithTag("Menu");
             goGameM = GameObject.FindGameObjectWithTag("gameM");
            goGameM.SendMessage("SeteoVariablesComienzo");
        }
    }

    void gu()
    {
        contadorCarga = 2;
    }

}
