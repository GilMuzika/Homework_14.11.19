using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12._11._19_MemoryGame_Itay
{

    enum States
    {
        stateConcealed = 1,
        stateRevealed = 0,
        stateUsed = 3
    }
    class Label_field: Label
    {
        private const int WIDTH = 40;
        private const int HEIGHT = 40;

        private string _identityDefault = " ";
        private string _identity;
        public string IdentityTrue { get; private set; } = "?";
        private bool _identityRevealed = false;

        public delegate void thisEventHandler(object sender, EventArgs e);
        public event thisEventHandler customClick;
        public event thisEventHandler customHover;
        public bool IdentityRevealed
        {
            get => _identityRevealed;
            set
            {
                _identityRevealed = value;
                _ = value ?  _identity = IdentityTrue  :  _identity = _identityDefault;

              //  if (value) { _identity = _identityConcealed; this.State = States.stateConcealed; }
              //  else { _identity = _identityDefault; this.State = States.stateRevealed; }

                this.Image = imageOnLabel(_identity, Color.Black);

                StaticStaff.allWhichWasOpened.Add(this);
            }
        }
        private States _state;
        public States State
        {
            get => _state;
            set
            {
                _state = value;
                if (_state == States.stateUsed) Used = true;
                else Used = false;
                if (_state == States.stateRevealed) IdentityRevealed = true;
                if (_state == States.stateConcealed) IdentityRevealed = false;
            }

        }

        private bool _matched = false;
        public bool Mark
        {
            get => _matched;
            set
            {
                _matched = value;
                _ = _matched ? this.Image = imageOnLabel(_identity, Color.Red) : this.Image = imageOnLabel(_identity, Color.Black); 
            }
        }


        private bool _used = false;
        public bool Used
        {
            get => _used;
            set
            {
                Color clr = this.BackColor;
                _used = value;
                _ = value ? this.BackColor = Color.Black : this.BackColor = clr;
                _identity = _identityDefault;
                this.Image = imageOnLabel(_identity, Color.Black);
                this.customClick = null;
                this.customClick += (object sender, EventArgs e) => { };

                StaticStaff.allWhichWasOpened.Remove(this);
            }
        }

        private Graphics _graphicsObj;

        public Label_field(string identityConcealed)
        {
            IdentityTrue = identityConcealed;
            initiateMe();
        }

        protected override void OnClick(EventArgs e)
        {
            customClick(this, e);
            base.OnClick(e);
        }
        protected override void OnMouseHover(EventArgs e)
        {
            customHover(this, e);
            base.OnMouseHover(e);
        }


        private void initiateMe()
        {
            _identity = _identityDefault;
            this.Width = WIDTH;
            this.Height = HEIGHT;
            this.AutoSize = false;

            this.Image = imageOnLabel(_identity, Color.Black);
        }

        private Bitmap imageOnLabel(string idntty, Color color)
        {
            Bitmap forPicture = new Bitmap(WIDTH, HEIGHT);
            _graphicsObj = Graphics.FromImage(forPicture);

            Pen myPen = new Pen(color, 1);
            _graphicsObj.DrawRectangle(myPen, 0, 0, WIDTH - 1, HEIGHT - 1);

            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Purple);
            StringFormat drawFormat = new StringFormat();

            int shiftingFactor = 0;
            switch(idntty.ToString().Length)
            {
                case 1:
                    shiftingFactor = 10;
                    break;
                case 2:
                    shiftingFactor = 15;
                    break;
                case 3:
                    shiftingFactor = 23;
                    break;
                default:
                    shiftingFactor = 5;
                    break;
            }

            _graphicsObj.DrawString(idntty.ToString(), drawFont, drawBrush, this.Width / 2 - shiftingFactor, this.Height / 2 - 10, drawFormat);
            _graphicsObj.Dispose();

            return forPicture;
        }

        

    }
}
