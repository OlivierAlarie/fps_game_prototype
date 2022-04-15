using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAudioManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource _HurtClip;
    [SerializeField]
    private AudioSource _DetectionClip;
    [SerializeField]
    private AudioSource _AttackClip;

    public void PlayClip(string clipname)
    {
        if(clipname == "Hurt" && _HurtClip != null && !_HurtClip.isPlaying)
        {
            _HurtClip.Play();
        }
        if(clipname == "Attack" && _AttackClip != null && !_AttackClip.isPlaying)
        {
            _AttackClip.Play();
        }
    }
}
