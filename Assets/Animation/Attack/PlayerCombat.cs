using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class PlayerCombat : MonoBehaviour {   
    Animator anim;
    PlayerInput input;
    public List<AttackSO> combo;
    float lastClickedTime;
    public float lastComboEnd;
    public int comboCounter;
    public float comboBuffer = 0.2f;
    public float animPercentage = 0.9f;
    //bool isAttacking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        anim = GetComponentInChildren<Animator>();
        input = GetComponent<PlayerInput>();
        //isAttacking = false;
    }

    // Update is called once per frame
    void Update(){
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }
        
        
        // if(input.attackValue){
        //     Attack();
        // }
        ExitAttack();
    }

    void Attack(){
        if(Time.time - lastComboEnd > 0.5f  && comboCounter <= combo.Count /*&& isAttacking*/ ){
            CancelInvoke("EndCombo");

            if( Time.time - lastClickedTime >= comboBuffer){
                anim.runtimeAnimatorController = combo[comboCounter].animOv;
                anim.Play("Attack", 0,0);
                //Debug.Log($"attack : {attackClip} performed");
                comboCounter++;
                //isAttacking = false;
                lastClickedTime = Time.time;
                if(comboCounter >= combo.Count){
                    comboCounter = 0;
                }
            }



            //  if( Time.time - lastClickedTime >= comboBuffer){
            //     string attackClip = combo[comboCounter].clip.name;
            //     anim.CrossFade(attackClip, 0,0);
            //     //Debug.Log($"attack : {attackClip} performed");
            //     comboCounter++;
            //     //isAttacking = false;
            //     lastClickedTime = Time.time;

            //     if(comboCounter >= combo.Count){
            //         comboCounter = 0;
            //     }
            // }
        }
    }

    void ExitAttack(){
        if( anim.GetCurrentAnimatorStateInfo(0).normalizedTime > animPercentage && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            Invoke("EndCombo", 1f);
        }
    }

    void EndCombo(){
        comboCounter = 0;
        lastComboEnd = Time.time;
        //anim.CrossFade("Idle", 0.1f);
    }
}
