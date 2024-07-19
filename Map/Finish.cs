using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    public ParticleSystem particles;
    public GameObject finishText, finishPanel;
    private AudioSource audioSource;
    public AudioClip finishSound;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = finishSound;
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        playerMovement.OnFinish.AddListener(Finishing);
    }
    void Finishing()
    {
        particles.Play();
        finishText.SetActive(false);
        finishPanel.SetActive(false);
        audioSource.Play();
    }

}
