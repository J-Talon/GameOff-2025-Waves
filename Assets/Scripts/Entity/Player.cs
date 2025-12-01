using System;
using System.Collections.Generic;
using Effect;
using Effect.Behaviour;
using EventSystem;
using Item;
using UnityEditor.Experimental;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IManager, IDataUser
{
    public static Player Instance;
    private Vector2 inputMovement;
    private Vector2 impulse;

    private Rigidbody2D rigidBody;

    [SerializeField] GameData data;

    private List<GameItem> items;
    private GameObject itemPulse;
    private Camera cam;

    private Animator anim;
    private GameObject hairRenderer;
    private Animator hairAnim;
    
    private int dir = 1;

    public void Start()
    {
        Instance = this;
        Main.Instance.AddManager(this);
        items = new List<GameItem>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        impulse = Vector2.zero;
        inputMovement = Vector2.zero;
        Subscribe();
        cam = Camera.main;

        hairRenderer = transform.GetChild(0).gameObject;
        hairAnim = hairRenderer.GetComponent<Animator>();
        
        if (cam == null)
            throw new NullReferenceException("Cam not found");

        anim = gameObject.GetComponent<Animator>();
        

        itemPulse = Resources.Load<GameObject>("Prefab/ItemPulseAttack");
        ParticleFactory.LoadResources();
    }
    public void SetData(GameData data)
    {
        this.data = data;
        Debug.Log($"{this} has been given GameData");
        InitializeWeapons();
    }
    public void InitializeWeapons()
    {
        for (int i = 0; i < data.weapons.Count; i++)
        {
            GameObject itemInstance = Instantiate(itemPulse);
            GameItem item = itemInstance.GetComponent<ItemPulse>();
            item.data = this.data.weapons[i];
            item.playerData = data.playerData;
            AddItem(item);
        }
    }
    public void AddItem(GameItem item)
    {
        item.gameObject.transform.parent = gameObject.transform;
        item.transform.localPosition = Vector3.zero;
        item.Init(this);
        items.Add(item);
    }

    public Animator GetHairRendererer()
    {
        return hairAnim;
    }

    public void GainEnergy(float amount)
    {
        data.playerData.currentEnergy += amount;
    }
    public void GainHealth(float amount)
    {
        data.playerData.currentHealth += amount;
    }
    public void FixedUpdate()
    {
        Vector3 position = gameObject.transform.position;
        position.z = cam.transform.position.z;
        cam.transform.position = position;

        if (impulse.sqrMagnitude < 0.01f)
            impulse = Vector2.zero;

<<<<<<< HEAD

        Vector2 linVel = Vector2.zero;
=======
>>>>>>> development
        if (data != null)
        {
            linVel = (inputMovement * data.playerData.moveSpeed) + impulse;
            rigidBody.linearVelocity = linVel;
            impulse *= (1 - data.playerData.frictionCoeff);
            GainEnergy(1);
        }
<<<<<<< HEAD
        

        float coarseMoveMagSqd = (inputMovement.x * inputMovement.x) + (inputMovement.y * inputMovement.y);
        

        /////testing code
        // float moveSpeed = 5;
        // float frictionCoeff = 0.25f;
        // rigidBody.linearVelocity = (inputMovement * moveSpeed) + impulse;
        // impulse *= (1 - frictionCoeff);
        // ////////
        
        
        
        
        //todo: consider concurrent modification exceptions
=======

>>>>>>> development
        if (SceneManager.GetActiveScene().name == "Temporary")
        {
            foreach (GameItem item in items)
            {
                item.ItemTick();
            }
        }

<<<<<<< HEAD
        GainEnergy(1);
        
        //idle animations technically aren't in here yet 
        //attack anims are in items
        if (coarseMoveMagSqd > 0)
        {
            anim.SetBool(EntityAnimatorState.WALK.value, true);
            hairAnim.SetBool(EntityAnimatorState.WALK.value, true);
        }
        else
        {
            anim.SetBool(EntityAnimatorState.WALK.value, false);
            hairAnim.SetBool(EntityAnimatorState.WALK.value, false);
        }
        
        
        

        int facingDir = inputMovement.x > 0 ? 1 : (inputMovement.x < 0 ? -1 : 0);
        if (facingDir == 0)
            return;


        Vector2 scale = gameObject.transform.localScale;
        if (dir > 0 && facingDir < 0)
        {
            dir = -1;
            scale.x *= -1;
        }
        else if (dir < 0 && facingDir > 0)
        {
            dir = 1;
            scale.x *= -1;
        }

        transform.localScale = scale;


=======
        // ----- BOUNDARY CLAMP -----
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, data.bottomLeft.x, data.topRight.x);
        pos.y = Mathf.Clamp(pos.y, data.bottomLeft.y, data.topRight.y);
        transform.position = pos;
>>>>>>> development
    }

    //if you need to apply knockback to the player
    public void push(Vector2 impulse)
    {
        this.impulse += impulse;
    }


    public void die()
    {
        Destroy(gameObject);
        // do stuff related to death here
        Unsubscribe();
    }

    //return whether the player was successfully damaged or not
    //skeleton code, will require change in the future
    public bool damage(float damage)
    {
        if (damage < 0)
            return false;

        float next = this.data.playerData.currentHealth - damage;
        if (next <= 0)
        {
            StartCoroutine(Main.Instance.LoadGameover());
            // die();

        }

        this.data.playerData.currentHealth = next;
        HealthManager.Instance.InvokeHealthEvent(-damage);
        return true;
    }



    public void onKeyboardMoveInput(Vector2 movement)
    {
        this.inputMovement = movement;
    }


    public void Unsubscribe()
    {
        EventManager.keyboardMoveActionEvent.Unsubscribe(onKeyboardMoveInput);
    }

    public void Subscribe()
    {
        EventManager.keyboardMoveActionEvent.Subscribe(onKeyboardMoveInput);
    }

    public void Register(IWorker worker)
    {
    }
    public void Deregister(IWorker worker)
    {
    }
    public void OnDestroy()
    {
        Main.Instance.RemoveManager(this);
    }
}

