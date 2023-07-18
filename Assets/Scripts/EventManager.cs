using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public static class EventManager
{
    public static event Action TwoHandedWeaponSpawned;
    public static event Action OneHandedWeaponSpawned;
    public static event Action CrossBowSpawned;
    public static event Action<int> SpawnButtonPressed;
    public static event Action<int> EnemyHit;
    
    public static void OnTwoHandedWeaponSpawned()
    {
        TwoHandedWeaponSpawned?.Invoke();
    }
    public static void OnOneHandedWeaponSpawned()
    {
        OneHandedWeaponSpawned?.Invoke();
    }
    public static void OnCrossBowSpawned()
    {
        CrossBowSpawned?.Invoke();
    }
    public static void OnSpawnButtonPressed(int id)
    {
        SpawnButtonPressed?.Invoke(id);
    }
    public static void OnEnemyHit(int damage)
    {
        EnemyHit?.Invoke(damage);
    }
}
