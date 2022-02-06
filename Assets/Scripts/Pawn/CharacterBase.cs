using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    public float cHP; //current health
    public float mHP; //maximum health

    [SerializeField] private int cSpeed; //current movement speed left
    [SerializeField] public int mSpeed; //maximum movement speed left

    [SerializeField] private bool action1 = true; //Action 1
    [SerializeField] private bool action2 = true; //Action 2
    [SerializeField] private bool scarAction = true; //P.otentia Action
    [SerializeField] private bool[] chargeActions; //Charge Actions
}
