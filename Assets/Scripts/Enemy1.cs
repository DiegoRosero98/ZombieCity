using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Enemy1 : MonoBehaviour
{
    public int rutina;
    public float cronometro;
    private Animator ani;
    public Quaternion angulo;
    public float grado;
    private GameObject target;
    private NavMeshAgent agente;
    public bool atacando;
    private Vida vida;
    private Collider collider1;
    private Vida vidaJugador;
    private LogicaJugador logicaJugador;
    public bool estaAtacando = false;
    public float speed = 0.5f;
    public float angularSpeed = 80;
    public float daño = 1;
    public bool Vida0 = false;
    public bool mirando;
    public bool sumarPuntos = false;
    public GameObject puntajePantalla;
    public AudioSource deathSoundBoss;
    void Start()
    {
        target = GameObject.Find("Player");
        vidaJugador = target.GetComponent<Vida>();
        if(vidaJugador== null)
        {
            throw new System.Exception("El objeto Jugador no tiene componente Vida");
        }

        logicaJugador = target.GetComponent<LogicaJugador>();

        if (logicaJugador == null)
        {
            throw new System.Exception("El objeto Jugador no tiene componente LogicaJugador");
        }

        //agente = GetComponent<NavMeshAgent>();
        vida = GetComponent<Vida>();
        ani = GetComponent<Animator>();
        collider1 = GetComponent<Collider>();
        
    }

    public void Comportamiento_Enemigo()
    {
        if(Vector3.Distance(transform.position, target.transform.position) > 10)
        {
            ani.SetBool("run", false);
            cronometro += 1 * Time.deltaTime;
            if(cronometro >= 4)
            {
                rutina = Random.Range(0, 2);
                cronometro = 0;
            }
            switch (rutina)
            {
                case 0:
                    ani.SetBool("walk", false);
                    break;
                case 1:
                    grado = Random.Range(0, 360);
                    angulo = Quaternion.Euler(0, grado, 0);
                    rutina++;
                    break;
                case 2:
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                    transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                    ani.SetBool("walk", true);
                    break;
            }
        }
        else
        {
            if (Vector3.Distance(transform.position, target.transform.position) > 5 && !atacando)
            {
                var lookPos = target.transform.position - transform.position;
                lookPos.y = 0;
                var rotation = Quaternion.LookRotation(lookPos);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 3);
                ani.SetBool("walk", false);

                ani.SetBool("run", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);

                ani.SetBool("attack", false);
            }
            else
            {
                ani.SetBool("walk", false);
                ani.SetBool("run", false);

                if(vidaJugador.valor > 0)
                {
                    ani.SetBool("attack", true);
                    atacando = true;
                    vidaJugador.RecibirDañoJefe(daño);                  
                }
                if(vidaJugador.valor <= 0)
                {
                    atacando = false;
                    ani.SetBool("walk", true);
                }
            }
        }
        if(vida.valor <= 0)
        {
            deathSoundBoss.Play();
            Destroy(gameObject, 0.5f);
            sumarPuntos = true;
            if(sumarPuntos)
            {
                puntajePantalla.GetComponent<Puntaje>().valor += 300;
                sumarPuntos = false;
            }
        }
    }

    public void Final_Ani()
    {
        ani.SetBool("attack", false);
        atacando = false;
    }

    void Update()
    {
        Comportamiento_Enemigo();
    }

}
