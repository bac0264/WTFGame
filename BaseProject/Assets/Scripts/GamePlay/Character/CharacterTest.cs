using UnityEngine;
using System.Collections;

public class CharacterTest : MonoBehaviour
{
    public bool IsCollider;

    private Collider2D _Collider;
    public Animator _Animator;
    public StatOfCharacterTest HP;
    public StatOfCharacterTest ATK;

    private void Awake()
    {
        _Collider = GetComponent<Collider2D>();
        _Animator = GetComponent<Animator>();
    }
    public void ReduceHP(float value)
    {
        HP.ReduceValue(value);
        if (HP.value < 0)
        {
            HP.value = 0;
            gameObject.SetActive(false);
        }
    }

    public void OnCollider()
    {
        Invoke("_OnCollider", 0.5f);
    }
    public void OffCollider()
    {
        Invoke("_OffCollider", 0);
    }
    private void _OnCollider()
    {
        _Collider.enabled = true;
    }
    private void _OffCollider()
    {
        _Collider.enabled = false;
    }
    public virtual void CharacterTestATK()
    {

    }
    public virtual void CharacterTestRun()
    {

    }
}
