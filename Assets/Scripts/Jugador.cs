using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Jugador : MonoBehaviour
{
    private Animator atrPugs;

    private float fuerzaUp = 7;
    private float fuerzaDown = -4f;
    private float fuerzaLenta =2f;

    private Rigidbody2D rbPug;
    public GameManager gameManager;
    private Vector3 dimensionPantalla;

    public AudioClip acCrash;
    public AudioClip acPremio;
    public AudioClip acMuerte;

    private AudioSource asCrash;
    private AudioSource asPremio;
    private AudioSource asMuerte;

    public GameObject gogmObjeto;


    bool choqueObstaculo = false;
    bool enPrecipicio = false;

    float tempo1;
    float tempo2;

    // Start is called before the first frame update
    void Start()
    {
       atrPugs = GetComponent<Animator>();
       rbPug = GetComponent<Rigidbody2D>();

        asCrash = AnadirAudioS(false, false, 1f, acCrash);
        asPremio = AnadirAudioS(false, false, 1f, acPremio);
        asMuerte = AnadirAudioS(false, false, 1f, acMuerte);

        dimensionPantalla = Camera.main.ScreenToWorldPoint(new Vector3(0, Screen.height, 0));
      
    }

    void FixedUpdate()
    {

        //saltar en el celular
        if (choqueObstaculo == false && gameManager.start==true)
  {

            if (Input.touchCount == 1)
             {
                
                Touch touch = Input.GetTouch(0);

                atrPugs.SetInteger("animVariable", 0);

                if (touch.position.x < Screen.width / 2)
                {
                    atrPugs.SetInteger("animVariable", 1);
                    gameObject.transform.Translate(fuerzaUp * Vector3.up * Time.deltaTime);
                }

                if (touch.position.x >= Screen.width / 2)
                {
                    atrPugs.SetInteger("animVariable", 2);
                    gameObject.transform.Translate(fuerzaDown * Vector3.up * Time.deltaTime);
                }

            }
 
        }

        
              //saltar en la compu
            if (choqueObstaculo == false && gameManager.start == true)
            {

            atrPugs.SetInteger("animVariable", 0);
                         if (Input.GetKey(KeyCode.D))
                         {
                             atrPugs.SetInteger("animVariable", 1);
                             gameObject.transform.Translate(fuerzaUp * Vector3.up * Time.deltaTime);
                         }
                      if (Input.GetKey(KeyCode.S))
                          {
                             atrPugs.SetInteger("animVariable", 2);
                             gameObject.transform.Translate(fuerzaDown * Vector3.up * Time.deltaTime);
                           }

            }

    }

    // Update is called once per frame
    void Update()
    {

            if (transform.position.y <= -dimensionPantalla.y || transform.position.y >= dimensionPantalla.y)
        {
            if (choqueObstaculo==false)
            {
                gogmObjeto.SendMessage("ReproducirASMuerte");
            }

            gogmObjeto.SendMessage("MuerteLimites");
            gameManager.gameOver = true;
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Premio")
        {
            atrPugs.SetTrigger("animTrigger");
             
            asPremio.Play();
            collision.gameObject.transform.position = new Vector3(-15, 2, 0);
            gogmObjeto.SendMessage("ActualizaPremio", 30);

        }

        if (collision.gameObject.tag == "ObstaculoCoco")
        {
            MuertexCoco();
        }

        if (collision.gameObject.tag == "ObstaculoPelicano")
        {
            MuertexPelicano();
        }

        if (collision.gameObject.tag == "ObstaculoCangrejo")
        {
            MuertexCangrejo();
        }
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
    void MuertexCoco()
    {
        gogmObjeto.SendMessage("MuerteObstaculoCoco");
        gogmObjeto.SendMessage("ReproducirASMuerte");
        choqueObstaculo = true;
        gameObject.SetActive(false);
    }

    void MuertexPelicano()
    {
        gogmObjeto.SendMessage("MuerteObstaculoPelicano");
        gogmObjeto.SendMessage("ReproducirASMuerte");
        choqueObstaculo = true;
        gameObject.SetActive(false);
    }

    void MuertexCangrejo()
    {
        gogmObjeto.SendMessage("MuerteObstaculoCangrejo");
        gogmObjeto.SendMessage("ReproducirASMuerte");
        choqueObstaculo = true;
        gameObject.SetActive(false);
    }

    public void BotonSaltar()
    {
        gameObject.transform.Translate(100 * Vector3.up * Time.deltaTime);
    }

    IEnumerator coRPremio()
    {
        yield return new WaitForSeconds(0.5f);
    }
}
