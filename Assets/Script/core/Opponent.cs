using System;

namespace Script.core
{
    public class Opponent
    {
        public static Opponent instance;
        public view.Opponent opponentView;
        private int _心动值 = 10;
        private int _信任值 = 10;
        private int _上头值 = 10;
        public int 心动值
        {
            get => _心动值;
            set
            {
                if (value > 99)
                {
                    throw new Exception("心动值太大");
                }
                else
                {
                    _心动值 = value;
                    opponentView.心动值.text = value.ToString();
                }
            }
        }
        public int 信任值
        {
            get => _信任值;
            set
            {
                if (value > 99)
                {
                    throw new Exception("信任值太大");
                }
                else
                {
                    _信任值 = value;
                    opponentView.信任值.text = value.ToString();
                }
            }
        }
        public int 上头值
        {
            get => _上头值;
            set
            {
                if (value > 99)
                {
                    throw new Exception("上头值太大");
                }
                else
                {
                    _上头值 = value;
                    opponentView.上头值.text = value.ToString();
                }
            }
        }
        public Opponent(view.Opponent opponentView,(int,int,int) 心动上头信任值)
        {
            this.opponentView = opponentView;
            instance = this;
            心动值 = 心动上头信任值.Item1;
            上头值 = 心动上头信任值.Item2;
            信任值 = 心动上头信任值.Item3;
        }
    }
}