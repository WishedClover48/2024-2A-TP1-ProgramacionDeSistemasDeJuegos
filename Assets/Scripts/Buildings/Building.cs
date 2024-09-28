using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{

    [SerializeField] int maxHP = 100;
    [SerializeField] float respawnTime = 10;
    [SerializeField] MeshRenderer meshRenderer;
    private HealthPoints HealthPoints = new HealthPoints(100);
    private bool isAlive = true;


    private void OnEnable()
    {
        HealthPoints.SetMaxHP(maxHP);
        HealthPoints.AddOnDeath(Dies);
        isAlive = true;
    }

    [ContextMenu("Kill test")]
    public void Test()
    {
        HealthPoints.Kill();
    }
    public void Dies()
    {
        StartCoroutine(Respawn(respawnTime));
        HealthPoints.RemoveOnDeath(Dies);
        isAlive = false;
        meshRenderer.enabled = false;
    }
    IEnumerator Respawn(float timer)
    {
        while (timer > 0f)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        Debug.Log("Ahora voy a habilitarme");
        meshRenderer.enabled=true;
        OnEnable();
    }
}
