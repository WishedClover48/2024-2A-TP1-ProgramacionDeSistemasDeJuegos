using Audio;
using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SoundEffect", menuName = "Pools/SoundPool")]
public class SFX_SO : ScriptableObject
{
    public RandomContainer<AudioClipData> _soundEffects = new RandomContainer<AudioClipData>();

}
