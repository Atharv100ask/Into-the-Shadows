using UnityEngine;

public class PlayScreamSoundBehavior : StateMachineBehaviour
{
    public AudioClip screamClip; // Assign in the Animator
    private AudioSource audioSource;
    
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (audioSource == null)
        {
            audioSource = animator.GetComponent<AudioSource>();
        }

        if (audioSource != null && screamClip != null)
        {
            audioSource.clip = screamClip;
            audioSource.Play();
        }
    }
}
