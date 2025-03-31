using UnityEngine;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private AudioSource footstepSource;
    [SerializeField] private AudioClip[] footstepSounds;
    [SerializeField] private float stepInterval = 0.3f; // Adjust for walking speed

    [SerializeField] private Rigidbody2D playerRB;
    private float stepTimer;

    void Start()
    {
        stepTimer = stepInterval;
    }

    void Update()
    {
        if (playerRB.velocity.magnitude > 0.1f)
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }
    }

    void PlayFootstep()
    {
        footstepSource.clip = footstepSounds[Random.Range(0, footstepSounds.Length)];
        footstepSource.Play();
    }
}