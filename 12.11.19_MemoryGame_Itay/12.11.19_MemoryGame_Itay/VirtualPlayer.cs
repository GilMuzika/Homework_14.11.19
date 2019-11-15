using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _12._11._19_MemoryGame_Itay
{
    class VirtualPlayer
    {

        private List<Label_field> _possibilities = new List<Label_field>();
        private Random _rnd = new Random();

        public int DetermineOneInAPair { get; set; } = -1;

        private int _moveCount = 0;

        private int _difficulty = -1;
        public int Difficulty
        {
            get
            {
                return _difficulty;
            }
            set
            {
                _difficulty = value;
                difficultyLevelNow?.Invoke(value);
            }
        }

        public delegate void difficultyLevel(int difficulty);
        public event difficultyLevel difficultyLevelNow;


        private Label_field[,] _fieldsContainer;
        public VirtualPlayer(Label_field[,] fieldsContainer)
        {
            _fieldsContainer = fieldsContainer;
        }


        public void possibilities()
        {
            _possibilities.Clear();
            foreach(var s in _fieldsContainer)
            {
                if (s.Used == false) 
                { 
                    _possibilities.Add(s);
                }
            }
        }

        private Label_field findMostPossibleToHit()
        {
            var duplicates = StaticStaff.allWhichWasOpened.Where(x => x.Used == false).ToList().Select(x => x.IdentityTrue).ToList().findDuplicates_thisProject().ToList();

            //allWhichWasOpenedStrings is for debugging only
            var allWhichWasOpenedStrings = StaticStaff.allWhichWasOpened.Select(x => x.IdentityTrue).ToList();

            
            
            if (duplicates.Count == 0) { duplicates = allWhichWasOpenedStrings.findDuplicates_thisProject().ToList(); }

            var theSameIdentity = duplicates;

            //////////////////////////////////////////////////////"indexOfTheSameIdentityFiledsList" must life 2 iterations of the class
            if (DetermineOneInAPair == 0) StaticStaff.indexOfTheSameIdentityFiledsList = _rnd.Next(0, theSameIdentity.Count - 1);

            //string luckyPossibility = theSameIdentity[StaticStaff.indexOfTheSameIdentityFiledsList];

            List<string> cands = _possibilities.Select(x => x.IdentityTrue).ToList();


            string luckyPossibility = null;
            try
            {
                luckyPossibility = cands[StaticStaff.indexOfTheSameIdentityFiledsList];
            }
            catch
            {
                if (StaticStaff.indexOfTheSameIdentityFiledsList > cands.Count - 1)
                {
                    List<string> possibs = new List<string>();
                    foreach (var s in _fieldsContainer)
                    {
                        if (s.Used == false) possibs.Add(s.IdentityTrue);
                    }

                    if(possibs.Count > 1) luckyPossibility = possibs.findDuplicates_thisProject().ToList()[_rnd.Next(0, possibs.findDuplicates_thisProject().ToList().Count - 1)];
                }
            }


            



            List<Label_field> candidatePair = _possibilities.Where(x => x.IdentityTrue.Equals(luckyPossibility)).ToList();

            //for debugging only
            List<string> candidatePairStrings = _possibilities.Where(x => x.IdentityTrue.Equals(luckyPossibility)).ToList().Select(x => x.IdentityTrue).ToList();
            //for debugging only
            List<string> possibilitiesStrings = _possibilities.Select(x => x.IdentityTrue).ToList();

            if (DetermineOneInAPair == 1 || DetermineOneInAPair == 0) { } else throw new Exception($"DetermineOneInAPair is {DetermineOneInAPair}, this's isn't 0 nor 1 either! DetermineOneInAPair must be 1 or 0");
            try
            {
                if (DetermineOneInAPair == 1 || DetermineOneInAPair == 0) return candidatePair[DetermineOneInAPair];
            }
            catch
            {
                
            }
            return new Label_field("???");



        }

        public Label_field determineFieldToHit()
        {
            _moveCount++;

            if (Difficulty != -1)
            {
                if (_moveCount % Difficulty == 0) return determineFieldToHitRandomly();
                else return determineFieldToHitWisely();
            }
            else return determineFieldToHitWisely();
        }

        private Label_field determineFieldToHitWisely()
        {
            Label_field mostPossible = findMostPossibleToHit();



            if (mostPossible != null) return mostPossible;
            else throw new Exception("findMostPossibleToHit() had return null!");
        }

        private Label_field determineFieldToHitRandomly()
        {
            _ = StaticStaff.keepRandomlyDeterminedIndex ? StaticStaff.keepRandomlyDeterminedIndex = false : StaticStaff.keepRandomlyDeterminedIndex = true;

            List <Label_field> possibs = new List<Label_field>();
            foreach (var s in _possibilities) possibs.Add(s);



            if (StaticStaff.keepRandomlyDeterminedIndex)
            {
               if(possibs.Count > 1) StaticStaff.keep = possibs[_rnd.Next(0, possibs.Count - 1)];
               else return new Label_field("???"); 
            }

            try
            {
                if (possibs.Where(x => !x.IdentityTrue.Equals(StaticStaff.keep.IdentityTrue)).Last().Equals(StaticStaff.keep.IdentityTrue)) return new Label_field("???"); //throw new Exception("");

                    if (!StaticStaff.keepRandomlyDeterminedIndex) return possibs.Where(x => x.IdentityTrue.Equals(StaticStaff.keep.IdentityTrue)).First();
                else return possibs.Where(x => !x.IdentityTrue.Equals(StaticStaff.keep.IdentityTrue)).Last();

                
            }
            catch
            {
                determineFieldToHitRandomly();
            }

            return new Label_field("???");




        }











    }
}
