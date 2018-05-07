using System;
using System.Collections.Generic;
using System.Text;

namespace StandardPokerHandEvaluator
{
    /// <summary>
    /// Enumeration values are used to calculate hand rank keys
    /// </summary>
    public enum CardEnum : int
    {
        Two = 2,
        Three = 3,
        Four = 5,
        Five = 7,
        Six = 11,
        Seven = 13,
        Eight = 17,
        Nine = 19,
        Ten = 23,
        Jack = 29,
        Queen = 31,
        King = 37,
        Ace = 41
    }
    public enum SuitEnum : int
    {
        Spades,
        Hearts,
        Clubs,
        Diamonds
    }
    public enum CardToStringFormatEnum
    {
        ShortCardName,
        LongCardName
    }

    public class Card
    {

        private CardEnum cardValue;
        private SuitEnum cardSuit;

        public Card(CardEnum CV, SuitEnum CS)
        {
            cardValue = CV;
            cardSuit = CS;
        }

        public CardEnum CardValue { get => cardValue; }
        public SuitEnum CardSuit { get => cardSuit; }

        public override string ToString()
        {
            return ToString(CardToStringFormatEnum.LongCardName);
        }

        public string ToString(CardToStringFormatEnum format)
        {
            switch (format)
            {
                case CardToStringFormatEnum.LongCardName:
                    {
                        return CardValue.ToString() + " of " + CardSuit.ToString();
                    }

                case CardToStringFormatEnum.ShortCardName:
                    {
                        switch (CardValue)
                        {
                            case CardEnum.Two:
                                {
                                    return "2" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.Three:
                                {
                                    return "3" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.Four:
                                {
                                    return "4" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.Five:
                                {
                                    return "5" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.Six:
                                {
                                    return "6" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.Seven:
                                {
                                    return "7" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.Eight:
                                {
                                    return "8" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.Nine:
                                {
                                    return "9" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.Ten:
                                {
                                    return "T" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.Jack:
                                {
                                    return "J" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.Queen:
                                {
                                    return "Q" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.King:
                                {
                                    return "K" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }

                            case CardEnum.Ace:
                                {
                                    return "A" + CardSuit.ToString().Substring(0, 1).ToLower();
                                }
                        }

                        break;
                    }
            }

            return "<Card value not set>";
        }
    }

    /// <summary>
    /// A five card poker hand.
    /// </summary>
    /// <remarks>This class represents a five card poker hand. 
    /// It implements the IComparable(Of Hand) interface, so 
    /// hands can be compared and sorted.</remarks>
    public class Hand : IComparable<Hand>
    {
        public enum HandToStringFormatEnum
        {
            ShortCardsHeld,
            LongCardsHeld,
            HandDescription
        }

        private Card mC1;
        private Card mC2;
        private Card mC3;
        private Card mC4;
        private Card mC5;
        private EvalHand mEvalHand;

        /// <summary>
        /// Initialize the hand.
        /// </summary>
        /// <param name="C1">The first of 5 cards.</param>
        /// <param name="C2">The second of 5 cards.</param>
        /// <param name="C3">The third of 5 cards.</param>
        /// <param name="C4">The fourth of 5 cards.</param>
        /// <param name="C5">The fifth of 5 cards.</param>
        /// <param name="EvalTable">An instance of the PokerHandList class.</param>
        /// <remarks>As this class initializes, it will calculate
        /// its "Key" value based on the cards held, and get its rank
        /// and description from the PokerHandList instance passed in
        /// by using the calculated Key value,
        /// which will allow the user of this class to compare
        /// this hand to other hands for the purpose of declaring
        /// winning, losing, or tied hands.</remarks>
        public Hand(Card C1, Card C2, Card C3, Card C4, Card C5, PokerHandRankingTable EvalTable)
        {
            int key;
            this.mC1 = C1;
            this.mC2 = C2;
            this.mC3 = C3;
            this.mC4 = C4;
            this.mC5 = C5;

            key = (int)mC1.CardValue * (int)mC2.CardValue * (int)mC3.CardValue * (int)mC4.CardValue * (int)mC5.CardValue;
            if (mC1.CardSuit == mC2.CardSuit && mC2.CardSuit == mC3.CardSuit && mC3.CardSuit == mC4.CardSuit && mC4.CardSuit == mC5.CardSuit)
            {
                //flush keys are negative to differentiate them
                //from non-flush hands of the same 5 card values
                key *= -1;
            }

            mEvalHand = EvalTable.EvalHands[key];
        }

        /// <summary>
        /// The Key value of this hand.
        /// </summary>
        /// <value></value>
        /// <returns>The Key value of this hand.</returns>
        /// <remarks>The Key is calculated by multiplying
        /// the "CardValue" of each card in this five card
        /// hand. Each of the 13 cards (2-A) has a unique
        /// prime number associated with it (same for each suit).
        /// Those numbers are multiplied together to get a unique
        /// value for the hand. If the hand is a flush (all
        /// five cards are of the same suit) the Key is
        /// multiplied by -1 to make it negative, to differentiate
        /// it from a non-flush hand of the same five cards.</remarks>
        public int Key
        {
            get
            {
                return mEvalHand.Key;
            }
        }

        /// <summary>
        /// The rank of this hand.
        /// </summary>
        /// <value></value>
        /// <returns>The Rank of this hand.</returns>
        /// <remarks>The Rank is used to compare this hand
        /// with other hands to determine which is the
        /// "better" hand. The lower the rank, the better
        /// the hand.</remarks>
        public int Rank
        {
            get
            {
                return mEvalHand.Rank;
            }
        }

        /// <summary>
        /// Compares this instance to another instance.
        /// </summary>
        /// <param name="other">An instance of the Hand class to be compared
        /// to this instance.</param>
        /// <returns>Less than zero if this instance is less than "Other", Zero if this instance is equal to "Other", More than zero if this instance is greater than "Other"</returns>
        /// <remarks></remarks>
        int IComparable<Hand>.CompareTo(Hand other)
        {
            return mEvalHand.Rank.CompareTo(other.Rank);
        }

        #region "Operator overrides"

        /// <summary>
        /// Equality Operator.
        /// </summary>
        /// <param name="ThisHand">The Hand object on the left hand side of the Operator.</param>
        /// <param name="OtherHand">The Hand object on the right hand side of the Operator.</param>
        /// <returns>True if ThisHand equals OtherHand
        /// False if ThisHand does not equal OtherHand</returns>
        /// <remarks>Internally, the Rank property of ThisHand is compared to the Rank property of OtherHand.</remarks>
        public static bool operator ==(Hand lhs, Hand rhs)
        {
            return lhs.Rank == rhs.Rank;
        }

        /// <summary>
        /// Inequality Operator
        /// </summary>
        /// <param name="ThisHand">The Hand object on the left hand side of the Operator.</param>
        /// <param name="OtherHand">The Hand object on the right hand side of the Operator.</param>
        /// <returns>False if ThisHand equals OtherHand
        /// True if ThisHand does not equal OtherHand</returns>
        /// <remarks>Internally, the Rank property of ThisHand is compared to the Rank property of OtherHand.</remarks>
        public static bool operator !=(Hand lhs, Hand rhs)
        {
            return lhs.Rank != rhs.Rank;
        }

        /// <summary>
        /// Greater Than Operator
        /// </summary>
        /// <param name="ThisHand">The Hand object on the left hand side of the Operator.</param>
        /// <param name="OtherHand">The Hand object on the right hand side of the Operator.</param>
        /// <returns>True if ThisHand is greater than OtherHand
        /// False if ThisHand is less than or equal to OtherHand</returns>
        /// <remarks>Internally, the Rank property of ThisHand is compared to the Rank property of OtherHand.
        /// The smaller Ranks are "greater than" larger Ranks (i.e. a rank of 300 is "greater than" a rank of 500)
        /// because smaller Ranks represent better hands.</remarks>
        public static bool operator >(Hand lhs, Hand rhs)
        {
            return lhs.Rank > rhs.Rank;
        }

        /// <summary>
        /// Less Than Operator
        /// </summary>
        /// <param name="ThisHand">The Hand object on the left hand side of the Operator.</param>
        /// <param name="OtherHand">The Hand object on the right hand side of the Operator.</param>
        /// <returns>True if ThisHand is less than OtherHand
        /// False if ThisHand is greater than or equal to OtherHand</returns>
        /// <remarks>Internally, the Rank property of ThisHand is compared to the Rank property of OtherHand.
        /// The larger Ranks are "less than" smaller Ranks (i.e. a rank of 500 is "less than" a rank of 300)
        /// because smaller Ranks represent better hands.</remarks>
        public static bool operator <(Hand lhs, Hand rhs)
        {
            return lhs.Rank < rhs.Rank;
        }

        /// <summary>
        /// Greater Than Or Equal To Operator
        /// </summary>
        /// <param name="ThisHand">The Hand object on the left hand side of the Operator.</param>
        /// <param name="OtherHand">The Hand object on the right hand side of the Operator.</param>
        /// <returns>True if ThisHand is greater than or equal to OtherHand
        /// False if ThisHand is less than OtherHand</returns>
        /// <remarks>Internally, the Rank property of ThisHand is compared to the Rank property of OtherHand.
        /// The smaller Ranks are "greater than" larger Ranks (i.e. a rank of 300 is "greater than" a rank of 500)
        /// because smaller Ranks represent better hands.</remarks>
        public static bool operator >=(Hand lhs, Hand rhs)
        {
            return lhs.Rank >= rhs.Rank;
        }

        /// <summary>
        /// Less Than Or Equal To Operator
        /// </summary>
        /// <param name="ThisHand">The Hand object on the left hand side of the Operator.</param>
        /// <param name="OtherHand">The Hand object on the right hand side of the Operator.</param>
        /// <returns>True if ThisHand is less than or equal to OtherHand
        /// False if ThisHand is greater than OtherHand</returns>
        /// <remarks>Internally, the Rank property of ThisHand is compared to the Rank property of OtherHand.
        /// The larger Ranks are "less than" smaller Ranks (i.e. a rank of 500 is "less than" a rank of 300)
        /// because smaller Ranks represent better hands.</remarks>
        public static bool operator <=(Hand lhs, Hand rhs)
        {
            return lhs.Rank <= rhs.Rank;
        }

        #endregion "Operator overrides"

        public override string ToString()
        {
            return ToString(HandToStringFormatEnum.HandDescription);
        }

        /// <summary>
        /// Returns a formatted string  for the hand.
        /// </summary>
        /// <param name="Format"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public string ToString(HandToStringFormatEnum Format)
        {
            switch (Format)
            {
                case Hand.HandToStringFormatEnum.ShortCardsHeld:
                    return mC1.ToString(CardToStringFormatEnum.ShortCardName) + mC2.ToString(CardToStringFormatEnum.ShortCardName) + mC3.ToString(CardToStringFormatEnum.ShortCardName) + mC4.ToString(CardToStringFormatEnum.ShortCardName) + mC5.ToString(CardToStringFormatEnum.ShortCardName);
                case Hand.HandToStringFormatEnum.LongCardsHeld:
                    return mC1.ToString() + ", " + mC2.ToString() + ", " + mC3.ToString() + ", " + mC4.ToString() + ", " + mC5.ToString();
                case Hand.HandToStringFormatEnum.HandDescription:
                    return mEvalHand.Name;
                default:
                    return mEvalHand.Name;
            }

        }

        public override bool Equals(object obj)
        {
            var hand = obj as Hand;
            return hand != null && Rank == hand.Rank;
        }

        public override int GetHashCode()
        {
            return 1852875615 + Rank.GetHashCode();
        }
    }


    /// <summary>
    /// A deck of playing cards.
    /// </summary>
    /// <remarks>This class represents a standard deck of 52 playing cards.</remarks>
    public class Deck : Stack<Card>
    {


        /// <summary>
        /// Initializes the Deck.
        /// </summary>
        /// <param name="Shuffled">Optional. If True, Deck will be shuffled after it is initialized.</param>
        /// <remarks>Creates a new Deck with 52 standard playing cards.</remarks>
        public Deck(bool shuffle = true)
        {
            if (shuffle)
            {
                Shuffle();
            }
            else
            {
                InitDeck();
            }
        }

        /// <summary>
        /// Adds standard 52 playing cards to the deck.
        /// </summary>
        /// <remarks>Cards will be in order, just like in a 
        /// new box of cards from the store. 
        /// Deck has 52 Cards (no Jokers).</remarks>
        private void InitDeck()
        {
            this.Clear();

            this.Push(new Card(CardEnum.Two, SuitEnum.Spades));
            this.Push(new Card(CardEnum.Three, SuitEnum.Spades));
            this.Push(new Card(CardEnum.Four, SuitEnum.Spades));
            this.Push(new Card(CardEnum.Five, SuitEnum.Spades));
            this.Push(new Card(CardEnum.Six, SuitEnum.Spades));
            this.Push(new Card(CardEnum.Seven, SuitEnum.Spades));
            this.Push(new Card(CardEnum.Eight, SuitEnum.Spades));
            this.Push(new Card(CardEnum.Nine, SuitEnum.Spades));
            this.Push(new Card(CardEnum.Ten, SuitEnum.Spades));
            this.Push(new Card(CardEnum.Jack, SuitEnum.Spades));
            this.Push(new Card(CardEnum.Queen, SuitEnum.Spades));
            this.Push(new Card(CardEnum.King, SuitEnum.Spades));
            this.Push(new Card(CardEnum.Ace, SuitEnum.Spades));

            this.Push(new Card(CardEnum.Two, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.Three, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.Four, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.Five, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.Six, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.Seven, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.Eight, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.Nine, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.Ten, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.Jack, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.Queen, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.King, SuitEnum.Hearts));
            this.Push(new Card(CardEnum.Ace, SuitEnum.Hearts));

            this.Push(new Card(CardEnum.Two, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.Three, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.Four, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.Five, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.Six, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.Seven, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.Eight, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.Nine, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.Ten, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.Jack, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.Queen, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.King, SuitEnum.Clubs));
            this.Push(new Card(CardEnum.Ace, SuitEnum.Clubs));

            this.Push(new Card(CardEnum.Two, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.Three, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.Four, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.Five, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.Six, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.Seven, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.Eight, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.Nine, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.Ten, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.Jack, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.Queen, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.King, SuitEnum.Diamonds));
            this.Push(new Card(CardEnum.Ace, SuitEnum.Diamonds));

        }
        /// <summary>
        /// Shuffles the cards in the Deck.
        /// </summary>
        /// <remarks>If the Deck is not full (Count=52) then the Deck will be reinitialized with 52 Cards and shuffled.</remarks>
        public void Shuffle()
        {
            //Collection<Card> col = new Collection<Card>();
            List<Card> lst = new List<Card>();
            Random r = new Random();
            Card c;
            int j;

            if (this.Count != 52)
            {
                //cards have been dealt (popped from stack), 
                //or the deck has not been created yet, 
                //so lets start fresh.
                //NEVER shuffle a partial deck.
                InitDeck();
            }

            for (int i = 0; i < 52; i++)
            {
                c = this.Pop();
                lst.Add(c);
            }

            for (int i = 0; i < 52; i++)
            {
                j = r.Next(0, 52 - i);
                c = lst[j];
                lst.RemoveAt(j);
                this.Push(c);
            }
        }
        /// <summary>
        /// Removes and returns the card at the top of the deck. 
        /// </summary>
        /// <returns>The top Card object from the top of the deck.</returns>
        /// <remarks>This function should be called to "Deal" the next card from the deck.
        /// This function will reduce the deck "Count" by 1.</remarks>
        public Card NextCard()
        {
            return this.Pop();
        }
    }
}
