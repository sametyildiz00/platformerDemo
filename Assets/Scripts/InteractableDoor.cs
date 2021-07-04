using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractableDoor : MonoBehaviour, IInteractable
{
    public string sceneName;
    public virtual void Interact()
    {
        SceneManager.LoadScene(sceneName);
    }
}
