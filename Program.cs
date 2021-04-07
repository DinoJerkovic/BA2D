using System;
using System.Linq;
using System.Collections.Generic;

namespace BA2D
{
	class Program
	{
		public static string ProfileProbable(string text, int r, decimal[,] matrix)
		{
			List<decimal> lista = new List<decimal>();
			for (int i = 0; i < text.Length - r+1; i++)
			{
				string s = text.Substring(i, r);
				decimal suma = 1;
				for (int j = 0; j < s.Length; j++)
				{
					if (s.Substring(j, 1) == "A")
						suma *= matrix[0, j];
					if (s.Substring(j, 1) == "C")
						suma *= matrix[1, j];
					if (s.Substring(j, 1) == "G")
						suma *= matrix[2, j];
					if (s.Substring(j, 1) == "T")
						suma *= matrix[3, j];
				}
				lista.Add(suma);
			}
			decimal max = lista.Max();
			string p = "";
			for (int i = 0; i < lista.Count; i++)
			{
				if (lista[i] == max)
				{
					p = text.Substring(i, r);
					break;

				}
			}
			return p;

		}
		public static int Score(List<string> lista)
		{
			int suma = 0;
			for (int i = 0; i < lista[1].Length; i++)//stringovi u listi iste duljine pa bezveze uzeo prvi
			{
				int As= 0;
				int Cs= 0;
				int Gs = 0;
				int Ts = 0;
				foreach (string s in lista)
				{
					if (s.Substring(i, 1) == "A")
						As++;
					if (s.Substring(i, 1) == "C")
						Cs++;
					if (s.Substring(i, 1) == "G")
						Gs++;
					if (s.Substring(i, 1) == "T")
						Ts++;
				}
				List<int> l = new List<int>();
				l.Add(As);
				l.Add(Cs);
				l.Add(Gs);
				l.Add(Ts);
				int C = l.Max();
				suma += (lista[1].Length - C);
			}
			return suma;
		}
		public static decimal[,] Profile(List<string> lista)
		{
			decimal[,] matrix = new decimal[4, lista[0].Length];
			for (int i = 0; i < 4; i++)
			{for (int j = 0; j < lista[0].Length; j++)
					matrix[i, j] = (decimal)0;
			}
			foreach (string s in lista)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (s.Substring(i, 1) == "A")
						matrix[0, i]++;
					if (s.Substring(i, 1) == "C")
						matrix[1, i]++;
					if (s.Substring(i, 1) == "G")
						matrix[2, i]++;
					if (s.Substring(i, 1) == "T")
						matrix[3, i]++;

				}
			}
			decimal[,] matrix2 = new decimal[4, lista[0].Length];
			for (int i = 0; i < 4; i++)
			{
				for (int j = 0; j < lista[0].Length; j++)
					matrix2[i, j] = matrix[i,j]/lista.Count;
			}
			return matrix2;
		}
		public static List<string> Greedymotifsearch(List<string> dna, int k, int t)
		{ List<string> bestmotifs = new List<string>();
			foreach (string s in dna)
				bestmotifs.Add(s.Substring(0, k));
			for (int w = 0; w< dna[0].Length-k+1; w++)
			{
				string motif = dna[0].Substring(w,k);
				List<string> llista = new List<string>();
				llista.Add(motif);
				for (int j = 1; j < t; j++)
				{
					decimal[,]matr=Profile(llista);
					llista.Add(ProfileProbable(dna[j], k, matr));
				}
				if (Score(llista) < Score(bestmotifs))
					bestmotifs = llista;
				
			}
			return bestmotifs;
		}
		static void Main(string[] args)
		{
			List<string> z = new List<string>();
			int k = 3;
			int t = 5;
			z.Add("GGCGTTCAGGCA");
			z.Add("AAGAATCAGTCA");
			z.Add("CAAGGAGTTCGC");
			z.Add("CACGTCAATCAC");
			z.Add("CAATAATATTCG");
			//Console.WriteLine(z[0].Length);
			List<string> kraj = Greedymotifsearch(z, k, t);
			foreach (string s in kraj)
			{
				Console.WriteLine(s);
			}
			Console.WriteLine("kraj");



		}
	}
}
