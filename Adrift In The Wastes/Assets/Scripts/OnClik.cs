using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class OnClik : MonoBehaviour
{
	public Button Butt;

	void Start()
	{
		Button btn = Butt.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	void TaskOnClick()
	{
        
        PlayerController.Level = 1;
        LOad.loadin = true;
        Butt.gameObject.SetActive(false);
	}
}
