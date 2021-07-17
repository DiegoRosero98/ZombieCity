using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalanceoDeArma : MonoBehaviour
{
    public float cantidad;
    public float cantidadMax;
    public float tiempo;
    private Vector3 PosInicial;
    public bool seBalancea;

    void Start()
    {
        PosInicial = transform.localPosition;
    }
   
    void Update()
    {
        seBalancea = true;
        float movX = Input.GetAxis("Mouse X") * cantidad;
        float movY = Input.GetAxis("Mouse Y") * cantidad;
        movX = Mathf.Clamp(movX, -cantidadMax, cantidadMax); //Clamp DA UN VALOR ENTRE -5 Y 5
        movY = Mathf.Clamp(movY, -cantidadMax, cantidadMax);
        Vector3 PosFinalMov = new Vector3 (movX, movY, 0);
        if(Input.GetMouseButton(1))
        {
            seBalancea = false;
        }
        if(seBalancea == true)
        {
            transform.localPosition =  Vector3.Lerp(transform.localPosition, PosFinalMov + PosInicial, tiempo * Time.deltaTime);
        }
    }
}
