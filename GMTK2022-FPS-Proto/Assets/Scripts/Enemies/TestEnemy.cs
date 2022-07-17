using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : Enemy
{
    protected override void UpdateDestination()
	{
		if (Vector3.Distance(transform.position, player.transform.position) > 1.8f)
		{
			agent.isStopped = false;
			agent.destination = player.transform.position;
		}
		else
		{
			agent.isStopped = true;
			player.ReceiveDamage(damages);
		}
	}
}
