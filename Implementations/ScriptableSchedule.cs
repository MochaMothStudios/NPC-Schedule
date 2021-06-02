using Sirenix.OdinInspector;
using UnityEngine;

namespace FedoraDev.NPCSchedule.Implementations
{
	[CreateAssetMenu(fileName = "New Schedule", menuName = "NPC/Schedule")]
	[HideMonoScript]
	public class ScriptableSchedule : SerializedScriptableObject, ISchedule
	{
		[SerializeField] ISchedule _schedule;

		public ITask GetCurrentTask(float value) => _schedule.GetCurrentTask(value);
		public ISchedule GetRuntime() => _schedule.GetRuntime();
	}
}
