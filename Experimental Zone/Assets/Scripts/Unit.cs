using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField, Range(0, 100)] private float HitPoints;
    [SerializeField] public AttackType Attack; // ��� ���� ������ ����������. 1) ����������� ������� ��������� ������ � ��, ��������/�������. 
    //2) ������������� �����������: ����������-��������, ����������-��������. ��� ���� �� ���� ����������� � �����, ������ �������� ���� � �� �� �����
    // ��� �� ���� �������� "����������� �����" �.�. ����-���������� ��� ��-����������

    [SerializeField] private bool _isPlayer;
    public bool isImmortal;
    private Animator _animator;

    public bool _isDead;
    private void Awake()
    {
       
        if (Attack == null) gameObject.TryGetComponent<AttackType>(out Attack);
        if (CompareTag("Player"))
        {
            _isPlayer = CompareTag("Player");
            return;
        }
        _animator = GetComponent<Animator>();
    }

    private void Death()
    {
        if (!_isPlayer) _animator.Play("Death");
        _isDead = true;
        if (!_isPlayer)
        {
            Destroy(gameObject, _animator.GetCurrentAnimatorClipInfo(0).Length - 0.33f);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }
    public void ApplyDMG(AttackType What)
    {
        if (isImmortal) return;
        if (_isPlayer)
        {
            //GetComponent<TakeDamageCanvas>().isPlayerTakeDamage();
        }
        else _animator.Play("TakeDamage");
        HitPoints -= What.Damage;
        if (HitPoints <= 0) Death();
    }
    private void GiveDMG(Unit Target)
    {
        if (_isDead) return;
        if (Target == null) return;
        if (!_isPlayer) _animator.Play("Attack");
        Target.ApplyDMG(Attack);
    }

    private void GiveDMG(Unit[] Target)
    {
        // Debug.Log(Target);
        foreach (Unit current in Target)
        {
            if (current != null)
            {
                // Debug.Log("GiveDMG" + current.gameObject.name);
                GiveDMG(current);
            }
        }
    }

    public float ReturnHP()
    {
        return HitPoints;
    }
    public void Command_Attack()
    {
        GiveDMG(Attack.ReturnTargets());
    }
}
