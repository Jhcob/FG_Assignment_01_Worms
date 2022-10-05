using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class FootstepsSpider : MonoBehaviour
{
    [SerializeField] private LocomotionInputController movementController;
    
    [SerializeField] private AudioClip steps;
    AudioSource source;

    [SerializeField] TurnManager turnManager;
    [SerializeField] AudioMixerGroup mixerOutput;
    [SerializeField] float pitchMin = 0.95f, pitchMax = 1.05f, volumeMin = 0.95f, volumeMax  = 1f;
    [SerializeField] private float walkingSpeed;
    
    private bool playerIsMoving;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CallFootsteps", 0, walkingSpeed);
        source = gameObject.AddComponent<AudioSource>();
        source.outputAudioMixerGroup = mixerOutput;
    }

    // Update is called once per frame
    void Update()
    {
        ActivePlayer currentPlayer = turnManager.GetCurrentPlayer();

        if (Mathf.Abs(currentPlayer.GetComponent<CharacterController>().velocity.magnitude) >= 0.03f)
        {
            playerIsMoving = true;
        }
        else
        {
            playerIsMoving = false;
        }
    }


    void CallFootsteps()
    {
        if (playerIsMoving == true)
        {
            source.pitch = Random.Range(pitchMin, pitchMax);
            source.volume = Random.Range(volumeMin, volumeMax);
            source.PlayOneShot(steps);
        }
    }
}
