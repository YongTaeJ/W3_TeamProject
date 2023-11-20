using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace W3_TeamProject
{
	internal abstract class BaseScene
	{
		// beforeState는 뒤로가기만을 위한 변수입니다.
		public SceneState beforeState = SceneState.None;

		protected SceneState nextState = SceneState.None;

		// Scene의 주된 행동을 여기서 작성하시면 됩니다.
		public abstract void EnterScene();

		// nextState를 반환하시면 됩니다.
		// return nextState;
		public abstract SceneState ExitScene();

		// 상태를 초기화하는 메서드입니다.
		public void ClearKey()
		{
			nextState = SceneState.None;
		}
		public static void WordColor(string _text, ConsoleColor _color) //색 지정해주기 - 박정혁
        {
            Console.ForegroundColor = _color;
            Console.WriteLine(_text);
            Console.ResetColor();
        }
        public static void SceneBase(bool _isWidth = false) //새로운 장면을 만들때 기본 구성, 맵의 중간선을 만들고 싶으면 true를 넣어주세요
        {
            Console.Clear();
            MapFrame(_isWidth);
            Console.SetCursorPosition(0, 1);
        }
        //1.텍스트, 2. Y좌표를 올리거나 내릴 수 있습니다, 3. 색을 원하면  true 4. 컬러색 지정해주기
        //SetStringPostion("hello"); 이런식으로 작성하면 왼쪽 테두리 옆으로 출력
        public static void SetStringPosition(string _text = " ", int _topPos = 0, bool _isWantColor = false, ConsoleColor _color = ConsoleColor.Yellow) // 색을 원하면, true를 사용하고, 원하는 컬러를 선택함, 현재 포지션에서 한칸 아래
        {
            int _mapPosX = Console.GetCursorPosition().Left + 2;
            int _mapPosY = Console.GetCursorPosition().Top + _topPos;
            Console.SetCursorPosition(_mapPosX, _mapPosY);
            if (_isWantColor)
                WordColor(_text, _color);
            else
                Console.WriteLine(_text);
        }
        public static void SetWritePosition(string _text = " ")
        {
            int _mapPosX = Console.GetCursorPosition().Left + 2;
            int _mapPosY = Console.GetCursorPosition().Top;
            Console.SetCursorPosition(_mapPosX, _mapPosY);
            Console.Write(_text);
        }
        public static void MapFrame(bool _isWidth) // 맵 테두리에 중간선을 넣고 싶으면 true로 써주면 된다.
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0); // 테두리_가로선 맨 윗줄
            for (int i = 0; i < 60; i++)
            {
                Console.Write('ㅡ');
            }

            for (int i = 0; i < 29; i++) // 테두리_세로선 양쪽
            {
                Console.SetCursorPosition(0, i + 1);
                Console.Write('ㅣ');
                Console.SetCursorPosition(118, i + 1);
                Console.Write('ㅣ');
            }
            if (_isWidth)
            {
                Console.SetCursorPosition(0, 18); //테두리 중간선 
                for (int i = 0; i < 60; i++)
                {
                    Console.Write('ㅡ');
                }
            }
            Console.SetCursorPosition(0, 29); // 테두리_가로선 맨 밑줄
            for (int i = 0; i < 60; i++)
            {
                Console.Write('ㅡ');
            }
        }
    }
}
