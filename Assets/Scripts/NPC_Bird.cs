using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Bird : MonoBehaviour
{
    public Transform[] posizyonlar;
    int randompoz;
    public float hiz;

    public float beklemeSuresi;
    float beklemeSayac;

    Animator animator;
    Vector2 kusYonu;

    private void Start()
    {
        animator = GetComponent<Animator>();
        
        randompoz = 0;
        
        foreach (var item in posizyonlar)
        {
            item.parent = null;
        }

        transform.position = posizyonlar[randompoz].position;
    }

    private void Update()
    {
        if (beklemeSayac > 0)
        {
            beklemeSayac -= Time.deltaTime;
            animator.SetBool("ucsunMu",false);
        }
            
        else
        {
            kusYonu = new Vector2(posizyonlar[randompoz].position.x - transform.position.x, posizyonlar[randompoz].position.y - transform.position.y);
            float angle = Mathf.Atan2(kusYonu.y, kusYonu.x) * Mathf.Rad2Deg;
            print(angle);
            if (transform.position.x > posizyonlar[randompoz].position.x)
                transform.localScale = new Vector3(1f, -1f, 1f);
            else
                transform.localScale = new Vector3(1f, 1f, 1f);

            transform.SetPositionAndRotation(Vector3.MoveTowards(transform.position, posizyonlar[randompoz].position, hiz * Time.deltaTime), Quaternion.Euler(0f, 0f, angle));
            animator.SetBool("ucsunMu", true);
            if (Vector3.Distance(transform.position, posizyonlar[randompoz].position) <= 0.1f)
            {
                beklemeSayac = beklemeSuresi;
                randompoz = Random.Range(0, posizyonlar.Length - 1);
            }

        }

    }
}
