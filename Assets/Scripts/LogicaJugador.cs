using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicaJugador : MonoBehaviour
{
    public Vida vida;
    public bool Vida0 = false;

    [Header("UI")]
    public Text winText;
    public Text loseText;
    public Text instText;
    //[SerializeField] private Animator animadorPerder;
    public Puntaje puntaje;
    
    void Start()
    {
        winText.gameObject.SetActive(false);
        loseText.gameObject.SetActive(false);
        Invoke("DesactivarTexto", 5f);
        vida = GetComponent<Vida>();
        puntaje.valor = 0;
    }

    // Update is called once per frame
    void Update()
    {
        RevisarVida();
        RevisarPuntaje();
    }

    void DesactivarTexto()
    {
        instText.enabled = false;
    }
    void RevisarVida()
    {
        if(Vida0) return;
        if(vida.valor <= 0)
        {
            //AudioListener.volume = 0f;
            loseText.gameObject.SetActive(true);
            winText.text = "DERROTA!"; 
            //animadorPerder.SetTrigger("Mostrar");
            Vida0 = true;   
            Invoke("ReiniciarJuego", 2f);         
        }
    }

    void ReiniciarJuego()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        SceneManager.LoadScene("Menu");
        puntaje.valor = 0;
        AudioListener.volume = 1f;
    }

    void RevisarPuntaje(){
        float valor1 = puntaje.valor;
        if(valor1 >= 35000){
            winText.gameObject.SetActive(true);
            winText.text = "VICTORIA!"; 
            Invoke("ReiniciarJuego", 3f);
        }
    }
}
