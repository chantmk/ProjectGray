using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResemblanceManager : Interactable
{
    public GameObject player;
    public AudioClip pickSound;
    public float pickVolume = 1f;
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
        audioSrc.PlayOneShot(pickSound, pickVolume);
        Destroy(gameObject);
    }
}
