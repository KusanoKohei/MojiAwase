using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MojiPlateParameter : ScriptableObject
{	
	public List<Param> param = new List<Param> ();

	[System.SerializableAttribute]
	public class Param
	{
		public int ID;
		public int Rank;
		public string Sprite;
		public string Text;
	}
}