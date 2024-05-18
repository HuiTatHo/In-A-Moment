using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("----- Audio Soure -----")]
    [SerializeField] AudioSource musicSource;
    [SerializeField] AudioSource SfxSource;
    [SerializeField] AudioSource UiSource;

    [Header("----- Audio Clip -----")]
    public AudioClip backgroud;
    public AudioClip death;

    public AudioClip sowrd_swap;
    public AudioClip sowrd_charge;

    public AudioClip gun_shot;

    public AudioClip burnning;
    public AudioClip ui_hover;
    public AudioClip ui_click;
}
