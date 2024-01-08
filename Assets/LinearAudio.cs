using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WerewolfHunt.Player;

public class LinearAudio : MonoBehaviour
{
    [SerializeField] private Transform end1;
    [SerializeField] private Transform end2;

    private Vector2 line;
    private PlayerController player;


    private void Start()
    {
        line = end1.position - end2.position;
        player = PlayerController.Instance;
    }

    private void Update()
    {
        Vector2 closest = Vector3.Project(player.transform.position - end1.position, line);
        transform.position = (Vector3)closest + end1.position;
    }
}
