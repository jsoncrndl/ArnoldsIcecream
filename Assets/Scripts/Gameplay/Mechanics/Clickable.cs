using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using WerewolfHunt.Manager;
using WerewolfHunt.Player;

namespace WerewolfHunt.Mechanics
{
    public class Clickable : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent hoverEnter;
        public UnityEvent hoverExit;
        public UnityEvent onClick;

        [SerializeField] private float maxPlayerDistance = 2.5f;

        private bool hovering;
        private bool canClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (canClick && PlayerController.Instance.canInteract)
            {
                onClick?.Invoke();
            }
        }

        public void OnPointerEnter(PointerEventData eventData)
        {

        }

        public void OnPointerExit(PointerEventData eventData)
        {

        }

        void Update()
        {
            if (hovering)
            {
                if ((PlayerController.Instance.transform.position - transform.position).sqrMagnitude > maxPlayerDistance * maxPlayerDistance)
                {
                    if (canClick)
                    {
                        canClick = false;
                        hoverExit?.Invoke();
                        PlayerController.Instance.AllowAttacks(true);
                        CursorManager.Instance.setCrosshair();
                    }
                }
                else
                {
                    if (!canClick)
                    {
                        canClick = true;
                        hoverEnter?.Invoke();
                        PlayerController.Instance.AllowAttacks(false);
                        CursorManager.Instance.setCursor();
                    }
                }
            }
            else if (canClick)
            {
                canClick = false;
                hoverExit?.Invoke();
                PlayerController.Instance.AllowAttacks(true);
                CursorManager.Instance.setCrosshair();
            }
        }

        private void OnDisable()
        {
            PlayerController.Instance.AllowAttacks(true);
            CursorManager.Instance.setCrosshair();
        }
    }
}
