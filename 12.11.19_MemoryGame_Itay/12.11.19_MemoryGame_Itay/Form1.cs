using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12._11._19_MemoryGame_Itay
{
    public partial class Form1 : Form
    {

        private MemoryGame _currentGame;

        private Panel _pnlGameBoardContainer;
        public Form1()
        {
            InitializeComponent();
            initialiseCombo1();
            initializeGameInThisForm();
        }
        private void initialiseCombo1()
        {
            cmbBoardSize.Items.Clear();
            for (int i = 2; i <= 16; i *= 2) cmbBoardSize.Items.Add(i);
            cmbBoardSize.SelectedItem = 4;
        }
        private void initialiseCombo2()
        {
            cmbDifficultyLevel.Items.Clear();
            for (int i = 1; i <= 10; i++) cmbDifficultyLevel.Items.Add(new DifficultyLevel(i));
            cmbDifficultyLevel.Items.Add(new DifficultyLevel(-1));
            cmbDifficultyLevel.SelectedIndex = StaticStaff.difficultyLevelIndex;
            cmbDifficultyLevel.DropDownStyle = ComboBoxStyle.DropDownList;

            cmbCharset.Items.Clear();
            cmbCharset.Items.Add("Numbers");
            cmbCharset.Items.Add("Symbols");
            cmbCharset.SelectedItem = StaticStaff.charsetSelectedItem;
            cmbCharset.DropDownStyle = ComboBoxStyle.DropDownList;
            _currentGame.Charset = cmbCharset.SelectedIndex;
        }

        private void initializeGameInThisForm()
        {
            int cmbcharsetIndex = 0;
            switch (cmbCharset.SelectedItem)
            {
                case "Numbers":
                    cmbcharsetIndex = 0;
                    break;
                case "Symbols":
                    cmbcharsetIndex = 1;
                    break;
                default:
                    cmbcharsetIndex = 0;
                    break;
            }

            _currentGame = new MemoryGame((int)cmbBoardSize.SelectedItem, cmbcharsetIndex);
            initialiseCombo2();

            _pnlGameBoardContainer = new Panel();
            _pnlGameBoardContainer.Location = new Point(0, 0);
            _pnlGameBoardContainer.Width = _currentGame.BoardSize * new Label_field("?").Width + 40;
            _pnlGameBoardContainer.Height = _currentGame.BoardSize * new Label_field("?").Height + 40;
            this.Height = _pnlGameBoardContainer.Height < 200 ? _pnlGameBoardContainer.Height + 200 : _pnlGameBoardContainer.Height + 100;
            this.Controls.Add(_pnlGameBoardContainer);

            lblIdentityRevealing.Location = new Point(_pnlGameBoardContainer.Location.X + 10, _pnlGameBoardContainer.Location.Y + _pnlGameBoardContainer.Height + 10);


            _currentGame.exportFieldsNow += new MemoryGame.exportFields((Label_field field) => { this._pnlGameBoardContainer.Controls.Add(field); });
            _currentGame.displayGame();

            _currentGame.yourScoreNow += (string score) => { lblYourScore.Text = $"Your score: {score}"; };
            _currentGame.opponentScoreNow += (string score) => { lblOpponentScore.Text = $"Opponent score: {score}"; };

            _currentGame.identityrevealingNow += (string idn) => { lblIdentityRevealing.Text = idn; };
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            //foreach (var s in this.Controls) { this.Controls.Remove(s as Label_field); }
            this.Controls.Remove(_pnlGameBoardContainer);
            lblYourScore.Text = "Your score: 0";
            lblOpponentScore.Text = "Opponent score: 0";
            initializeGameInThisForm();
            
            
        }

        private void cmbDifficultyLevel_SelectedIndexChanged(object sender, EventArgs e)
        {
           _currentGame.Difficulty = (cmbDifficultyLevel.SelectedItem as DifficultyLevel).ToGo;
            StaticStaff.difficultyLevelIndex = cmbDifficultyLevel.SelectedIndex;
        }

        private void cmbCharset_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentGame.Charset = cmbCharset.SelectedIndex;
            StaticStaff.charsetSelectedItem = (string)cmbCharset.SelectedItem;
        }
    }
}
