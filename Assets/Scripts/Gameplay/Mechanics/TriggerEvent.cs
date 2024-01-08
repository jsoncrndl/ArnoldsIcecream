using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using WerewolfHunt.Player;

namespace WerewolfHunt.Mechanics
{

    public class TriggerEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent triggerEnter;
        [SerializeField] private UnityEvent triggerStay;
        [SerializeField] private UnityEvent triggerExit;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject == PlayerController.Instance.gameObject)
            {
                triggerEnter.Invoke();
            }
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (collision.gameObject == PlayerController.Instance.gameObject)
            {
                triggerStay.Invoke();
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (PlayerController.Instance == null) return;

            if (collision.gameObject == PlayerController.Instance.gameObject)
            {
                triggerExit.Invoke();
            }
        }
    }

}
