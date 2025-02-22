﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class FamePopup : MonoBehaviour
{
	public Text fameText, awardText;
	public Image fader;
	public CanvasGroup cg;

	public void Show()
	{
		fameText.text = DataStore.sessionData.gameVars.fame.ToString();
		//AWARD value based on FAME divided by 12, rounded down (for every 12 Fame you earn, you gain 1 Reward
		int awards = Mathf.FloorToInt( DataStore.sessionData.gameVars.fame / 12 );
		//reset to 0 at round 8+
		if ( DataStore.sessionData.gameVars.round >= 8 )
			awards = 0;
		awardText.text = awards.ToString();

		gameObject.SetActive( true );
		fader.color = new Color( 0, 0, 0, 0 );
		fader.DOFade( .95f, 1 );
		cg.DOFade( 1, .5f );

		transform.GetChild( 0 ).localScale = new Vector3( .85f, .85f, .85f );
		transform.GetChild( 0 ).DOScale( 1, .5f ).SetEase( Ease.OutExpo );
	}

	public void OnCancel()
	{
		FindObjectOfType<Sound>().PlaySound( FX.Click );
		fader.DOFade( 0, .5f ).OnComplete( () => gameObject.SetActive( false ) );
		cg.DOFade( 0, .2f );
		transform.GetChild( 0 ).DOScale( .85f, .5f ).SetEase( Ease.OutExpo );
	}

	private void Update()
	{
		if ( Input.GetKeyDown( KeyCode.Space ) )
			OnCancel();
	}
}
