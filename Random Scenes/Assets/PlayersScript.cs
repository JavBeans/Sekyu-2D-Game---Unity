using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Base1") || other.gameObject.CompareTag("Base2"))
        {

        }


        if(other.gameObject.CompareTag("Player1"))
        {
            if(Base1Script.playernumber1 > Base2Script.playernumber2)
            {
                Destroy(gameObject);
            }
        }
        else if(other.gameObject.CompareTag("Player2"))
        {
            if (Base2Script.playernumber2 > Base1Script.playernumber1)
            {
                Destroy(gameObject);
            }
        }
    }
}
