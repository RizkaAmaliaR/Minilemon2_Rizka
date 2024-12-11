using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MissionDisplay : MonoBehaviour
{
    public MissionGiver missionGiver { private get; set; }

    [SerializeField]
    TextMeshProUGUI missionDescription;

    void Start()
    {
        Popup.Open();

        Debug.Log(missionGiver);

        foreach (Mission mission in missionGiver.missionsToGive)
        {
            missionDescription.text += mission.GetDescription() + "\n";
        }
    }

    public void AcceptMission()
    {
        Popup.Close();

        missionGiver.GiveMission();
        Destroy(gameObject);
    }

    public void DeclineMission()
    {
        Popup.Close();

        Destroy(gameObject);
    }
}
