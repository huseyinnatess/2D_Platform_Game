using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Big_Boar : MonoBehaviour
{
    public Transform[] pozisyonlar;
    public float speed;
    int kacincipoz;
    float beklemeSayac;
    public float beklemeSuresi;
    bool attackYapsinMi;

    Animator animator;
    Rigidbody2D rb;
    BoxCollider2D boarCollider;
    CapsuleCollider2D AttackCollider;

    Transform hedefPlayer;
    float boarYekseni;

    int health;
    public int disaridanHealth;
    SpriteRenderer sr;

    public Slider healthSlider;
    public GameObject bigBoarCanvas;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        boarCollider = GetComponent<BoxCollider2D>();
        hedefPlayer = GameObject.FindWithTag("Player").transform;
        AttackCollider = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    private void Start()
    {
        kacincipoz = 0;
        boarYekseni = transform.position.y;
        attackYapsinMi = true;
        health = disaridanHealth;
        foreach (var item in pozisyonlar)
        {
            item.parent = null;
        }
    }

    private void Update()
    {
        if (!attackYapsinMi)
            return;
        if (GameManager.gameManager.healthSlider.value <= 0)
            return;

            if ((hedefPlayer.position.x > pozisyonlar[0].position.x) && (hedefPlayer.position.x < pozisyonlar[1].position.x))
        {
            animator.SetBool("yurusunMu", false);
            animator.SetBool("kossunMu", true);
            speed = 4;
            if (transform.position.x > hedefPlayer.position.x)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            else
                transform.localScale = new Vector3(1f, 1f, 1f);

            transform.position = Vector3.MoveTowards(transform.position, hedefPlayer.position, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, boarYekseni, transform.position.z);
            beklemeSayac = 0;
        }

        else if (beklemeSayac > 0)
        {
            beklemeSayac -= Time.deltaTime;
            animator.SetBool("yurusunMu", false);
            animator.SetBool("kossunMu", false);
        }

        else
        {
            speed = 3;
            animator.SetBool("yurusunMu", true);
            animator.SetBool("kossunMu", false);
            
            if (transform.position.x > pozisyonlar[kacincipoz].position.x)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            else
                transform.localScale = new Vector3(1f, 1f, 1f);

            transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacincipoz].position, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, boarYekseni, transform.position.z);

            if (Vector3.Distance(transform.position, pozisyonlar[kacincipoz].position) < 3f)
            {
                beklemeSayac = beklemeSuresi;
                kacincipoz++;

                if (kacincipoz > pozisyonlar.Length - 1)
                    kacincipoz = 0;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (boarCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && attackYapsinMi)
        {
            if(GameManager.gameManager.healthSlider.value > 0)
            {
                attackYapsinMi = false;
                animator.SetBool("kossunMu", false);
                animator.SetTrigger("attackYapsinMi");
                PlayerControl.playerControl.GeriTepki();
                StartCoroutine(AttackYap());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (AttackCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            PlayerControl.playerControl.GeriTepki();
            GameManager.gameManager.CanAzalt(20f);
        }
    }
    IEnumerator AttackYap()
    {
        yield return new WaitForSeconds(1f);
        attackYapsinMi = true;
    }

    public IEnumerator DarbeAl(int hasar)
    {
        attackYapsinMi = false;
        health -= hasar;
        bigBoarCanvas.SetActive(true);
        healthSlider.value -= hasar;
        animator.SetBool("kossunMu", false);
        animator.SetBool("yurusunMu", false);
        sr.color = new Color(255f, 0f, 0f, 1f);
        
        if (health <= 0)
        {
            health = 0;
            animator.SetTrigger("olduMu");
            rb.velocity = new Vector2(0f, 0f);
            boarCollider.enabled = false;
            bigBoarCanvas.SetActive(false);
            sr.color = new Color(255f, 255f, 255f, 1f);
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);  
        }

        else
        {
           /* for(int i = 0; i < 3; i++)
            {
                rb.velocity = new Vector2(-transform.localScale.x + i, rb.velocity.y);
                yield return new WaitForSeconds(0.05f);
            } */

            //animator.SetBool("kossunMu", true);
           
            attackYapsinMi = true;
            sr.color = new Color(255f, 255f, 255f, 1f);
        }
       
    }
}
