using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTp : MonoBehaviour
{
    [SerializeField] private List<KeyToPosition> teleportsInfos = new List<KeyToPosition>();

    private Character linkedCharacter = null;

    private void Start()
    {
        var gameManager = GameManager.Instance;
        if(gameManager)
        {
            gameManager.OnMainPlayerSpawned += LinkCharacter;
        }
    }

    private void Update()
    {
        if (linkedCharacter && teleportsInfos != null)
        {
            foreach (KeyToPosition tpInfo in teleportsInfos)
            {
                if (Input.GetKeyDown(tpInfo.keyCode))
                {
                    linkedCharacter.transform.position = tpInfo.worldTransform.position;
                    linkedCharacter.transform.rotation = tpInfo.worldTransform.rotation;
                }
            }
        }
    }

    private void LinkCharacter(Character character)
    {
        linkedCharacter = character;
    }

    [System.Serializable]
    public struct KeyToPosition
    {
        public Transform worldTransform;
        public KeyCode keyCode;
    }
}
