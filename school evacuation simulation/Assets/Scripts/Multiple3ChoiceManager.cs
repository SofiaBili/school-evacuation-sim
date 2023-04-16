using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using TMPro;

public class Multiple3ChoiceManager : MonoBehaviour
{
    public Multiple3ChoiceQuestios[] questions;
	private static List<Multiple3ChoiceQuestios> unansweredQuestions;
	private Multiple3ChoiceQuestios currentQuestion;
	[SerializeField] private TextMeshProUGUI factText;
	[SerializeField] private TextMeshProUGUI ans0Text;
	[SerializeField] private TextMeshProUGUI ans1Text;
	[SerializeField] private TextMeshProUGUI ans2Text;

    StartQuestionProcedureScript startQuestionProcedureScript;
    GameObject startQuestionProcedureScriptObject;
    ToggleQuestionCanvas toggleQuestionCanvasScript;
    [SerializeField] GameObject toggleQuestionCanvasObject;

	[SerializeField] Button but1;
	[SerializeField] Button but2;
	[SerializeField] Button but3;
	static int randomQuestionIndex;

	
	[SerializeField] Camera canvasCamera;
	
    [SerializeField] GameObject slimeObject;
	Animator slimeAnimator;
    private string currentState;

	void Awake(){
		toggleQuestionCanvasScript = toggleQuestionCanvasObject.GetComponent<ToggleQuestionCanvas>();
        slimeAnimator = slimeObject.GetComponent<Animator>();
	}
	void Start(){
		if(unansweredQuestions == null || unansweredQuestions.Count==0){
			unansweredQuestions = questions.ToList<Multiple3ChoiceQuestios>();
		}

		SetCurrentQuestion();
	}
	void SetCurrentQuestion(){
		int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
		currentQuestion = unansweredQuestions[randomQuestionIndex];
		factText.text = currentQuestion.question;
		ans0Text.text = currentQuestion.ans1;
		ans1Text.text = currentQuestion.ans2;
		ans2Text.text = currentQuestion.ans3;
		unansweredQuestions.RemoveAt(randomQuestionIndex);
	}
    private IEnumerator ChangeButtonColour(Button btn){
		yield return new WaitForSeconds (1.9f);
		btn.GetComponent<Image>().color = Color.white;
	}
	
    private IEnumerator ChangeQuestion(){
		yield return new WaitForSeconds (2.1f);
		canvasCamera.cullingMask &=  ~(1 << LayerMask.NameToLayer("QuestionCanvas"));
		//canvasCamera.cullingMask |=  (1 << LayerMask.NameToLayer("QuestionCanvas"));
		SetCurrentQuestion();
		yield return new WaitForSeconds (0.09f);
		//canvasCamera.cullingMask |=  (1 << LayerMask.NameToLayer("QuestionCanvas"));
		//canvasCamera.cullingMask &=  ~(1 << LayerMask.NameToLayer("QuestionCanvas"));
		Debug.Log(unansweredQuestions.Count);
	}
	public void UserSelectExit(){
		startQuestionProcedureScript = toggleQuestionCanvasScript.GetCurrHitbox().GetComponent<StartQuestionProcedureScript>();
		startQuestionProcedureScript.StopAnimationAndCloseCanvasFromExit();
		StartCoroutine(ChangeQuestion());
	}
	public void UserSelect0(){
		startQuestionProcedureScript = toggleQuestionCanvasScript.GetCurrHitbox().GetComponent<StartQuestionProcedureScript>();
		if(currentQuestion.corrAns == 0){
			Debug.Log("Cor");
			but1.GetComponent<Image>().color = Color.green;
			startQuestionProcedureScript.DeleteHexagon();
			unansweredQuestions.RemoveAt(randomQuestionIndex);
			ChangeAnimationState("congratulations");
		}else{
			but1.GetComponent<Image>().color = Color.red;
			startQuestionProcedureScript.StopAnimationAndCloseCanvas();
			Debug.Log("WRONG");
			ChangeAnimationState("disappoint");
		}
		StartCoroutine(ChangeButtonColour(but1));
		StartCoroutine(ChangeQuestion());
	}
	public void UserSelect1(){
		startQuestionProcedureScript = toggleQuestionCanvasScript.GetCurrHitbox().GetComponent<StartQuestionProcedureScript>();
		if(currentQuestion.corrAns == 1){
			Debug.Log("Cor");
			but2.GetComponent<Image>().color = Color.green;
			startQuestionProcedureScript.DeleteHexagon();
			unansweredQuestions.RemoveAt(randomQuestionIndex);
			ChangeAnimationState("congratulations");
		}else{
			but2.GetComponent<Image>().color = Color.red;
			startQuestionProcedureScript.StopAnimationAndCloseCanvas();
			Debug.Log("WRONG");
			ChangeAnimationState("disappoint");
		}
		StartCoroutine(ChangeButtonColour(but2));
		StartCoroutine(ChangeQuestion());
	}
	public void UserSelect2(){
		startQuestionProcedureScript = toggleQuestionCanvasScript.GetCurrHitbox().GetComponent<StartQuestionProcedureScript>();
		if(currentQuestion.corrAns == 2){
			Debug.Log("Cor");
			but3.GetComponent<Image>().color = Color.green;
			startQuestionProcedureScript.DeleteHexagon();
			unansweredQuestions.RemoveAt(randomQuestionIndex);
			ChangeAnimationState("congratulations");
		}else{
			but3.GetComponent<Image>().color = Color.red;
			startQuestionProcedureScript.StopAnimationAndCloseCanvas();
			Debug.Log("WRONG");
			ChangeAnimationState("disappoint");
		}
		StartCoroutine(ChangeButtonColour(but3));
		StartCoroutine(ChangeQuestion());
	}
	public void ChangeAnimationState(string newState){
        currentState=newState;
        if(!slimeAnimator.GetCurrentAnimatorStateInfo(0).IsName(currentState))
            slimeAnimator.Play(newState);
    }
}
