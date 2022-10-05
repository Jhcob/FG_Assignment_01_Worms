using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private LocomotionInputController movementController;
    
    [SerializeField] private AudioClip steps;
    [SerializeField] AudioSource source;

    [SerializeField] TurnManager turnManager;
    [SerializeField] AudioMixerGroup mixerOutput;
    [SerializeField] float pitchMin = 0.95f, pitchMax = 1.05f, volumeMin = 0.95f, volumeMax  = 1f;
    [SerializeField] private float walkingSpeed;
    private bool playerIsMoving;
    
    // Start is called before the first frame update
    void Start()
    {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            CallFootsteps();
            Debug.Log("hittng ground");
        }
    }

    void CallFootsteps()
    {
        ActivePlayer currentPlayer = turnManager.GetCurrentPlayer();

        if (playerIsMoving == true)
        {
            source.pitch = Random.Range(pitchMin, pitchMax);
            source.volume = Random.Range(volumeMin, volumeMax);
            currentPlayer.GetComponent<AudioSource>().PlayOneShot(steps);
        }
    }
}
