using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12._11._19_MemoryGame_Itay
{
    class MemoryGame
    {
        public int Charset { get; set; } 

        private Queue<int> _numQueue1;

        public MemoryGame(int boardSize, int charset)
        {
            BoardSize = boardSize;
            this.Charset = charset;

            initialiseGame();
        }

        private int _playerScore = 0;
        private int _oponentScore = 0;
        private List<Label_field> _clicksList = new List<Label_field>();

        private Label_field[,] _fieldsContainer;

        private VirtualPlayer _currentVirtPlayer;

        private int START_LOCATION_X = 10;
        private int START_LOCATION_Y = 10;

        public delegate void exportFields(Label_field field);
        public event exportFields exportFieldsNow;
        public delegate void identityrevealing(string idn);
        public event identityrevealing identityrevealingNow;

        public delegate void yourScore(string yourScore);
        public event yourScore yourScoreNow;
        public delegate void opponentScore(string opponentScore);
        public event opponentScore opponentScoreNow;

        public int Difficulty
        {
            set
            {
                _currentVirtPlayer.Difficulty = value;
            }
        }

        public int BoardSize { get; set; } = 16;
        private void initialiseGame()
        {
            int[] array = new int[BoardSize * BoardSize];

            _numQueue1 = fillUpFields();

            _fieldsContainer = new Label_field[BoardSize, BoardSize];

            int widthFactor = 0;
            int heightFactor = 0;
            for (int i = 0; i < _fieldsContainer.GetLength(0); i++)
            {
                widthFactor = 0;
                for (int j = 0; j < _fieldsContainer.GetLength(1); j++)
                {
                    int num = -1;
                    if (_numQueue1.Count > 0) num = _numQueue1.Dequeue();

                    
                    string labelIdentity = null;
                    if (Charset == 0) labelIdentity = num.ToString();
                    else
                    {
                        labelIdentity = array.numbersToChars_thisProject()[num].ToString();
                    }

                    Label_field field = new Label_field(labelIdentity);
                    field.Location = new System.Drawing.Point(START_LOCATION_X + widthFactor, START_LOCATION_Y + heightFactor);
                    widthFactor += field.Width + 1;

                    field.customClick += onClick;
                    field.customHover += OnHover;

                    _fieldsContainer[i, j] = field;

                }
                heightFactor += new Label_field("?").Height + 1;
            }

            _currentVirtPlayer = new VirtualPlayer(_fieldsContainer);
        }

        private Queue<int> fillUpFields()
        {
            Random rnd = new Random();
            List<int> values1 = new List<int>();
            List<int> values2 = new List<int>();
            Queue<int> queue = new Queue<int>();
            int randomNumber;

            do
            {
               randomNumber = rnd.Next(0, BoardSize * BoardSize / 2);
                if(!values1.Contains(randomNumber))
                {
                    values1.Add(randomNumber); values2.Add(randomNumber);
                }
            }
            while (values1.Count < BoardSize * BoardSize / 2);

            List<int> combimed;
            if (BoardSize <= 8)
            {
                combimed = values1.Concat(values2).ToList();
                combimed.Shuffle_thisProject();
            }
            else
            {
                values1.Shuffle_thisProject(); values2.Shuffle_thisProject();
                combimed = values1.Concat(values2).ToList();
            }

            foreach (var s in combimed) queue.Enqueue(s);

            return queue;
        }


        private void OnHover(object sender, EventArgs e)
        {
            string message = $"{(sender as Label_field).IdentityTrue}; {(sender as Label_field).Used}";
            identityrevealingNow?.Invoke(message);
            //(sender as Label_field).Used = true;
        }



        private void onClick(object sender, EventArgs e)
        {            
            hitIt("player", sender as Label_field);
            (sender as Label_field).customClick -= onClick;
            (sender as Label_field).customClick += onClickEmpty;
        }
        private void onClickEmpty(object sender, EventArgs e) {}


        private void hitIt(string opponent, Label_field clickedField)
        {
            _clicksList.Add(clickedField);
            _clicksList[0].IdentityRevealed = true;
             

            if(_clicksList.Count >= 2)
            {
                if (_clicksList[0].IdentityTrue.Equals(_clicksList[1].IdentityTrue))
                {
                    switch(opponent)
                    {
                        case "player":
                            _playerScore++;
                            yourScoreNow?.Invoke(_playerScore.ToString());
                            break;
                        case "opponent":
                            _oponentScore++;
                            opponentScoreNow?.Invoke(_oponentScore.ToString());
                            break;
                    }

                    _clicksList[1].IdentityRevealed = true;
                    _clicksList[0].Mark = true; _clicksList[1].Mark = true;

                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                    timer.Interval = 1000;
                    timer.Enabled = true;
                    timer.Tick += (object sender, EventArgs e) => 
                        {
                            _clicksList[0].Used = true;
                            _clicksList[1].Used = true;
                            _clicksList[0].customClick -= onClick;
                            _clicksList[0].customClick += onClickEmpty;
                            _clicksList[1].customClick -= onClick;
                            _clicksList[1].customClick += onClickEmpty;

                            timer.Stop();
                            timer.Enabled = false;
                            winCheck();
                            
                            timer.Dispose();
                            _clicksList.Clear();
                        };
                    timer.Start();
                }
                else
                {
                    _clicksList[1].IdentityRevealed = true;
                    _clicksList[0].Mark = true; _clicksList[1].Mark = true;
                    System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                    timer.Interval = 1000;
                    timer.Enabled = true;
                    timer.Tick += (object sender, EventArgs e) =>
                    {
                        _clicksList[0].IdentityRevealed = false;
                        _clicksList[1].IdentityRevealed = false;
                        timer.Dispose();
                        _clicksList.Clear();

                        if(opponent.Equals("player")) computerGo();
                    };
                    timer.Start();




                    
                }
                
            }
        
        }

        private void winCheck()
        {
            List<Label_field> used = new List<Label_field>();

            foreach(var s in _fieldsContainer)
            {
                if (s.Used == true) used.Add(s);
            }
            //identityrevealingNow?.Invoke(used.Count.ToString());
            if(used.Count == BoardSize * BoardSize)
            {



                string winnerMessage;
                if (_playerScore > _oponentScore) winnerMessage = "You win!";
                else if (_oponentScore > _playerScore) winnerMessage = "Computer win!";
                else winnerMessage = "Draw!";

                string message = String.Format($"Game over!\n{winnerMessage}");
                MessageBox.Show(message);
            }

        }

        private void computerGo()
        {
            foreach (var s in _fieldsContainer) 
            {
                if (!s.Used)
                {
                    s.customClick -= onClick; s.customClick += onClickEmpty;
                }
            }






            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.Tick += (object sender, EventArgs e) => 
                {

                    _currentVirtPlayer.possibilities();
                    _currentVirtPlayer.DetermineOneInAPair = 0;
                    Label_field hittingField = _currentVirtPlayer.determineFieldToHit();
                    if (hittingField.IdentityTrue.Equals("???")) return;
                    hitIt("opponent", hittingField);

                    System.Windows.Forms.Timer timer2 = new System.Windows.Forms.Timer();
                    timer2.Interval = 1000;
                    timer2.Enabled = true;
                    timer2.Tick += (object sender2, EventArgs e2) =>
                        {

                            _currentVirtPlayer.possibilities();
                            _currentVirtPlayer.DetermineOneInAPair = 1;
                            Label_field hittingField2 = _currentVirtPlayer.determineFieldToHit();
                            if (hittingField2.IdentityTrue.Equals("???")) return;
                            if (hittingField2.IdentityTrue.Equals(hittingField.IdentityTrue)) StaticStaff.isVirtPlayerGuessedRight = true;
                            hitIt("opponent", hittingField2);

                            timer.Enabled = false;
                            timer2.Enabled = false;

                            foreach (var s in _fieldsContainer) 
                            {
                                if (!s.Used)
                                {
                                    s.customClick -= onClickEmpty; s.customClick += onClick;
                                }
                            }

                            if (hittingField2.IdentityTrue.Equals(hittingField.IdentityTrue))
                            {
                                System.Windows.Forms.Timer timer3 = new System.Windows.Forms.Timer();
                                timer3.Interval = 1000;
                                timer3.Enabled = true;
                                timer3.Tick += (object sender3, EventArgs e3) =>
                                    {
                                        timer3.Enabled = false;
                                        computerSucsessedAndGoNext();

                                        timer3.Dispose();
                                    };
                                timer3.Start();
                            }
                            
                                

                        timer2.Dispose();
                        };
                    timer2.Start();
                    



                    timer.Dispose();
                };
            timer.Start();



            
        }


        private void computerSucsessedAndGoNext()
        {


            if(StaticStaff.isVirtPlayerGuessedRight) //MessageBox.Show(StaticStaff.isVirtPlayerGuessedRight.ToString());
            {
                computerGo();
            }
        }

        public void displayGame()
        {
            foreach(var s in _fieldsContainer) exportFieldsNow?.Invoke(s);
        }






    }


}
