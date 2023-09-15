using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHasar_Collider : MonoBehaviour
{
    [Header("Sword")]
    public int SwordSpiderHasar;
    public int SwordBigBoarHasar;

    [Header("Spear")]
    public int SpearSpiderHasar;
    public int SpearBigBoarHasar;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (PlayerControl.playerControl.SwordVarMi)
        {
            if (other.CompareTag("Spider"))
                StartCoroutine(other.GetComponent<Spider>().DarbeAl(SwordSpiderHasar));

            else if (other.CompareTag("Big_Boar"))
                StartCoroutine(other.GetComponent<Big_Boar>().DarbeAl(SwordBigBoarHasar));
        }

        else if (PlayerControl.playerControl.SpearVarMi)
        {
            if (other.CompareTag("Spider"))
                StartCoroutine(other.GetComponent<Spider>().DarbeAl(SpearSpiderHasar));

            else if (other.CompareTag("Big_Boar"))
                StartCoroutine(other.GetComponent<Big_Boar>().DarbeAl(SpearBigBoarHasar));
        }
    }
}
