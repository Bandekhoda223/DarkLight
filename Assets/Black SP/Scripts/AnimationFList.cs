using UnityEngine;
using System.Collections;

public class AnimationFList : MonoBehaviour {

	public Sprite[] SpriteList; // List of sprites that change with SpeedOfChange
	public float SpeedOfChange; // Speed Of Change "!=0"
	private int Current = 0;
	private bool GoBack = false;
	private SpriteRenderer thisComponent;


	void Start(){
		thisComponent = gameObject.GetComponent<SpriteRenderer> ();
		StartCoroutine ("WorkOfAnimation");
	}

	IEnumerator WorkOfAnimation(){
		if (!GoBack) {
			if (Current < SpriteList.Length - 1) {
				Current++;
			} else {
				GoBack = true;
			}
		} else {
			if (Current > 0) {
				Current--;
			} else {
				GoBack = false;
			}
		}
		thisComponent.sprite = SpriteList[Current];
		if (SpeedOfChange != 0) {
			yield return new WaitForSeconds (SpeedOfChange);
		} else {
			yield return new WaitForSeconds (0.01f);
		}
		StartCoroutine ("WorkOfAnimation");
	}




}
