using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IBaseNPC
{
	void Move(Vector2 moveDestination);

	void Selected(bool t);
}

public class NPCGroup : IBaseNPC
{
	public List<IBaseNPC> memberList = new List<IBaseNPC>();

	public void Move(Vector2 moveDestination)
	{
		for(int i = 0; i < memberList.Count; i++)
		{
			memberList[i].Move(moveDestination + new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f)));
		}
	}

	public void Selected(bool t)
	{
		for(int i = 0; i < memberList.Count; i++)
		{
			memberList[i].Selected(t);
		}
	}

	public void AddMember(IBaseNPC newMember)
	{
		memberList.Add(newMember);
	}

	public void RemoveMember(IBaseNPC newMember)
	{
		memberList.Remove(newMember);
	}

	public IBaseNPC GetMember(int i)
	{
		return memberList[i];
	}

	public void ClearMembers()
	{
		memberList.Clear();
	}
}

[System.Serializable]
public class BoundKey
{
	public bool isBound = false;

	public Image boundKeyStatusImage;
	public Text unitAmountText;

	public NPCGroup boundGroup = new NPCGroup();

	public void Initialize()
	{
		UpdateKey();
		unitAmountText = boundKeyStatusImage.GetComponentInChildren<Text>();
	}

	public void BindGroup(NPCGroup newGroup)
	{
		isBound = true;
		boundGroup.ClearMembers();
		for(int i = 0; i < newGroup.memberList.Count; i++)
		{
			boundGroup.AddMember(newGroup.memberList[i]);
		}
//		boundGroup = newGroup; //Error, it passed by reference
		UpdateKey();
	}

	public void UpdateKey()
	{
		if(!isBound)
		{
			boundKeyStatusImage.color = Color.red;
		}
		else
		{
			boundKeyStatusImage.color = Color.green;
		}

		if(boundGroup.memberList.Count > 0)
		{
			unitAmountText.text = boundGroup.memberList.Count.ToString();
		}
	}
}

public class NPCSelectionManagerScript : MonoBehaviour
{
	public static NPCSelectionManagerScript Instance;
	public bool isBinding = false;
	NPCGroup selectedUnits = new NPCGroup();
	public BoundKey[] boundList = new BoundKey[5];

	void Awake ()
	{
		Instance = this;
	}

	void Start()
	{
		for(int i = 0; i < boundList.Length; i++)
		{
			boundList[i].Initialize();
		}
	}

	void Update()
	{
		if(Input.GetMouseButton(1))
		{
			Vector2 tempMoveTarget = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			selectedUnits.Move(tempMoveTarget);
		}

		if(Input.GetKeyDown(KeyCode.Space))
		{
			isBinding = true;
		}
		else if(Input.GetKeyUp(KeyCode.Space))
		{
			isBinding = false;
		}

		if(isBinding)
		{
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				boundList[0].BindGroup(selectedUnits);
			}
			else if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				boundList[1].BindGroup(selectedUnits);
			}
			else if(Input.GetKeyDown(KeyCode.Alpha3))
			{
				boundList[2].BindGroup(selectedUnits);
			}
			else if(Input.GetKeyDown(KeyCode.Alpha4))
			{
				boundList[3].BindGroup(selectedUnits);
			}
			else if(Input.GetKeyDown(KeyCode.Alpha5))
			{
				boundList[4].BindGroup(selectedUnits);
			}
		}
		else
		{
			if(Input.GetKeyDown(KeyCode.Alpha1))
			{
				UpdateSelectedGroup(boundList[0].boundGroup);
			}
			if(Input.GetKeyDown(KeyCode.Alpha2))
			{
				UpdateSelectedGroup(boundList[1].boundGroup);
			}
			if(Input.GetKeyDown(KeyCode.Alpha3))
			{
				UpdateSelectedGroup(boundList[2].boundGroup);
			}
			if(Input.GetKeyDown(KeyCode.Alpha4))
			{
				UpdateSelectedGroup(boundList[3].boundGroup);
			}
			if(Input.GetKeyDown(KeyCode.Alpha5))
			{
				UpdateSelectedGroup(boundList[4].boundGroup);
			}
		}
	}

	void UpdateSelectedGroup(NPCGroup tempGroup)
	{
		selectedUnits.Selected(false);
		selectedUnits.ClearMembers();

		for(int i = 0; i < tempGroup.memberList.Count; i++)
		{
			selectedUnits.AddMember(tempGroup.memberList[i]);
		}

		selectedUnits.Selected(true);
	}

	public void CreateGroup(List<IBaseNPC> memberList)
	{
		selectedUnits.ClearMembers();
		for(int i = 0; i < memberList.Count; i++)
		{
			selectedUnits.AddMember(memberList[i]);
		}
	}
}
