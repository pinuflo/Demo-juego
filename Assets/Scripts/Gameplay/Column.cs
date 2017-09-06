using UnityEngine;
using System.Collections;

public class Column : MonoBehaviour 
{
    //Si una moneda toca la muralla invisible, debe destruirse
    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Coin")
        {
            Destroy(other.gameObject);
        }


    }

}
