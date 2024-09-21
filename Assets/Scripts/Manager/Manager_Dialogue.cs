using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MyBox;

public class Manager_Dialogue : MonoBehaviour
{
    [SerializeField] private UI_ManagerAnimation _animationManager;
    [SerializeField] private TextMeshProUGUI _nameText;
    [SerializeField] private TextMeshProUGUI _dialogueText;
    [SerializeField] private float typeSpeed = 1;
    private SO_Dialogue _dialogue;

    private Queue<string> _paragraphs = new Queue<string>();
    private Coroutine _typeDialogueCoroutine;
    private string _currentParagraph;
    private bool _isTyping = false;
    private const float MAX_TYPE_TIME = 0.5f;

    private void Start(){
        ResetText();
    }

    private void ResetText(){
        _nameText.text = "";
        _dialogueText.text = "";
    }

    public void StartDialogue(SO_Dialogue dialogue){
        _dialogue = dialogue;
        StartCoroutine(StartDialogueCoroutine());
    }

    private IEnumerator StartDialogueCoroutine(){
        ResetText();
        
        //Wait Animation End
        yield return StartCoroutine(_animationManager.PlayAnimation("Start"));
        
        //Display name
        _nameText.text = _dialogue.Name;

        //Adiciona paragrafos na fila
        foreach(string paragraph in _dialogue.Paragraphs)
            _paragraphs.Enqueue(paragraph);

        //Escreve o primeiro paragrafo
        DisplayNextParagraph();
    }

    private void EndDialogue(){
        _paragraphs.Clear();
        Debug.Log("EndDialogue");
        //Ao terminar a animação Invoke OnEndDialogue Event
        StartCoroutine(_animationManager.PlayAnimation("End",() => Manager_Event.DialogueManager.OnEndDialogue.Get().Invoke()));
    }

    [ButtonMethod]
    public void DisplayNextParagraph(){

        //Sem mais paragrafos, Dialogue End
        if(_paragraphs.Count == 0 && !_isTyping){
            EndDialogue();
            return;
        }

        //Skip Typing
        if(_isTyping){
            SkipTyping();
        }
        
        //Novo paragrafo
        else{
            _currentParagraph = _paragraphs.Dequeue();
            _typeDialogueCoroutine = StartCoroutine(TypeDialogueText());
        }
    }

    private IEnumerator TypeDialogueText()
    {
        _isTyping = true;

        int maxVisibleChars = 0;

        _dialogueText.text = _currentParagraph;
        _dialogueText.maxVisibleCharacters = maxVisibleChars;        

        foreach (char c in _currentParagraph.ToCharArray())
        {
            maxVisibleChars++;
            _dialogueText.maxVisibleCharacters = maxVisibleChars;

            yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);
        }

        _isTyping = false;
    }

    private void SkipTyping(){
        StopCoroutine(_typeDialogueCoroutine);
        _dialogueText.maxVisibleCharacters = _currentParagraph.Length;
        _isTyping = false;
    }
}
