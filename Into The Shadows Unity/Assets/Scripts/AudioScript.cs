using UnityEngine;

public class Audio : StateMachineBehaviour
{
    public AudioClip screamClip;
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
