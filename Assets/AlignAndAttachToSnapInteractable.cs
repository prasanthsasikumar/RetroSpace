using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Oculus.Interaction;

public class AlignAndAttachToSnapInteractable : MonoBehaviour
{
    private SnapInteractor snapInteractor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AlignAndAttachToSnapInteractableMethod()
    {
        snapInteractor = GetComponent<SnapInteractor>();
        this.transform.position = snapInteractor.Interactable.gameObject.transform.position;
    }
}
