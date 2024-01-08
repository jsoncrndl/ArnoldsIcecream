using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using WerewolfHunt.Player;

public class DebugUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stateDebug; 
    [SerializeField] private TextMeshProUGUI attackDebug;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        stateDebug.text = PlayerController.Instance.GetDebugInfo();        stateDebug.text = PlayerController.Instance.GetDebugInfo();
        attackDebug.text = PlayerController.Instance.GetAttackDebugInfo();

    }
}
