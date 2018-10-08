using System;

namespace step2
{
	class Program 
	{ 
		public static void Main(string[] args) 
		{ 
			char[] suit = new char[4] { '♥', '♦', '♣', '♠' }; //массив масти
			int[] number = new int[] { 6, 7, 8, 9, 10, 11, 12, 13, 14 }; //масств номер карты
			int n = 0; // количество игроков
			int cardnum = 0;
			Karta[] deck = new Karta[36]; // колода (состоит из 36 карт) 
			Funct func = new Funct();
			
			do
			{
				Console.Clear();
				Console.WriteLine("1 < number of players < 7");
				Console.Write("Enter the number of players : ");
				n = Convert.ToInt32(Console.ReadLine());	 
			}
			while(n < 2 || n > 6);// проверка (мин количество игроков - 2, максаимальное -6)
			
			func.CreateDeck(cardnum, deck, suit, number);//создаем колоду
			
			func.RandomSort(deck);//перетасовываем карты колоды
				
			func.ShowDeck(cardnum, deck);//вывод колоды после перетасовки
			
			Karta trump = deck[deck.Length-1];//козырная карта (последняя в колоде)					
			Console.Write("Trump is : " );
			func.CheckCard(deck, (deck.Length-1));		
			Console.WriteLine();
			
				
			func.ShowPlayersCards(n, deck);//вывод карт игроков
			
			int[] summ = new int[n];//массив состоящий из сумм карт игроков
			
			func.FindSumm(summ, deck, n, trump);//поиск максимальной суммы
			
			
			Console.ReadKey(); 
		} 
	
		public class Funct
		{
			public void CreateDeck(int cardnum, Karta[] deck, char[] suit, int[] number)//создание колоды из 2х массивов (номер карты и масть)
			{
				for(int i = 0; i < 9; i++) 
				{ 
					for(int j = 0; j < 4; j++, cardnum++) 
					{ 
						deck[cardnum] = new Karta(); 
						deck[cardnum].suit = suit[j]; 
						deck[cardnum].number = number[i]; 
					} 
				} 
			}
			
			public void ShowPlayersCards(int n, Karta[] deck)
			{	
				int j = 0;
				
				for(int i = 0; i < n; i++)
				{
					Console.Write(i+1 + "player : ");
					
					for(int h = j; h < 6*(i+1); h++, j++)
					{					
						CheckCard(deck, h);//проверка (если номер карты 11 - то при выводе в консоль будет отображено как валет)
					}
					
					Console.WriteLine();
				}
			}
			
			public void ShowDeck(int cardnum, Karta[] deck)
			{
				cardnum = 0;
				
				for(int i = 0; i < 9; i++) 
				{ 
					for(int j = 0; j < 4; j++, cardnum++) 
					{ 
						CheckCard(deck, cardnum);
					} 
				
					Console.WriteLine();
				}
				
				Console.WriteLine();
			}
			
			public void RandomSort(Karta[] deck)
			{
				Random rnd = new Random(); 
			
				for(int i = 0; i < deck.Length; i++) 
				{ 
					int swap = rnd.Next(deck.Length); 
					Karta buffer = deck[i]; 
					deck[i] = deck[swap]; 
					deck[swap] = buffer; 
				} 
			}
			
			public void FindSumm(int[] summ, Karta[] deck, int n, Karta trump)
			{
				int j = 0;
				
				for(int i = 0; i < n; i++)//поочередно находим суммы карт каждого из игроков
				{				
					for(int h = j; h < 6*(i+1); h++, j++)
					{					
						summ[i] += deck[h].number;
						
						if(deck[h].suit == trump.suit)
							summ[i] += 9;
					}
				}
				
				int maxsumm = summ[0];//максимальная сумма карт из игроков
				
				for(int i = 0; i < summ.Length; i++)
				{
					if(summ[i] > maxsumm)
					{
						maxsumm = summ[i];
					}
				}
				
				for(int i = 0; i < summ.Length; i++)
				{
					Console.WriteLine(i+1 + " : " + summ[i]);
				}
				
				for(int i = 0; i < summ.Length; i++)
				{
					if(summ[i] == maxsumm)
					{
						Console.WriteLine("Player " + (i+1) + " has the strongest card combination.");
					}
				}
			}
			
			public void CheckCard(Karta[] deck, int cardnum)
			{
				if(deck[cardnum].number < 11) 
							Console.Write(Convert.ToString(deck[cardnum].number) + deck[cardnum].suit + "\t"); 

						else if(deck[cardnum].number == 11) 
							Console.Write("J" + deck[cardnum].suit + "\t"); 

						else if(deck[cardnum].number == 12) 
							Console.Write("Q" + deck[cardnum].suit + "\t"); 

						else if(deck[cardnum].number == 13) 
							Console.Write("K" + deck[cardnum].suit + "\t"); 

						else 
							Console.Write("A" + deck[cardnum].suit + "\t");
			}
		}
		
		public class Karta 
		{ 
			public char suit { get; set; } //масть карты
			public int number { get; set; } //номер карты
		} 
	} 
}