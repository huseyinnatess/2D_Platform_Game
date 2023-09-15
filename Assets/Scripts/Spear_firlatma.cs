using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear_firlatma : MonoBehaviour
{
  
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spider"))
        {
            StartCoroutine(other.GetComponent<Spider>().DarbeAl(30));
        }
        
        else if (other.CompareTag("Big_Boar"))
        {
            StartCoroutine(other.GetComponent<Big_Boar>().DarbeAl(30));
            gameObject.SetActive(false);
        }
    }
}
