using UnityEngine;
using System.Collections;
using IntrovertStudios.Messaging;

public class UiContext : MonoBehaviour
{
	public static IMessageHub<MessageId> Hub{get;private set;}

	//An enum to represent 
	public enum MessageId
	{
		Shot 
	}

	void Awake()
	{
		Hub = new MessageHub<MessageId>();
	}

	void Destroy()
	{
		Hub.DisconnectAll();
		Hub = null;
	}

}
