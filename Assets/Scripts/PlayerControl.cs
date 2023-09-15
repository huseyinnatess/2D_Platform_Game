using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Karakter Kontrolleri")]
    public float hareketHizi;
    public float ziplamaGucu;
    bool zemindeMi;
    bool ikinciKezZipla;
    float beklemeSuresi;
    float geriTepkiSuresi, geriTepkiGucu, yukariZiplat;
    bool YonSagMi;
    public static PlayerControl playerControl;
    public SpriteRenderer sr;
    Rigidbody2D rb;
    public Transform zeminKontrolNoktasi;
    public Animator animator;
    float GeriTephiHasarZamanlama;

    [Header("Sword")]
    public bool SwordVarMi;
    public float SwordBeklemeSayac;

    [Header("Spear")]
    public float SpearBeklemeSayac;
    public bool SpearVarMi;

    [Header("Bow")]
    public bool bowVarmi;
    public float bowBeklemeSayac;
    public Transform ArrowOlusmaNoktasi;

    [Header("Voice Effect")]
    public AudioSource ZiplamaSes;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
        sr = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        animator.SetBool("swordVarMi", SwordVarMi);
        animator.SetBool("spearVarMi", SpearVarMi);
        animator.SetBool("bowVarMi", bowVarmi);
        geriTepkiSuresi = 0;
        playerControl = this;
        GeriTephiHasarZamanlama = 0f;
        beklemeSuresi = 0f;
    }
    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time > beklemeSuresi)
            AttackAnimations();

        if (GameManager.gameManager != null)
        {
            if (GameManager.gameManager.healthSlider.value > 0)
            {
                if (geriTepkiSuresi <= 0 && Time.time > beklemeSuresi)
                {
                    Hareketet();
                    Zipla();
                    sr.color = new Color(255f, 255f, 255f, 1f);
                }
                else if (geriTepkiSuresi > 0)
                {
                    geriTepkiSuresi -= Time.deltaTime;
                    animator.SetFloat("hareketHizi", 0);
                    if (!zemindeMi)
                        rb.velocity = new Vector2(rb.velocity.x, yukariZiplat);
                    if (YonSagMi)
                        rb.velocity = new Vector2(-geriTepkiGucu, rb.velocity.y);
                    if (!YonSagMi)
                        rb.velocity = new Vector2(geriTepkiGucu, rb.velocity.y);

                    sr.color = new Color(255f, 0f, 0f, .5f);
                }
            }
        }

        else if (!GameManager.gameManager)
        {
            Hareketet();
            Zipla();
        }
        GeriTephiHasarZamanlama -= Time.deltaTime;
    }

    public void GeriTepki()
    {
        rb.velocity = new Vector2(0f, rb.velocity.y);
        geriTepkiSuresi = 0.5f;
        geriTepkiGucu = 5f;
        yukariZiplat = 2f;
        if (GeriTephiHasarZamanlama <= 0)
        {
            GameManager.gameManager.CanAzalt(10f);
            GeriTephiHasarZamanlama = .8f;
        }
    }
    void Hareketet()
    {
        float horizontal = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(horizontal * hareketHizi, rb.velocity.y);
        animator.SetFloat("hareketHizi", Mathf.Abs(rb.velocity.x));
        animator.SetBool("zemindeMi", zemindeMi);

        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector2(-1f, 1f);
            YonSagMi = false;

        }
        else if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector2(1f, 1f);
            YonSagMi = true;
        }
        
    }

    void AttackAnimations()
    {
        if (SpearVarMi)
        {
            animator.SetTrigger("Attack_1");
            beklemeSuresi = Time.time + SpearBeklemeSayac;
        }
        else if (SwordVarMi)
        {
            animator.SetTrigger("Attack_1");
            beklemeSuresi = Time.time + SwordBeklemeSayac;
        }
        else if (bowVarmi)
        {
            animator.SetTrigger("Attack_1");
            beklemeSuresi = Time.time + bowBeklemeSayac;
            Invoke(nameof(SilahFirlat), .55f);
            animator.SetBool("bowVarMi", bowVarmi);
        }
    }
    void Zipla()
    {

        int layerMask = 1 << 6;
        zemindeMi = Physics2D.OverlapCircle(zeminKontrolNoktasi.position, .2f, layerMask);

        if (Input.GetButtonDown("Jump") && (zemindeMi || ikinciKezZipla))
        {
            if (zemindeMi)
                ikinciKezZipla = true;
            else
                ikinciKezZipla = false;
            rb.velocity = new Vector2(rb.velocity.x, ziplamaGucu);
            ZiplamaSes.Play();
        }
    }

    public void SilahFirlat()
    {
        if (bowVarmi)
            ArrowObjeHavuz.arrowObjeHavuz.ArrowFirlat(ArrowOlusmaNoktasi, gameObject.transform);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Sword"))
        {
            SwordVarMi = true;
            bowVarmi = false;
            SpearVarMi = false;
            AnimasyonGecisAyarla();
            other.gameObject.SetActive(false);
        }

        else if (other.CompareTag("Spear"))
        {
            SpearVarMi = true;
            SwordVarMi = false;
            bowVarmi = false;
            AnimasyonGecisAyarla();
            other.gameObject.SetActive(false);
        }

        else if (other.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            GameManager.gameManager.Coin();

        }

        else if (other.CompareTag("CampFire"))
        {
            GeriTepki();
        }

        else if (other.CompareTag("Bow"))
        {
            other.gameObject.SetActive(false);
            bowVarmi = true;
            SpearVarMi = false;
            SwordVarMi = false;
            AnimasyonGecisAyarla();
        }
    }

    public void AnimasyonGecisAyarla()
    {
        animator.SetBool("swordVarMi", SwordVarMi);
        animator.SetBool("spearVarMi", SpearVarMi);
        animator.SetBool("bowVarMi", bowVarmi);
    }
    public void HareketsizYap()
    {
        rb.velocity = Vector2.zero;
        animator.SetFloat("hareketHizi", 0f);
    }
}
