#if UNITY_EDITOR
using System.IO;
using UnityEditor;
#else
using System;
#endif
using UnityEngine;
using Sirenix.OdinInspector;

namespace FedoraDev.NPCSchedule.Implementations
{
    public class ScheduleFactoryBehaviour : MonoBehaviour
    {
		const string FACTORY_PATH = "Assets/Fedora Dev/";
		const string FACTORY_NAME = "ScheduleFactory.asset";
		static string FactoryAsset => $"{FACTORY_PATH}{FACTORY_NAME}";

		public static ScheduleFactoryBehaviour Instance
		{
            get
			{
				if (_instance == null)
					_instance = FindObjectOfType<ScheduleFactoryBehaviour>();
				return _instance;
			}
		}

        public static ScheduleFactory ScheduleFactory
		{
			get
			{
				if (Instance._scheduleFactory == null)
				{
#if UNITY_EDITOR
					Instance.GenerateNewFactory();
#else
					throw new NullReferenceException("No Schedule Factory found! This is really bad and breaks the game. =[");
#endif
				}

				return Instance._scheduleFactory;
			}

			private set
			{
				Instance._scheduleFactory = value;
			}
		}

        [SerializeField, InlineEditor] ScheduleFactory _scheduleFactory;

		static ScheduleFactoryBehaviour _instance;

		private void Reset()
		{
			Debug.Log(ScheduleFactory.name + " Added.");
		}

		[Button]
		void CreateNewFactory()
		{
			GenerateNewFactory();
		}

#if UNITY_EDITOR
		void GenerateNewFactory()
		{
			Instance._scheduleFactory = SerializedScriptableObject.CreateInstance<ScheduleFactory>();
			if (!Directory.Exists(FACTORY_PATH))
				_ = Directory.CreateDirectory(FACTORY_PATH);
			AssetDatabase.CreateAsset(Instance._scheduleFactory, FactoryAsset);
			AssetDatabase.SaveAssets();
		}
#endif
	}
}
