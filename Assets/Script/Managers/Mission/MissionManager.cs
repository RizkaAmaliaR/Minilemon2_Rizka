using System.Collections.Generic;
using UnityEngine;

public class MissionManager : MonoBehaviour
{
    public static MissionManager instance { get; private set; }

    public List<Mission> missions { get; private set; } = new();

    GameObject missionPrefab;

    [SerializeField]
    private GameObject missionDisplay;

    [SerializeField]
    private GameObject missionPanel;

    void Awake()
    {
        if (instance != null && instance != this) Destroy(gameObject);
        else
        {
            instance = this;

            EventBus.MissionCompleted.AddListener(RemoveMission);
            EventBus.MissionFailed.AddListener(RemoveMission);
            EventBus.MissionStarted.AddListener(StartMission);
            EventBus.MissionsGiven.AddListener(ShowMissions);
        }
    }

    void Update()
    {
        foreach (Mission mission in missions)
        {
            mission?.Update();
        }
    }

    void RemoveMission(Mission mission)
    {
        missions.Remove(mission);
    }

    void StartMission()
    {
        foreach (Mission mission in missions)
        {
            if (!mission.isRunning)
            {
                GameObject panel = Instantiate(missionPanel, GameObject.Find("Missions").transform);
                panel.GetComponent<MissionPanel>().mission = mission;

                mission.Start();
            }
        }
    }

    void ShowMissions(MissionGiver giver)
    {
        GameObject display = Instantiate(missionDisplay, GameObject.FindGameObjectWithTag("UI").transform);
        display.GetComponent<MissionDisplay>().missionGiver = giver;
    }

    void OnDestroy()
    {
        foreach (Mission mission in missions)
        {
            mission.End();
        }
    }

    [ContextMenu("Test Mission")]
    void GetMissions() => Debug.Log(missions.Count);
}
