﻿using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using System;
using UnityEngine.EventSystems;

public class GroupChooserScreen : MonoBehaviour
{
	public CardZoomer cardZoomer;
	public Image previewImage, fader;
	public CanvasGroup cg;
	public GroupToggleContainer groupToggleContainer;
	public MissionToggleContainer missionToggleContainer;
	public HeroAllyToggleContainer heroAllyToggleContainer;
	public Text enemyGroupTitle;
	public GameObject previewButton;

	Sound sound;
	ChooserMode mode;

	private void Awake()
	{
		sound = FindObjectOfType<Sound>();
	}

	//0=starting, 1=reserved, 2=villains, 3=ignored, 4=heroes
	public void ActivateScreen( ChooserMode mode, int dataGroupIndex = 0 )
	{
		gameObject.SetActive( true );
		cg.alpha = 0;
		cg.DOFade( 1, .5f );
		fader.DOFade( .95f, .5f );
		this.mode = mode;

		//reset expansion buttons except Core
		Transform[] expButtons = ( from Transform x in transform.Find( "expansion selector container" ) select x ).ToArray();
		for ( int i = 1; i < expButtons.Length; i++ )
		{
			if ( !DataStore.ownedExpansions.Contains( (Expansion)Enum.Parse( typeof( Expansion ), expButtons[i].name ) ) )
				expButtons[i].gameObject.SetActive( false );
			else
				expButtons[i].gameObject.SetActive( true );
		}

		expButtons[7].gameObject.SetActive( true );//Other is always active

		//reset UI
		previewButton.gameObject.SetActive( false );
		if ( mode == ChooserMode.DeploymentGroups )
		{
			switch ( dataGroupIndex )
			{
				case 0:
					enemyGroupTitle.text = "initial";
					break;
				case 1:
					enemyGroupTitle.text = "reserved";
					break;
				case 2:
					enemyGroupTitle.text = "villains";
					break;
				case 3:
					enemyGroupTitle.text = "ignored";
					break;
			}
			groupToggleContainer.transform.parent.gameObject.SetActive( true );
			groupToggleContainer.ResetUI( dataGroupIndex );
		}
		else if ( mode == ChooserMode.Missions )
		{
			missionToggleContainer.transform.parent.gameObject.SetActive( true );
			missionToggleContainer.ResetUI();
		}
		else if ( mode == ChooserMode.Hero || mode == ChooserMode.Ally )
		{
			heroAllyToggleContainer.transform.parent.gameObject.SetActive( true );
			heroAllyToggleContainer.ResetUI( mode );
		}
	}

	public void OnViewCard()
	{
		sound.PlaySound( FX.Click );
		EventSystem.current.SetSelectedGameObject( null );
		cardZoomer.ZoomIn( previewImage.sprite );
	}

	public void OnChangeExpansion( string expansion )
	{
		sound.PlaySound( FX.Click );
		if ( mode == ChooserMode.DeploymentGroups )
			groupToggleContainer.OnChangeExpansion( expansion );
		else if ( mode == ChooserMode.Missions )
			missionToggleContainer.OnChangeExpansion( expansion );
		else if ( mode == ChooserMode.Hero || mode == ChooserMode.Ally )
			heroAllyToggleContainer.OnChangeExpansion( expansion );
	}

	public void OnBack()
	{
		sound.PlaySound( FX.Click );
		EventSystem.current.SetSelectedGameObject( null );
		fader.DOFade( 0, .5f );
		cg.DOFade( 0, .5f ).OnComplete( () =>
		{
			gameObject.SetActive( false );
			groupToggleContainer.transform.parent.gameObject.SetActive( false );
			missionToggleContainer.transform.parent.gameObject.SetActive( false );
			heroAllyToggleContainer.transform.parent.gameObject.SetActive( false );
		} );
		FindObjectOfType<NewGameScreen>().OnReturnTo();
	}

	private void Update()
	{
		if ( Input.GetKeyDown( KeyCode.Space ) && !cardZoomer.gameObject.activeInHierarchy )
			OnBack();
	}
}
