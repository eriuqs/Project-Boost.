using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    AudioSource thrustNoise;
    Rigidbody rb;
    [SerializeField] float thrustSpeedUp = 300f;
    [SerializeField] float thrustSpeedRotate = 200f;
    [SerializeField] AudioClip engine;

    [SerializeField] ParticleSystem mainThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        thrustNoise = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }
    void ProcessThrust()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            rb.AddRelativeForce(Vector3.up * thrustSpeedUp * Time.deltaTime);
            if(!thrustNoise.isPlaying)
            {
                thrustNoise.PlayOneShot(engine);
            }
            if(!mainThrustParticles.isPlaying)
            {
                mainThrustParticles.Play();
            }
        }
        else
        {
            thrustNoise.Stop();
            mainThrustParticles.Stop();
        }

        
    }
    void ProcessRotation()
    {
        if(Input.GetKey(KeyCode.A))
        {
            ApplyRotation(thrustSpeedRotate);
            if(!rightThrustParticles.isPlaying)
            {
                rightThrustParticles.Play();
            }
        }
        else if(Input.GetKey(KeyCode.D))
        {
           ApplyRotation(-thrustSpeedRotate);
           if(!leftThrustParticles.isPlaying)
            {   
                leftThrustParticles.Play();
            }
        }
        else
        {
            rightThrustParticles.Stop();
            leftThrustParticles.Stop();
        }
    }
    void ApplyRotation(float thrustSpeed)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * thrustSpeed * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
