using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour, IBuilding
{

    [SerializeField] int maxHP = 100;
    [SerializeField] float respawnTime = 10;
    [SerializeField] MeshRenderer meshRenderer;
    public HealthPoints HealthPoints = new HealthPoints(100);
    public bool _isAlive { get; set; }
    public Transform _transform { get; set; }

    private void OnEnable()
    {
        HealthPoints.SetMaxHP(maxHP);
        HealthPoints.AddOnDeath(Dies);
        _isAlive = true;
        _transform = transform;
    }

    [ContextMenu("Kill test")]
    public void Test()
    {
        HealthPoints.Kill();
    }
    public void Dies()
    {
        HealthPoints.RemoveOnDeath(Dies);
        _isAlive = false;
        meshRenderer.enabled = false;
        StartCoroutine(Respawn(respawnTime));
    }
    IEnumerator Respawn(float timer)
    {
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        meshRenderer.enabled=true;
        OnEnable();
    }
}
