using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spider : MonoBehaviour
{
    [Header("Degiskenler")]
    float beklemeSayac;
    public float beklemeSuresi;
    public float speed;
    int kacincipoz;
    bool attackYapabilirMi;
    public int Health;
    int gecerliHealth;
    public Transform[] pozisyonlar;
    float SpiderYekseni;
    Transform hedefPlayer;

    [Header("Componentler")]
    Animator animator;
    BoxCollider2D orumcekCollider;
    CapsuleCollider2D orumcekHasarCollider;
    Rigidbody2D rb;
    public Slider healthSlider;
    public GameObject SpiderCanvas;
    SpriteRenderer sr;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        orumcekCollider = GetComponent<BoxCollider2D>();
        orumcekHasarCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        SpiderYekseni = transform.position.y;
        sr = GetComponent<SpriteRenderer>();
        hedefPlayer = GameObject.FindWithTag("Player").transform;
    }
    private void Start()
    {
        gecerliHealth = Health;
        kacincipoz = 0;
        attackYapabilirMi = true;
        foreach (var item in pozisyonlar)
        {
            item.parent = null;
        }
    }

    private void Update()
    {
        if (!attackYapabilirMi)
            return;

        if ((hedefPlayer.position.x > pozisyonlar[0].position.x) && (hedefPlayer.position.x < pozisyonlar[1].position.x))
        {
            animator.SetBool("hareketEtsinMi", true);
            if (transform.position.x > hedefPlayer.position.x)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            else
                transform.localScale = new Vector3(1f, 1f, 1f);

            transform.position = Vector3.MoveTowards(transform.position, hedefPlayer.position, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, SpiderYekseni, transform.position.z);
            beklemeSayac = 0;
        }

        else if (beklemeSayac > 0)
        {
            animator.SetBool("hareketEtsinMi", false);
            beklemeSayac -= Time.deltaTime;
        }

        else
        {
            animator.SetBool("hareketEtsinMi", true);

            if (transform.position.x > pozisyonlar[kacincipoz].position.x)
                transform.localScale = new Vector3(-1f, 1f, 1f);
            else
                transform.localScale = new Vector3(1f, 1f, 1f);

            transform.position = Vector3.MoveTowards(transform.position, pozisyonlar[kacincipoz].position, speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, SpiderYekseni, transform.position.z);

            if (Vector3.Distance(transform.position, pozisyonlar[kacincipoz].position) <= 5f)
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
        if (orumcekCollider.IsTouchingLayers(LayerMask.GetMask("Player")) && attackYapabilirMi)
        {
            attackYapabilirMi = false;
            animator.SetBool("hareketEtsinMi", false);
            animator.SetTrigger("attackYapsinMi");
            StartCoroutine(AttackYap());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (orumcekHasarCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
            GameManager.gameManager.CanAzalt(10f);
        if (orumcekCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
            PlayerControl.playerControl.GeriTepki();
    }


    IEnumerator AttackYap()
    {
        yield return new WaitForSeconds(.8f);
        if (gecerliHealth > 0)
            attackYapabilirMi = true;
    }

    public IEnumerator DarbeAl(int hasar)
    {
        attackYapabilirMi = false;
        gecerliHealth -= hasar;
        healthSlider.value = gecerliHealth;
        SpiderCanvas.SetActive(true);
        sr.color = new Color(255f, 0f, 0f, 1f);
        animator.SetBool("hareketEtsinMi", true);

        if (gecerliHealth <= 0)
        {
            SpiderCanvas.SetActive(false);
            gecerliHealth = 0;
            animator.SetTrigger("olduMu");
            orumcekCollider.enabled = false;
            yield return new WaitForSeconds(2f);
            gameObject.SetActive(false);
        }
        else
        {
            yield return new WaitForSeconds(.3f);
            sr.color = new Color(255f, 255f, 255f, 1f);
            attackYapabilirMi = true;
        }
    }
}
