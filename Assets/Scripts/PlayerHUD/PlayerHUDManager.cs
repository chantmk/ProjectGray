using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHUDManager : MonoBehaviour
{

    private GameObject bossHealth;
    private GameObject playerBar;

    void Start()
    {
        bossHealth = transform.Find("BossHealthContainer").gameObject;
        playerBar = transform.Find("PlayerBar").gameObject;
        bossHealth.SetActive(false);

        EventPublisher.PlayCutscene += HideBar;
        EventPublisher.EndCutscene += ShowBar;
    }

    private void OnDestroy()
    {
        EventPublisher.PlayCutscene -= HideBar;
        EventPublisher.EndCutscene -= ShowBar;
    }

    public void HideBar()
    {
        bossHealth.SetActive(false);
        playerBar.SetActive(false);
    }

    public void ShowBar()
    {
        bossHealth.SetActive(true);
        playerBar.SetActive(true);
    }
}
