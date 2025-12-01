using System.Collections.Generic;
using Effect;
using EventSystem;
using Item;
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
        for (int i = 0; i < data.weaponCount; i++)
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
        item.Init();
        items.Add(item);
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

        //todo: Add dedicated handler for this
        Vector3 position = gameObject.transform.position;
        position.z = cam.transform.position.z;
        cam.transform.position = position;


        if (impulse.sqrMagnitude < 0.01f)
            impulse = Vector2.zero;
        if (data != null)
        {
            rigidBody.linearVelocity = (inputMovement * data.playerData.moveSpeed) + impulse;
            impulse *= (1 - data.playerData.frictionCoeff);
        }
        //todo: consider concurrent modification exceptions
        if (SceneManager.GetActiveScene().name == "Temporary")
        {
            foreach (GameItem item in items)
            {
                item.ItemTick();
            }
        }
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
            die();
            Main.Instance.LoadGameover();
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

