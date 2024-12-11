using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionPanel : MonoBehaviour
{
    [field: SerializeField, Tooltip("Background")]
    public Image image { get; private set; }

    [field: NonSerialized]
    public Mission mission { private get; set; }

    [SerializeField] private TextMeshProUGUI textDescription;
    [SerializeField] private TextMeshProUGUI textProgress;
    [SerializeField] private TextMeshProUGUI textTimer;

    void Start()
    {
        textDescription.text = mission.GetDescription();

        mission.MissionCompleted.AddListener(OnMissionCompleted);
        mission.MissionFailed.AddListener(OnMissionFailed);
    }

    void Update()
    {
        if (mission.isRunning)
        {
            if (mission is CollectItemMission current)
                textProgress.text = $"{current.progress}/{current.target}";
            if (mission.timeLimit > 0)
                textTimer.text = $"{mission.timeLimit - mission.timeElapsed:F1}s";
        }
    }

    void OnMissionCompleted()
    {
        textProgress.text = "BERHASIL";
        
        image.color = new Color(0, 1, 0, 1f / 3f);
        Destroy(gameObject, 3f);
    }

    void OnMissionFailed()
    {
        textProgress.text = "GAGAL";

        image.color = new Color(1, 0, 0, 1f / 3f);
        Destroy(gameObject, 3f);
    }
}
