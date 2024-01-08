using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using WerewolfHunt.Input;
using System;
using WerewolfHunt.Inventory.Items;
using WerewolfHunt.Manager;
using WerewolfHunt.Animation;
using System.Collections;
using WerewolfHunt.Mechanics;
using WerewolfHunt.Inventory;

namespace WerewolfHunt.Player
{
    [SelectionBase]
    public class PlayerController : MonoBehaviour, IAnimationEventListener
    {

        [SerializeField] private AudioClip[] footsteps;
        [SerializeField] Animator animator;
        public SpriteRenderer sprite => spriteRenderer;
        [SerializeField] GameObject flashlight;
        [SerializeField] private float damageHitStop;

        private Item heldItem;

        public Animator anim => animator;

        public Rigidbody2D rb { get; private set; }

        public static PlayerController Instance { get; private set; }

        public CircleCollider2D circleCollider { get; private set; }
        private AudioSource audioSource;
        [SerializeField] private SpriteRenderer spriteRenderer;

        public Checkpoint checkpoint { get; set; }

        public enum State { MOVE, DIALOGUE, DASH, KNOCKBACK, STUCK, DEATH, INVENTORY, MENU, ANY, ATTACK_GUN, ATTACK_SHOVEL, ATTACK_WHIP, ATTACK_SALT, ATTACK_IDLE }
        private State nextState;
        private float attackCooldown;

        public bool canInteract;

        public bool hasNextState { get; private set; }
        public Vector2 forward { get; private set; }

        private PlayerControls controls;

        public event Action<int, int> healthChanged;
        public event Action<Stats> ammoChanged;
        [SerializeField] Inventory.Inventory playerInventory;
        public Inventory.Inventory inventory => playerInventory;

        [SerializeField] private int startHealth;
        [SerializeField] private int startBullets;
        [SerializeField] private int startSalt;
        [SerializeField] private float armHeight = 1;
        public float ArmHeight => armHeight;

        public event Action<bool> onOpenMap;

        public float dashCooldown { get; set; }

        private int maxHealth;
        public bool stuck { get; private set; }

        public struct Stats
        {
            public int health;
            public int bullets;
            public int salt;
            public int experience;
        }

        public Stats stats;

        [SerializeField] AudioClip hurtSound;
        public int attackAllowed { get; private set; } = 0;

        private void Awake()
        {
            Instance = this;

            rb = GetComponent<Rigidbody2D>();
            circleCollider = GetComponent<CircleCollider2D>();
            audioSource = GetComponent<AudioSource>();
            controls = new PlayerControls();

            LoadStates();
            controls.Default.mouseMove.performed += ReadMousePosition;
            controls.Default.Enable();
            CursorManager.Instance.setCrosshair();
        }

        private void Start()
        {
            LoadInventory();
            ResetStats();

            checkpoint = new Checkpoint() { position = transform.position, scene = gameObject.scene.name };

        }

        private void LoadStates()
        {
            
        }

        private void Update()
        {            
            if (attackCooldown > 0)
            {
                attackCooldown -= Time.deltaTime;
            }

            if (dashCooldown > 0)
            {
                dashCooldown -= Time.deltaTime;
            }

            if (hasNextState)
            {
                ChangeState();
            }
        }

        private void FixedUpdate()
        {
        }

        private void OnDestroy()
        {
            controls.Default.mouseMove.performed -= ReadMousePosition;
        }

        public void SetNextState(State state, bool forceOverride = false)
        {
            if (hasNextState && !forceOverride) return;
            nextState = state;
            hasNextState = true;
        }

        private void ChangeState()
        {

            hasNextState = false;

            anim.SetFloat("Speed", rb.velocity.magnitude);
        }

        public void SetMapOpen(bool open)
        {
            onOpenMap?.Invoke(open);
        }
        public void GainEXP(int amount)
        {
            stats.experience += amount;
            ammoChanged?.Invoke(stats);
        }

        public void SetAttackCooldown(float cooldown)
        {
            attackCooldown = cooldown;
        }

        public void PlaySFX(AudioClip clip, float volume = 1)
        {
            audioSource.PlayOneShot(clip, volume);
        }

        public string GetDebugInfo()
        {
            return "";
        }

        public string GetAttackDebugInfo()
        {
            return "";
        }

        private void ReadMousePosition(InputAction.CallbackContext ctx)
        {

        }

        public void Trap()
        {
            SetNextState(State.STUCK);
            stuck = true;
        }

        public void Untrap()
        {
            stuck = false;
        }

        public void ReceiveEvent(Animation.AnimationEvent.AnimationEventType eventType)
        {
            if (eventType == Animation.AnimationEvent.AnimationEventType.FOOTSTEP)
            {
                Footstep();
            }
        }

        public void UseSalt()
        {
            stats.salt--;
            ammoChanged?.Invoke(stats);
        }

        public void UseBullet()
        {
            stats.bullets--;
            ammoChanged?.Invoke(stats);
        }

        public void AllowAttacks(bool allowed)
        {
            attackAllowed = allowed ? 0 : 1;
        }       

        private void LoadInventory()
        {
            if (!PersistenceManager.Instance.hasStoredPlayer) return;
    
            playerInventory = PersistenceManager.Instance.inventory;
            stats = PersistenceManager.Instance.stats;

        }

        public void ResetStats(bool resetEXP = false)
        {

        }
        
        public void RefreshStats()
        {
        }

        private void Footstep()
        {
            AudioClip clip = footsteps[UnityEngine.Random.Range(0, footsteps.Length)];
            PlaySFX(clip, 0.1f);
        }
    }
}
