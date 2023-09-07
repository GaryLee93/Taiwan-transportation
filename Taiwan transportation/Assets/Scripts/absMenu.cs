using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class absMenu : MonoBehaviour
{
    [System.Serializable] 
    public class Button
    {
        public string name;
        public GameObject button;
        public Sprite normalImg;
        public Sprite selectedImg;
    }
    [SerializeField] protected List<Button> buttons;
    [SerializeField] protected List<GameObject> buttonImgs;
    [SerializeField] protected AudioSource choseSound; 
    [SerializeField] protected AudioSource pressSound;
    
    protected int nowSelected = 0;
    protected bool canChose = false;

    protected abstract void chose();
}
