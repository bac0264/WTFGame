using UnityEngine;
using System.Collections;

public class MyCharacterTest : CharacterTest
{

    // Update is called once per frame
    void Update()
    {
        if (!IsCollider)
        {
            Vector2 temp = transform.position;
            temp.x += Time.deltaTime;
            transform.position = temp;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        Processing(col.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
      //  Processing(col);
    }
    public void Processing(GameObject col)
    {
        if (col != null && col.layer != LayerMask.NameToLayer("Ground_1")
    && col.gameObject.layer != LayerMask.NameToLayer("Ground_2"))
        {
            CharacterTest charac = col.GetComponent<CharacterTest>();
            if (charac != null)
            {
                if (charac.gameObject.layer == LayerMask.NameToLayer("Enemy"))
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
                else if (charac.gameObject.layer == LayerMask.NameToLayer("EnemyBuilding"))
                {
                    Time.timeScale = 0;
                    Debug.Log("win");
                }
            }
            else
            {
                IsCollider = false;
            }
        }
        else
        {
            //   IsCollider = false;
        }
    }
    public override void CharacterTestATK()
    {
        _Animator.Play("War_normalMain_combo_style01_03");
    }
    public override void CharacterTestRun()
    {
        _Animator.Play("Run");
    }
}
