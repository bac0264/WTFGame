using UnityEngine;
using System.Collections;

public class Enemy : CharacterTest
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
                CharacterTest charac = col.GetComponent<CharacterTest>();
                if (charac != null)
                {
                    if (charac.gameObject.layer == LayerMask.NameToLayer("MyCharacterTest"))
                    {
                        IsCollider = true;
                        charac.ReduceHP(ATK.value);
                        CharacterTestATK();
                        if (charac.HP.value == 0)
                        {
                            CharacterTestRun();
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
    public override void CharacterTestATK()
    {
        _Animator.Play("Enemy_4_Attack");
    }
    public override void CharacterTestRun()
    {
        _Animator.Play("Enemy_4_run");
    }
}
