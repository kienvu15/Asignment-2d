using System.Collections;
using System.Collections.Generic;
using UnityEngine;



    [CreateAssetMenu(fileName = "NewCharacterData", menuName = "Character Data", order = 1)]
    public class CharacterData : ScriptableObject
    {
        public float moveSpeed = 5f;
        public float dashForce = 10f;
        public float dashSpeed = 8f;
    }

    



