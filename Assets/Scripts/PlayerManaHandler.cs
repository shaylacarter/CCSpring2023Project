using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManaHandler : MonoBehaviour
{

    public int mana;
    public int manaCap;

    public ManaBarShrink manaBarShrink;
    public PlayerDamageHandler playerDamageHandler;

    // Start is called before the first frame update
    void Start()
    {
        mana = 0;
        manaCap = 100;
        playerDamageHandler = GetComponent<PlayerDamageHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Q) && mana == manaCap) {
            Debug.Log("Combo AOE!");
            RemoveMana(manaCap);
        }
        if (Input.GetKey(KeyCode.W) && mana >= manaCap/2 && playerDamageHandler.health < 100) {
            Debug.Log("Player heal!");
            playerDamageHandler.Heal(20);
            RemoveMana(manaCap/2);
        }
    }

    public void AddMana(int amount) {
        mana += amount;
        if (mana > manaCap) {
            mana = manaCap;
        }
        manaBarShrink.Heal(amount);
    }

    public void RemoveMana(int amount) {
        mana -= amount;
        if (mana < 0) {
            mana = 0;
        }
        manaBarShrink.Damage(amount);
    }
}
