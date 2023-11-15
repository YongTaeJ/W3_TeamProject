using SpartaDungeon.Scenes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SpartaDungeon
{
	enum State
	{
		None,
		End,
		Main,
		Town,
		Status,
		Inventory,
		Shop,
		Dungeon
	}

	internal class SceneManager
	{
		Dictionary<State, BaseScene> Scenes = new Dictionary<State, BaseScene>();
		State currentState, beforeState;

		public void ProcessScene(State state)
		{
			Scenes[state].ClearKey();
			Scenes[state].beforeState = beforeState;
			Scenes[state].EnterScene();
			beforeState = state;
			currentState = Scenes[state].ExitScene();
		}

		public void Init()
		{
			// Scenes.Add(State.End, new EndScene() ); 추후 구현 필요!!
			Scenes.Add(State.Main, new MainScene() );
			Scenes.Add(State.Town, new TownScene() );
			Scenes.Add(State.Status, new StatusScene() );
			Scenes.Add(State.Inventory, new InventoryScene());
			Scenes.Add(State.Shop, new ShopScene() );
			Scenes.Add(State.Dungeon, new DungeonScene());
		}

		public State Update()
		{
			return currentState;
		}
	}
}
