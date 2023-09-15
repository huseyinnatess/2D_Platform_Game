using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCarpisma : MonoBehaviour
{
    public GameObject matBlock;
    public GameObject parlayanBlock;
    public Transform hitPoint;

    bool hareketEtsinMi = false;
    bool hareketEttiMi = false;
    Vector2 orjinalPos;
    Vector2 hareketyonu = Vector2.up;
    Vector2 hareketPos;
    private void Start()
    {
        orjinalPos = transform.position;
        hareketPos = transform.position;
        hareketPos.y += 0.15f;
    }

    private void Update()
    {
        Hareket();
    }
    void Hareket()
    {
        if(!hareketEttiMi)
        {
            int layerMask = 1 << 3;
            RaycastHit2D hit = Physics2D.Raycast(hitPoint.position, Vector2.down, .1f, layerMask);
            Debug.DrawRay(hitPoint.position, Vector2.down, Color.red, .1f);

            if (hit)
            {
                parlayanBlock.SetActive(false);
                matBlock.SetActive(true);
                hareketEtsinMi = true;
                HareketEtsinMi();
                hareketEttiMi = false;
            }
        }
    }

    void HareketEtsinMi()
    {
        if (hareketEtsinMi)
        {
            transform.Translate(hareketyonu * Time.smoothDeltaTime);

            if (transform.position.y >= hareketPos.y)
                hareketyonu = Vector2.down;
            else if (transform.position.y <= orjinalPos.y)
                hareketEtsinMi = false;

        }




    }
}
