using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using WerewolfHunt.Player;
using static UnityEngine.Rendering.DebugUI;

public class Spear : MonoBehaviour
{
    [SerializeField] private float maxRadius;
    [SerializeField] private float maxTime;
    private float time;
    private Rigidbody2D rb;
    private float speed = 15;
    [SerializeField] GameObject spear;
    private Transform parent;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        parent = transform.parent;
    }

    private void OnEnable()
    {
        spear.transform.localScale = transform.parent.localScale;
        spear.transform.rotation = Quaternion.Euler(transform.parent.localScale.x == 1 ? 0 : 180, 0, spear.transform.rotation.z);
        time = 0;
        transform.parent = null;

        Vector3 temp = (PlayerController.Instance.transform.position - spear.transform.position).normalized;
        spear.transform.Rotate(0, 0, Mathf.Atan2(temp.y, temp.x) * 180 / Mathf.PI);
        rb.velocity =  temp * speed;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > maxTime)
        {
            transform.parent = parent;
            spear.transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            transform.localPosition = Vector3.zero;
            gameObject.SetActive(false);
        }
        else if (time > (maxTime * 3) / 4) 
        {
            rb.velocity = Vector3.zero;
        }
    }
}
