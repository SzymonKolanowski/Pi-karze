﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace TheBestScorersinHistory

{
	class ScorersDatabase
	{
		AllLists allLists = new AllLists();

		private const string databasePath = "D://Szymon//Git//TheBestScorersinHistory//TheBestFootballScorersInHistory//TheBestFootballScorersInHistory//TheBestScorersInHostory//TheBestScorersInHostory//Scorers.json";

		public ScorersDatabase()
		{
			string json = string.Empty;
			try
			{
				json = File.ReadAllText(databasePath);
			}
			catch { }
			allLists = JsonConvert.DeserializeObject<AllLists>(json) ?? new AllLists();
		}

		public IEnumerable<Scorer> ScorersList()
		{
			return allLists.Scorers;
		}

		public void AddScorer(Scorer scorer)
		{
			scorer.ID = allLists.Scorers.Select(s => s.ID).DefaultIfEmpty().Max() + 1;
			allLists.Scorers.Add(scorer);
		}

		public void Save()
		{
			var json = JsonConvert.SerializeObject(allLists, Formatting.Indented);
			if (File.Exists(databasePath))
			{
				File.Delete(databasePath);
			}
			File.WriteAllText(databasePath, json);
		}

		public void RemoveScorer(int id)
		{
			allLists.Scorers.RemoveAll(s => id == s.ID);
		}

		public Scorer GetScorerById(int id)
		{
			return allLists.Scorers.FirstOrDefault(s => id == s.ID);
		}

		public IEnumerable<Scorer> SortScorersDescByTotalGoals()
		{
			return allLists.Scorers.OrderByDescending(s => s.TotalGoals);
		}

		public IEnumerable<Scorer> SortScorersDescByChlGoals()
		{
			return allLists.Scorers.OrderByDescending(s => s.Goals[CompetitionType.ChampionsLeague]);
		}

		public IEnumerable<Scorer> SortScorersDescByLeaquesGoals()
		{
			return allLists.Scorers.OrderByDescending(s => s.Goals[CompetitionType.League]);
		}

		public IEnumerable<Scorer> SortScorersDescByNationalsGoals()
		{
			return allLists.Scorers.OrderByDescending(s => s.Goals[CompetitionType.National]);
		}



	}
}
