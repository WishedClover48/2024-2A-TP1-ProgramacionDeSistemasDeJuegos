using Audio;
using Enemies;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "VisualEffect", menuName = "Pools/VisualPool")]

public class VFX_SO : ScriptableObject
{
    public RandomContainer<ParticleSystem> _particleEffects = new RandomContainer<ParticleSystem>();
}
