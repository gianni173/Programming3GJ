using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterTargetDetectors : MonoBehaviour
{
    [SerializeField] private TriggerDetector visionRangeCollider = null;
    [SerializeField] private TriggerDetector shootingRangeCollider = null;
    [SerializeField] private Character character = null;

    private List<Character> inVisionRangeCharacters = new List<Character>();
    private List<Character> inShootingRangeCharacters = new List<Character>();

    private void Start()
    {
        visionRangeCollider.OnTriggerEntered += EnteredInVisionRange;
        shootingRangeCollider.OnTriggerEntered += EnteredInShootingRange;
        visionRangeCollider.OnTriggerExited += ExitFromVisionRange;
        shootingRangeCollider.OnTriggerExited += ExitFromShootingRange;
    }

    [Button("Set Detectors Active")]
    public void SetDetectorsActive(bool value)
    {
        visionRangeCollider.gameObject.SetActive(value);
        shootingRangeCollider.gameObject.SetActive(value);
    }

    [Button("Set Detectors Ranges")]
    public void SetDetectorsRanges(float visionRange, float shootingRange)
    {
        visionRangeCollider.GetComponent<SphereCollider>().radius = visionRange;
        shootingRangeCollider.GetComponent<SphereCollider>().radius = shootingRange;
    }

    private void EnteredInVisionRange(Collider collidedObject)
    {
        var collidedCharacter = collidedObject.gameObject.GetComponent<Character>();
        if (character.Faction && collidedCharacter.Faction && 
            collidedCharacter.Faction.IsEnemy(character.Faction))
        {
            inVisionRangeCharacters.Add(collidedCharacter);
            UpdateClosestCharacter();
        }
    }

    private void EnteredInShootingRange(Collider collidedObject)
    {
        var collidedCharacter = collidedObject.gameObject.GetComponent<Character>();
        if (character.Faction && collidedCharacter.Faction && 
            collidedCharacter.Faction.IsEnemy(character.Faction))
        {
            inShootingRangeCharacters.Add(collidedCharacter);
            UpdateClosestCharacter();
        }
    }

    private void ExitFromVisionRange(Collider collidedObject)
    {
        var collidedCharacter = collidedObject.gameObject.GetComponent<Character>();
        if (character.Faction && collidedCharacter.Faction && 
            collidedCharacter.Faction.IsEnemy(character.Faction))
        {
            if(inVisionRangeCharacters.Contains(collidedCharacter))
            {
                inVisionRangeCharacters.Remove(collidedCharacter);
                UpdateClosestCharacter();
            }
        }
    }

    private void ExitFromShootingRange(Collider collidedObject)
    {
        var collidedCharacter = collidedObject.gameObject.GetComponent<Character>();
        if (character.Faction && collidedCharacter.Faction && 
            collidedCharacter.Faction.IsEnemy(character.Faction))
        {
            if (inShootingRangeCharacters.Contains(collidedCharacter))
            {
                inShootingRangeCharacters.Remove(collidedCharacter);
                UpdateClosestCharacter();
            }
        }
    }

    private void UpdateClosestCharacter()
    {
        Character closestChar = null;
        float closestCharDistance = 10000f;
        if (inShootingRangeCharacters.Count > 0)
        {
            closestChar = inShootingRangeCharacters[0];
            closestCharDistance = Vector3.Distance(closestChar.transform.position, transform.position);
            foreach (Character character in inShootingRangeCharacters)
            {
                var distanceFromThisChar = Vector3.Distance(character.transform.position, transform.position);
                if (distanceFromThisChar < closestCharDistance)
                {
                    closestChar = character;
                    closestCharDistance = distanceFromThisChar;
                }
            }
        }
        else if (inVisionRangeCharacters.Count > 0)
        {
            closestChar = inVisionRangeCharacters[0];
            closestCharDistance = Vector3.Distance(closestChar.transform.position, transform.position);
            foreach (Character character in inVisionRangeCharacters)
            {
                var distanceFromThisChar = Vector3.Distance(character.transform.position, transform.position);
                if (distanceFromThisChar < closestCharDistance)
                {
                    closestChar = character;
                    closestCharDistance = distanceFromThisChar;
                }
            }
        }
        character.Input.closestTarget = closestChar;
    }
}
