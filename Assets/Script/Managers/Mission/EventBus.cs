using System.Collections.Generic;
using UnityEngine.Events;

public static class EventBus
{
    // Events
    public static UnityEvent<MissionGiver> MissionsGiven = new();
    public static UnityEvent MissionStarted = new();
    public static UnityEvent<Mission> MissionCompleted = new();
    public static UnityEvent<Mission> MissionFailed = new();

    public static UnityEvent<Item> ItemCollected = new();

    // Functions
    public static void InvokeMissionsGiven(MissionGiver giver) => MissionsGiven?.Invoke(giver);
    public static void InvokeMissionStarted() => MissionStarted?.Invoke();
    public static void InvokeMissionCompleted(Mission mission) => MissionCompleted?.Invoke(mission);
    public static void InvokeMissionFailed(Mission mission) => MissionFailed?.Invoke(mission);

    public static void InvokeItemCollected(Item item) => ItemCollected?.Invoke(item);
}