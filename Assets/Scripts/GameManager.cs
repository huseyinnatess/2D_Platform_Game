using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text CoinText;
    int toplamCoin = 0;

    public GameObject player;
    public static GameManager gameManager;
    
    private void Awake()
    {
        gameManager = this;
    }
    public void CanAzalt(float DarbeGucu)
    {
        healthSlider.value -= DarbeGucu;
        if (healthSlider.value <= 0)
        {
            PlayerControl.playerControl.animator.SetTrigger("olduMu");
            StartCoroutine(SahneYenile());
        }

    }

    IEnumerator SahneYenile()
    {
        yield return new WaitForSeconds(2f);
        PlayerControl.playerControl.sr.enabled = false;

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

    }
    public void Coin()
    {
        toplamCoin++;
        CoinText.text = toplamCoin.ToString();
    }

   /* public void PauseYap()
    {
        if(!pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void YenidenBaslat()
    {
        SceneManager.LoadSceneAsync(2);
    } */
}
