using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salinan_Axe : MonoBehaviour
{
    float donmeHizi = 100f;
    float zAngle;

    float minZAngle = -75f;
    float maxZAngle = 75f;
    private void Update()
    {
        zAngle += donmeHizi * Time.deltaTime;

        if (zAngle < minZAngle)
            donmeHizi *= -1;

        else if (zAngle > maxZAngle)
            donmeHizi = -donmeHizi;
        
        transform.rotation = Quaternion.AngleAxis(zAngle, Vector3.forward);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
        {
            PlayerControl.playerControl.GeriTepki();
            print("Çarpti");
        }
    }
}
