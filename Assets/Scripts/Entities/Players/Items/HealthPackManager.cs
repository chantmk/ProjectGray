using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPackManager : Interactable
{
    public GameObject player;
    [SerializeField] private float dropChance;
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
        player.GetComponent<PlayerInventory>().AddHealthPack(1);
        audioSrc.PlayOneShot(pickSound, pickVolume);
        Destroy(gameObject);
    }

    public float GetDropChance()
    {
        return dropChance;
    }
}
