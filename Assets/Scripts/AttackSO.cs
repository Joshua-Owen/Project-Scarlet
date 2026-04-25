using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AttackSO", menuName = "Scriptable Objects/AttackSO")]
public class AttackSO : ScriptableObject
{
    public AnimatorOverrideController animOv;
    public AnimationClip clip;
    public float damage;
    public float knockBack;
    public float stamina;
}
