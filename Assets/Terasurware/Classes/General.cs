using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class General : ScriptableObject
{	
	public List<Sheet> sheets = new List<Sheet> ();

	[System.SerializableAttribute]
	public class Sheet
	{
		public string name = string.Empty;
		public List<Param> list = new List<Param>();
	}

	[System.SerializableAttribute]
	public class Param
	{
		
		public double ID;
		public string Sprite;
		public string Rank_2_Text;
		public string Rank_0_Text_0;
		public string Rank_0_Text_1;
		public string Rank_1_Text_0;
		public string Rank_1_Text_1;
		public string Rank_1_Text_2;
		public string Rank_1_Text_3;
	}
}

