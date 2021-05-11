using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResemblanceManager : Interactable
{
    public GameObject player;
    public AudioClip pickingSound;
    public float soundVolume = 1f;
    private AudioSource audioSrc;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectsWithTag("Player")[0];
        audioSrc = GameObject.FindGameObjectsWithTag("Audio")[0].GetComponent<AudioSource>();
    }

    public override void OnInteract()
    {
        player.GetComponent<PlayerInventory>().AddResemblance(1);
        Debug.Log("picked");
        audioSrc.PlayOneShot(pickingSound, soundVolume);
        Destroy(gameObject);
    }
}
