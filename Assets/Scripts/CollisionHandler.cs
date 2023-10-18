using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class CollisionHandler : MonoBehaviour
{
    
    [SerializeField] AudioClip winner;
    [SerializeField] AudioClip loser;
    [SerializeField] float delay = 3f;
    [SerializeField] ParticleSystem winnerParticles;
    [SerializeField] ParticleSystem loserParticles;
    
    AudioSource audioClips;

    bool isTransitioning = false;
    bool collisionDisabled = false; 

    void Start()
    { 
       audioClips = GetComponent<AudioSource>();
    }
    void Update()
    {
        DebugKeys();
    }
       
   
    
    void OnCollisionEnter(Collision other) 
    {
        if(isTransitioning || collisionDisabled) { return; }
            switch (other.gameObject.tag)
            {
                case "Friendly":
                    Debug.Log("Friendly");
                    break;
                case "Finish":
                    LoadingSequence();
                    break;
                default:
                    CrashSequence();        
                    break;

            
            }
    }   
    void LoadingSequence()
    {
        isTransitioning = true;
        audioClips.Stop();
        audioClips.PlayOneShot(winner);
        winnerParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("LoadNextLvl", delay);
    }
    void CrashSequence()
    {
        isTransitioning = true;
        audioClips.Stop();
        audioClips.PlayOneShot(loser);
        loserParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLvl", delay);
    }
    void ReloadLvl()
    {
      int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
    void LoadNextLvl()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentScene + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLvl();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisabled = !collisionDisabled;
        }
    }
    
}

    

