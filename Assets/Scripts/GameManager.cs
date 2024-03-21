using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject goMenuPrincipal;
    public GameObject goInfo;
    public GameObject goTesContador;

    private float velocidad = 4;
    public GameObject goColumna;
    public Renderer fondo;
    public List<GameObject> gosColumnas;
    public List<GameObject> gosPiedras;
    public GameObject goPiedra1;
    public GameObject goPiedra2;
    public GameObject goPremio;

    public bool gameOver = false;
    public bool start = false;
    private float puntajePantalla = 0;
    float velocidadObstaculo;

    public GameObject goTxtPuntaje;
    public Text txtPuntaje;

    public AudioClip acJuego;
    public AudioClip acMar;
    public AudioClip acHelicoptero;
    public AudioClip acBoton;
    public AudioClip acMuerte;
    public AudioClip acCoco;
    public AudioClip acPelicano;
    public AudioClip acCangrejo;
    public AudioClip acLimites;
    public AudioClip acAullar;

    private AudioSource asJuego;
    private AudioSource asMar;
    private AudioSource asHelicoptero;
    private AudioSource asBoton;
    private AudioSource asMuerte;
    private AudioSource asCoco;
    private AudioSource asPelicano;
    private AudioSource asCangrejo;
    private AudioSource asLimites;
    private AudioSource asAullar;

    private AudioSource asIntro;

    private Vector3 dimensionPantallaGM;
    Testigo teContador = new Testigo();


    void Start()
    {

        if (teContador.contadorCargaEscenaUP >= 1)
        {
            start = true;
            gameOver = false;
        }

        asBoton = AnadirAudioS(false, false, 1f, acBoton);
        asMuerte = AnadirAudioS(false, false, 1f, acMuerte);
        asCoco = AnadirAudioS(false, false, 1f, acCoco);
        asPelicano = AnadirAudioS(false, false, 1f, acPelicano);
        asCangrejo = AnadirAudioS(false, false, 1f, acCangrejo);
        asLimites = AnadirAudioS(false, false, 1f, acLimites);
        asAullar = AnadirAudioS(false, false, 5f, acAullar);
        asIntro = gameObject.GetComponent<AudioSource>();


        int[] posicionesY = new int[] { 3, 1, -1, -3 };
   
        float randomVariableYinicial6 = Random.Range(-4, 5);
   
        //Crear piedras
        gosPiedras.Add(Instantiate(goPiedra1, new Vector2(11, posicionesY[Random.Range(0, 3)]), Quaternion.identity));
        gosPiedras.Add(Instantiate(goPiedra2, new Vector2(13, posicionesY[Random.Range(0, 3)]), Quaternion.identity));
        gosPiedras.Add(Instantiate(goPiedra1, new Vector2(15, posicionesY[Random.Range(0, 3)]), Quaternion.identity));
        gosPiedras.Add(Instantiate(goPiedra2, new Vector2(16, posicionesY[Random.Range(0, 3)]), Quaternion.identity));

        Instantiate(goPremio, new Vector2(20, randomVariableYinicial6), Quaternion.identity);

        dimensionPantallaGM = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

    }

    // Update is called once per frame
    void Update()
    {
      
        //el juego corriendo
        if (start == true && gameOver == false)
        {

            goMenuPrincipal.SetActive(false);

            goTxtPuntaje.SetActive(true);
            puntajePantalla = puntajePantalla + 0.01f;
            txtPuntaje.text = puntajePantalla.ToString("F1");

            //Mover fondo   
            fondo.material.mainTextureOffset = fondo.material.mainTextureOffset + new Vector2(0.06f, 0) * Time.deltaTime;

            //Premio
            if (goPremio.transform.position.x <= -11)
            {
                float randomVariableXpremio = Random.Range(20, 35);
                float randomVariableYpremio = Random.Range(-3, 3);
                goPremio.transform.position = new Vector3(randomVariableXpremio, randomVariableYpremio, 0);
            }

            goPremio.transform.position = goPremio.transform.position + new Vector3(-0.6f, 0, 0) * Time.deltaTime * velocidad;

            if (puntajePantalla <= 50)
            {
                velocidadObstaculo = 0;
            }

            if (puntajePantalla > 50) { velocidadObstaculo = 0.5f; if (asJuego.isPlaying) { asJuego.pitch = 1.02f; } }
            if (puntajePantalla > 100) { velocidadObstaculo = 0.55f; if (asJuego.isPlaying) { asJuego.pitch = 1.04f; } }
            if (puntajePantalla > 150) { velocidadObstaculo = 0.7f; if (asJuego.isPlaying) { asJuego.pitch = 1.06f; } }
            if (puntajePantalla > 200) { velocidadObstaculo = 0.7f; if (asJuego.isPlaying) { asJuego.pitch = 1.08f; } }
            if (puntajePantalla > 250) { velocidadObstaculo = 0.7f; if (asJuego.isPlaying) { asJuego.pitch = 1.10f; } }
            if (puntajePantalla > 300) { velocidadObstaculo = 0.7f; if (asJuego.isPlaying) { asJuego.pitch = 1.12f; } }
            if (puntajePantalla > 350) { velocidadObstaculo = 0.7f; if (asJuego.isPlaying) { asJuego.pitch = 1.14f; } } 
            if (puntajePantalla > 400) { velocidadObstaculo = 0.8f; if (asJuego.isPlaying) { asJuego.pitch = 1.16f; } }
            if (puntajePantalla > 450) { velocidadObstaculo = 0.8f; if (asJuego.isPlaying) { asJuego.pitch = 1.18f; } }
            if (puntajePantalla > 500) { velocidadObstaculo = 0.8f; if (asJuego.isPlaying) { asJuego.pitch = 1.20f; } }
            if (puntajePantalla > 550) { velocidadObstaculo = 1f; if (asJuego.isPlaying) { asJuego.pitch = 1.22f; } }
            if (puntajePantalla > 600) { velocidadObstaculo = 1f; if (asJuego.isPlaying) { asJuego.pitch = 1.24f; } }
            if (puntajePantalla > 650) { velocidadObstaculo = 1.2f; if (asJuego.isPlaying) { asJuego.pitch = 1.26f; } }
            if (puntajePantalla > 700) { velocidadObstaculo = 1.2f; if (asJuego.isPlaying) { asJuego.pitch = 1.28f; } }
            if (puntajePantalla > 750) { velocidadObstaculo = 1.2f; if (asJuego.isPlaying) { asJuego.pitch = 1.30f; } }


            //Reubicación de las piedras puntaje < 100
            for (int i = 0; i < gosPiedras.Count-1; i++)
                       {
                            if (gosPiedras[i].transform.position.x <= -12)
                            {

                                int[] posicionesY = new int[] { 3,1,-1,-3, -3 };
                                float randomVariableX = Random.Range(12, 18);
                                 gosPiedras[i].transform.position = new Vector3(randomVariableX, posicionesY[Random.Range(0,4)], 0);
                                 }

                                //Mover mapa izquierda
                                if (gosPiedras.IndexOf(gosPiedras[i]) == 0)
                                {

                    gosPiedras[i].transform.position = gosPiedras[i].transform.position + new Vector3(-1.2f , 0, 0) * Time.deltaTime * velocidad;
                                }

                                 else if (gosPiedras.IndexOf(gosPiedras[i]) == 1)
                                {
                        
                                    gosPiedras[i].transform.position = gosPiedras[i].transform.position + new Vector3(-1.5f - velocidadObstaculo, 0, 0) * Time.deltaTime * velocidad;
                                }

                                 else if (gosPiedras.IndexOf(gosPiedras[i]) == 2)
                                {
                         
                                    gosPiedras[i].transform.position = gosPiedras[i].transform.position + new Vector3(-1.8f - velocidadObstaculo, 0, 0) * Time.deltaTime * velocidad;
                                }

                                else if (gosPiedras.IndexOf(gosPiedras[i]) == 3)
                                {
                         
                                    gosPiedras[i].transform.position = gosPiedras[i].transform.position + new Vector3(-1.5f - velocidadObstaculo, 0, 0) * Time.deltaTime * velocidad;
                                }

                                else if (gosPiedras.IndexOf(gosPiedras[i]) == 4)
                              {
                     
                                }
                }

        }
        if (start == true && gameOver == true)
        {
            gameObject.SendMessage("HacerRecalculo", puntajePantalla);
        }
    }

    private void ActualizaPremio(float premio)
    {
        puntajePantalla = puntajePantalla + premio;
    }
    private AudioSource AnadirAudioS(bool lazo, bool inicioComenzar, float volumen, AudioClip acClip)
    {
        AudioSource asAudio = gameObject.AddComponent<AudioSource>();
        asAudio.loop = lazo;
        asAudio.playOnAwake = inicioComenzar;
        asAudio.volume = volumen;
        asAudio.clip = acClip;
        return asAudio;
    }

  

    private void ReproducirASMuerte()
    {
        asMar.Stop();
        asHelicoptero.Stop();
        asJuego.Stop();
        asMuerte.Play();
    }
    private void MuerteLimites()
    {
        gameOver = true;
        asLimites.Play();
        asAullar.Play();
    }
    private void MuerteObstaculoCoco()
    {
        gameOver = true;
        asCoco.Play();
        asAullar.Play();
    }
    private void MuerteObstaculoPelicano()
    {
        gameOver = true;
        asPelicano.Play();
        asAullar.Play();
    }
    private void MuerteObstaculoCangrejo()
    {
        gameOver = true;
        asCangrejo.Play();
        asAullar.Play();
    }

    public void Info()
    {
        goInfo.SetActive(true);
    }
    public void SalirInfo()
    {
        goInfo.SetActive(false);
    }
    void SeteoVariablesComienzo()
    {
        start = true;
        gameOver = false;
        asIntro = gameObject.GetComponent<AudioSource>();
        asIntro.Stop();
        asPlayReiniciado();
      
    }
    void asPlayReiniciado()
    {

        asJuego = AnadirAudioS(true, false, 0.5f, acJuego);
        asJuego.Play();
        asHelicoptero = AnadirAudioS(true, false, 0.3f, acHelicoptero);
        asHelicoptero.Play();
        asMar = AnadirAudioS(true, false, 0.5f, acMar);
        asMar.Play();

    }

    //Funciones de los Botones

    public void Jugar()
    {
      
        start = true;
        asIntro.Stop();
        asJuego = AnadirAudioS(true, false, 0.5f, acJuego);
        asJuego.Play();
        asHelicoptero = AnadirAudioS(true, false, 0.3f, acHelicoptero);
        asHelicoptero.Play();
        asMar = AnadirAudioS(true, false, 0.5f, acMar);
        asMar.Play();


    }

    public void Salir()
    {
        StartCoroutine(coRSalir());
    }

    public void Reiniciar()
    {
     StartCoroutine(coRCargaEscena());
    }
    public void SonidoBoton()
    {
        asBoton.Play();
    }

    IEnumerator coRCargaEscena()
    {
        yield return new WaitForSeconds(0.3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator coRSalir()
    {
        yield return new WaitForSeconds(0.3f);
        Application.Quit();
    }
}
