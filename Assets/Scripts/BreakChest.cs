using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakChest : MonoBehaviour
{
    int breakChestSayac;
    public GameObject parlamaEfekti;
    Animator animator;

    Vector2 patlamaMiktari = new(1,4);
    public GameObject OlusacakCoin;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            animator.SetTrigger("sallanma");
            breakChestSayac++;

            if (breakChestSayac == 3)
            {
                animator.SetTrigger("break");
              
                for (int i = 0; i < 3; i++)
                {
                    Vector2 RastgeleVektor = new Vector2(transform.position.x + (i - 1), transform.position.y);
                    GameObject coin = Instantiate(OlusacakCoin, RastgeleVektor, transform.rotation);
                    coin.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
                    coin.GetComponent<Rigidbody2D>().velocity = patlamaMiktari * new Vector2(Random.Range(1, 2) + transform.localScale.x, Random.Range(1, 2) + transform.localScale.y);
                }

                breakChestSayac = 0;
            }
            Instantiate(parlamaEfekti, transform.position, transform.rotation);
            StartCoroutine(SetActive());
        }
    }

    IEnumerator SetActive()
    {
        yield return new WaitForSeconds(2f);
        parlamaEfekti.SetActive(false);

        if (breakChestSayac == 3)
            gameObject.SetActive(false);
    }

}
