using UnityEngine;

[CreateAssetMenu]
public class AttackData : ScriptableObject
{
    public string animationName;
    public float damage;
    public float knockback;
    public float comboWindowStart;
    public float combowindowEnd;
}