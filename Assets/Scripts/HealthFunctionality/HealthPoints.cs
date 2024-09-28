using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthSystem;
using System;

//Wrapper of Health
public class HealthPoints 
{
    public int _maxHP { get; private set; }
    private Health _health;
    public HealthPoints(int maxHP) 
    {
        SetMaxHP(maxHP);
    }

    public void SetMaxHP(int maxHP) 
    {
        _maxHP = maxHP;
        _health = new Health(_maxHP);
    }
    public void AddOnDeath(Action myMethod)
    {
        _health.OnDeath += myMethod;
    }

    public void RemoveOnDeath(Action myMethod)
    {
        _health.OnDeath -= myMethod;
    }

    public void TakeDamage(int damage)
    {
        _health.TakeDamage(damage);
    }
    public void Kill() 
    {
        _health.Kill();
    }
    public void Heal(int healPoints) 
    {  
        _health.Heal(healPoints);
    }

    public void FullyHeal() 
    { 
        _health.FullyHeal(); 
    }


}
