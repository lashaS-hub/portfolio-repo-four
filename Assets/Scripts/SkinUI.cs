using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinUI : MonoBehaviour {


	public Button button;
	public Image icon;
	public Text text;
	public GameObject selected;

	private int skinIndex;

	private static SkinUI selectedSkin;

	public delegate void ChangeSkin(int i);
	public static event ChangeSkin OnChangeSkin;

	/// <summary>
	/// Start is called on the frame when a script is enabled just before
	/// any of the Update methods is called the first time.
	/// </summary>
	void Start()
	{
		button.onClick.AddListener(SelectSkin);
	}
	
	public void UpdateData(SkinModel model, int i, bool s)
	{
		skinIndex = i;
		icon.sprite = model.icon;
		text.text = model.text;
		selected.SetActive(s);
		button.interactable = Unlock(model);
		if(s)SelectSkin();
		
	}

	private bool Unlock(SkinModel model)
	{
		if((model.levelUnlock > 0 && model.levelUnlock <= PlayerPrefs.GetInt("current_level")+1) || 
			(model.comboUnlock > 0 && model.comboUnlock <= PlayerPrefs.GetInt("max_combo")))
			return true;
		return false;

	}

	private void SelectSkin()
	{
		OnChangeSkin(skinIndex);
		if(selectedSkin != null)
			selectedSkin.selected.SetActive(false);
		selected.SetActive(true);
		selectedSkin = this;
	}
}
