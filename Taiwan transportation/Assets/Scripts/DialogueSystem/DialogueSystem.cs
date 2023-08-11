using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [System.Serializable]
    class NamePic{
        public string picName;
        public Sprite pic;
    }

    [SerializeField] Canvas dialogueCanvas;
    [SerializeField] Text nameBox;
    [SerializeField] Text textBox;
    [SerializeField] GameObject dialogueBox;
    [SerializeField] DialogueCharacter[] characters;
    
    [SerializeField] float textSpeed;
    [SerializeField] float inputDelayTime;

    List<string> dialogueTexts;
    Queue<(int, string)> enterQueue, leaveQueue, speakQueue, nameQueue;
    Dictionary<string, DialogueCharacter> charaDict;

    int dialogueIndex, dialogueCount;
    bool isDisplaying, isRunning, canControl;

    void Awake(){
        dialogueCanvas.gameObject.SetActive(false);
        dialogueIndex = 0;
        dialogueCount = 0;
        dialogueTexts = new List<string>();
        enterQueue = new Queue<(int, string)>();
        leaveQueue = new Queue<(int, string)>();
        speakQueue = new Queue<(int, string)>();
        nameQueue = new Queue<(int, string)>();
        charaDict = new Dictionary<string, DialogueCharacter>();
        isRunning = false;
        canControl = false;

        foreach(var chara in characters){
            charaDict.Add(chara.name, chara);
        }
    }
    public void CharacterEnter(string name){
        if(!charaDict.ContainsKey(name)){
            Debug.LogWarning(name+ "is not in characters!!!");
        }
        enterQueue.Enqueue((dialogueCount,name));
    }
    public void CharacterLeave(string name){
        if(!charaDict.ContainsKey(name)){
            Debug.LogWarning(name+ "is not in characters!!!");
        }
        leaveQueue.Enqueue((dialogueCount,name));
    }
    public void SetSpeak(string name){
        if(!charaDict.ContainsKey(name)){
            Debug.LogWarning(name+ "is not in characters!!!");
        }
        speakQueue.Enqueue((dialogueCount, name));
    }
    public void SetName(string name){
        nameQueue.Enqueue((dialogueCount, name));
    }
    public void AddText(string text){
        dialogueTexts.Add(text);
        dialogueCount ++;
    }
    public void ActivateDialogue(){
        if(isRunning){
            Debug.LogWarning("Dialogue is running!");
        }
        else{
            dialogueCanvas.gameObject.SetActive(true);
            clearAllText();
            isRunning = true;
            StartCoroutine(activeBox());
        }
    }
    IEnumerator activeBox(){
        float timer = 0f, fadeTimer = 0.7f;
        Image box = dialogueBox.GetComponent<Image>();
        while(timer < fadeTimer){
            timer += Time.deltaTime;
            if(timer >= fadeTimer)
                timer = fadeTimer;
            Color col = box.color;
            col.a = timer /fadeTimer *0.7f;
            box.color = col;
            yield return null;
        }
        canControl = true;
        nextText();
    }
    void clearAllText(){
        textBox.text = "";
        nameBox.text = "";
    }
    public bool IsRunning(){
        return isRunning;
    }
    void Update(){
        if(canControl){
            if(Input.GetKey(KeyCode.Z) && !isDisplaying){
                nextText();
            }
        }
        
    }
    void nextText(){
        if(dialogueIndex < dialogueTexts.Count){
            while(enterQueue.Count>0 && enterQueue.Peek().Item1 == dialogueIndex){
                charaDict[enterQueue.Dequeue().Item2].Enter();
            }
            while(leaveQueue.Count>0 && leaveQueue.Peek().Item1 == dialogueIndex){
                charaDict[leaveQueue.Dequeue().Item2].Leave();
            }
            if(speakQueue.Count>0 && speakQueue.Peek().Item1 == dialogueIndex){
                foreach(var chara in characters){
                    chara.Speak(false);
                }
                string nameStr = speakQueue.Peek().Item2;
                charaDict[speakQueue.Dequeue().Item2].Speak(true);

                while(speakQueue.Count>0 && speakQueue.Peek().Item1 == dialogueIndex){
                    nameStr += "&" +speakQueue.Peek().Item2;
                    charaDict[speakQueue.Dequeue().Item2].Speak(true);
                }

                nameBox.text = nameStr;
            }
            while(nameQueue.Count>0 && nameQueue.Peek().Item1 == dialogueIndex){
                nameBox.text = nameQueue.Dequeue().Item2;
            }
            
            StartCoroutine(displayText(dialogueTexts[dialogueIndex]));
            dialogueIndex++;
        }
        else{
            endDialogue();
        }
    }
    IEnumerator displayText(string text){
        float displayTimer = 0f;
        textBox.text = text;
        isDisplaying = true;
        while(displayTimer < textSpeed){
            displayTimer += Time.deltaTime;
            if(displayTimer >= textSpeed)
                displayTimer = textSpeed;
            float rate = Mathf.Sqrt(displayTimer/textSpeed);
            textBox.transform.localScale = new Vector3(1f*rate, 1f*rate, 1);
            yield return null;
        }
        yield return new WaitForSeconds(inputDelayTime - textSpeed);
        isDisplaying = false;
    }
    
    void endDialogue(){
        clearAllText();
        canControl = false;
        foreach(var chara in characters){
            chara.Speak(false);
            if(chara.IsEntered())
                chara.Leave();
        }
        StartCoroutine(closeBox());
    }
    IEnumerator closeBox(){
        float timer = 0f, fadeTime = 1f;
        Image box = dialogueBox.GetComponent<Image>();
        while(timer < fadeTime){
            timer += Time.deltaTime;
            if(timer >= fadeTime)
                timer = fadeTime;
            Color col = box.color;
            col.a = (1- timer /fadeTime) *0.7f;
            box.color = col;
            yield return null;
        }

        isRunning = false;
    }
}
