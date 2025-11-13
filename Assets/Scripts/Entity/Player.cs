using System.Collections.Generic;
using EventSystem;
using Item;
using JetBrains.Annotations;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 inputMovement;
    private Vector2 impulse;

    private Rigidbody2D rigidBody;

    [SerializeField] private float health = 1;
    [SerializeField] private float maxHealth = 1;
    [SerializeField] private float moveSpeed = 5;
    [SerializeField] private float frictionCoeff = 0.02f; // this is for knockback calculation 


    private List<GameItem> items;
    
    public void Start()
    {
        items = new List<GameItem>();
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
        impulse = Vector2.zero;
        inputMovement = Vector2.zero;
        Subscribe();

        GameObject itemPulse = Resources.Load<GameObject>("Prefab/ItemPulseAttack");
        GameObject itemInstance = Instantiate(itemPulse);
        GameItem item = itemInstance.GetComponent<ItemPulse>();
        AddItem(item);

    }


    public void AddItem(GameItem item)
    {
        item.gameObject.transform.parent = gameObject.transform;
        item.transform.localPosition = Vector3.zero;
        item.Init();
        items.Add(item);
    }



    public void FixedUpdate()
    {
        if (impulse.sqrMagnitude < 0.01f)
            impulse = Vector2.zero;

        rigidBody.linearVelocity = (inputMovement * moveSpeed) + impulse;
        impulse *= (1 - frictionCoeff);



        foreach (GameItem item in items)
        {
            item.ItemTick();
        }

    }

    //if you need to apply knockback to the player
    public void push(Vector2 impulse)
    {
        this.impulse += impulse;
    }


    public void die()
    {
        // do stuff related to death here
        Unsubscribe();
    }

    //return whether the player was successfully damaged or not
    //skeleton code, will require change in the future
    public bool damage(float damage)
    {
        if (damage < 0)
            return false;

        float next = this.health - damage;
        if (next <= 0)
        {
            die();
        }

        this.health = next;
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
}

