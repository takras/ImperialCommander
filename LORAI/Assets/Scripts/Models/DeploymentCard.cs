﻿using System;
using System.Collections.Generic;

public class DeploymentCards
{
	public List<CardDescriptor> cards = new List<CardDescriptor>();
}

public class CardDescriptor : IEquatable<CardDescriptor>
{
	//== data from JSON
	public string name;
	public string id;
	public int tier;
	public string faction;
	public int priority;
	public int cost;
	public int rcost;
	public int size;
	public int fame;
	public int reimb;
	public string expansion;
	public string ignored;
	//==

	//upkeep properties
	public int currentSize;
	public bool isHealthy;
	public int colorIndex;
	public HeroHealth heroHealth;

	public bool Equals( CardDescriptor obj )
	{
		if ( obj == null )
			return false;
		CardDescriptor objAsPart = obj as CardDescriptor;
		if ( objAsPart == null )
			return false;
		else
			return id == objAsPart.id;
	}
}

/*
			"name": "Stormtrooper",
			"id": "DG001",
			"tier": 2,
			"faction": "Imperial",
			"priority": 2,
			"cost": 6,
			"rcost": 2,
			"size": 3,
			"fame": 6,
			"reimb": 3,
			"expansion": "Core",
			"ignored": "Squad Training"
*/
