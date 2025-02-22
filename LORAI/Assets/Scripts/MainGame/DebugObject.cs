﻿using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugObject : MonoBehaviour
{
	public Image thumb;
	public TextMeshProUGUI cardName, cardCost;

	CardDescriptor cardDescriptor;

	public void Init( CardDescriptor cd )
	{
		cardDescriptor = cd;

		if ( DataStore.villainCards.cards.Contains( cd ) )
			thumb.sprite = Resources.Load<Sprite>( $"Cards/Villains/{cd.id.Replace( "DG", "M" )}" );
		else
			thumb.sprite = Resources.Load<Sprite>( $"Cards/Enemies/{cd.expansion}/{cd.id.Replace( "DG", "M" )}" );

		cardName.text = cd.name;
		cardCost.text = cd.cost.ToString();
	}

	public void OnRemove()
	{
		DataStore.deploymentHand.Remove( cardDescriptor );
		//add card into manual deployment list, then sort list
		DataStore.manualDeploymentList.Add( cardDescriptor );
		DataStore.SortManualDeployList();

		Destroy( gameObject );
	}
}
