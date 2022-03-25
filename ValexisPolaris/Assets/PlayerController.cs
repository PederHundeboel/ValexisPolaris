using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using CMF;

public class PlayerController : NetworkBehaviour
{

    protected Mover mover;
    protected AdvancedWalkerController advancedWalkerController;
    protected CharacterKeyboardInput characterKeyboardInput;
    protected GameObject cameraRoot;

    private void Awake()
    {
        mover = transform.GetComponent<Mover>();
        advancedWalkerController = GetComponent<AdvancedWalkerController>();
        characterKeyboardInput = GetComponent<CharacterKeyboardInput>();
        var cRoot = transform.Find("CameraRoot");
        if (cRoot != null)
        {
            cameraRoot = cRoot.gameObject;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!IsLocalPlayer)
        {
            mover.enabled = false;
            advancedWalkerController.enabled = false;
            characterKeyboardInput.enabled = false;
            cameraRoot.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
