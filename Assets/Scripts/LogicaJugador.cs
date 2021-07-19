using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicaJugador : MonoBehaviour
{
    public Vida vida;
    public bool Vida0 = false;
    //[SerializeField] private Animator animadorPerder;
    public Puntaje puntaje;
    
    void Start()
    {
        vida = GetComponent<Vida>();
        puntaje.valor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();
        RevisarPuntaje();
    }

    void RevisarVida()
    {
        if(Vida0) return;
        if(vida.valor <= 0)
        {
            AudioListener.volume = 0f;
            //animadorPerder.SetTrigger("Mostrar");
            Vida0 = true;   
            Invoke("ReiniciarJuego", 2f);         
        }
    }

    void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //SceneManager.LoadScene("Main Menu");
        puntaje.valor = 0;
        AudioListener.volume = 1f;
    }

    void RevisarPuntaje(){
        float valor1 = puntaje.valor;
        if(valor1 == 10000){
            Invoke("ReiniciarJuego", 3f);
        }
    }
}
