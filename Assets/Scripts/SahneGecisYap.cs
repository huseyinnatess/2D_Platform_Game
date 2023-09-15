using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SahneGecisYap : MonoBehaviour
{
    public int GecilecekSahneIndex;


    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PlayerControl>().HareketsizYap();
        other.GetComponent<PlayerControl>().enabled = false;
        FadeControl.fadeControl.Matlastir();
        StartCoroutine(SahneGecis());
    }


    IEnumerator SahneGecis()
    {
        yield return new WaitForSeconds(2f);
        SceneManager.LoadSceneAsync(GecilecekSahneIndex);
        
        yield return new WaitForSeconds(1f);
        FadeControl.fadeControl.Seffaflastir();
        
    }
}
