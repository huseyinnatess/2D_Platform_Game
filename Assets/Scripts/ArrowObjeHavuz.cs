using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowObjeHavuz : MonoBehaviour
{
    public static ArrowObjeHavuz arrowObjeHavuz;
    public List<GameObject> arrowList = new List<GameObject>();

    private void Awake()
    {
        arrowObjeHavuz = this;
    }

    public void ArrowFirlat(Transform arrowCikisNoktasi, Transform parent)
    {
        for (int i = 0; i < arrowList.Count; i++)
        {
            if (!arrowList[i].activeInHierarchy)
            {
                arrowList[i].SetActive(true);
                arrowList[i].transform.position = arrowCikisNoktasi.position;
                arrowList[i].transform.localScale = parent.localScale;
                
                if (parent.localScale.x > 0)
                    arrowList[i].GetComponent<Rigidbody2D>().velocity = 15f * arrowCikisNoktasi.right;
                else
                    arrowList[i].GetComponent<Rigidbody2D>().velocity = 15f * -arrowCikisNoktasi.right;
                StartCoroutine(ArrowActiveTrue());
                return;
            }
        }
    }

    IEnumerator ArrowActiveTrue()
    {
        yield return new WaitForSeconds(.5f);

        for (int i = 0; i < arrowList.Count; i++)
        {
            if (arrowList[i].activeInHierarchy)
                arrowList[i].SetActive(false);
        }
    }
}
