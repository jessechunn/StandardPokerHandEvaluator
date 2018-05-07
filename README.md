# StandardPokerHandEvaluator
Fast poker hand evaluation in C#

The Standard Poker Hand Evaluator generates an internal list of every possible 5 card poker hand rank (there are 7,462 of them), and associates each of those hands with an ordered list of prime numbers which can be looked up by multiplying the prime numbers associated with each of the 5 cards in the hand. Each possible hand also has an associated rank which is used to compare hands to determine winners and losers in a given round.

Usage is relatively simple. The programmer creats a "Deck", deals 5 cards to each of one or more "Hand"s, and then compares (or sorts) the hands to rank them from best to worst.

The hands and individual cards have custom "ToString" formats to accomodate short or verbose hand or card descriptions. For example, a hand might be displayed as "AhAsAcAdKc" or "Ace of Hearts, Ace of Spades, Ace of Clubs, Ace of Diamonds, King of Clubs" or "Four Aces", depending on how the ToString method is called.

Testing on a gaming laptop was easily able to do the following steps 100,000 times in 1 second:
Create and shuffle a deck of cards.
Deal 10 hands, 5 cards each.
Sort the hands from best to worst.

Basic examples hopefully coming soon, including:
  5 card stud
  Texas Hold'em
  Omaha
  
Basic usage example:

//only create a PokerHandRankingTable once at application startup
//and pass a reference to the hands
PokerHandRankingTable rankingTable = new PokerHandRankingTable();

//create decks as often as needed.
//Passing in "true" means shuffle, otherwise
//cards will be in order like a new deck of cards.
Deck deck = new Deck(true);

//you can create a list of hands if you want

List<Hand> hands = new List<Hand>();

hands.Add(new Hand(deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), rankingTable));

hands.Add(new Hand(deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), rankingTable));

hands.Add(new Hand(deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), rankingTable));

hands.Add(new Hand(deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), rankingTable));

hands.Add(new Hand(deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), rankingTable));

hands.Add(new Hand(deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), rankingTable));

hands.Add(new Hand(deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), rankingTable));

hands.Add(new Hand(deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), rankingTable));

hands.Add(new Hand(deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), rankingTable));

hands.Add(new Hand(deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), deck.Pop(), rankingTable));


//and then using LINQ is one eash way to sort the hands, like this...
hands = hands.OrderBy(h => h.Rank).ToList<Hand>();
MessageBox.Show(hands[0].ToString(Hand.HandToStringFormatEnum.HandDescription) + " is the winning hand.");

//hands also have ==, !=, >,<,>=,<= operators overloaded so this works:
if (hands[0] == hands[1])
{
    MessageBox.Show(hands[0].ToString(Hand.HandToStringFormatEnum.HandDescription) + 
          " ties with " + hands[0].ToString(Hand.HandToStringFormatEnum.HandDescription));
}
