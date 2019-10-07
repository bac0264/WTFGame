﻿using UnityEngine;
using System.Collections;

public class Enemy : Character
{
    private void Update()
    {
        if (!IsCollider)
        {
            Vector2 temp = transform.position;
            temp.x -= Time.deltaTime;
            transform.position = temp;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        Processing(col.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
       // Processing(col.gameObject);
    }
    public void Processing(GameObject col)
    {
        {
            if (col != null && col.layer != LayerMask.NameToLayer("Ground_1")
                && col.layer != LayerMask.NameToLayer("Ground_2"))
            {
                Character charac = col.GetComponent<Character>();
                if (charac != null)
                {
                    if (charac.gameObject.layer == LayerMask.NameToLayer("MyCharacter"))
                    {
                        IsCollider = true;
                        charac.ReduceHP(ATK.value);
                        CharacterATK();
                        if (charac.HP.value == 0)
                        {
                            CharacterRun();
                            IsCollider = false;
                        }
                        else
                        {
                            OffCollider();
                            OnCollider();
                        }
                    }
                    else if (charac.gameObject.layer == LayerMask.NameToLayer("MyBuilding"))
                    {
                        Time.timeScale = 0;
                        Debug.Log("Lose");
                    }
                    else
                    {
                        // IsCollider = false;
                    }
                }
                else
                {
                    IsCollider = false;
                }
            }
            else
            {
                //  IsCollider = false;
            }
        }
    }
    public override void CharacterATK()
    {
        _Animator.Play("Enemy_4_Attack");
    }
    public override void CharacterRun()
    {
        _Animator.Play("Enemy_4_run");
    }
}
