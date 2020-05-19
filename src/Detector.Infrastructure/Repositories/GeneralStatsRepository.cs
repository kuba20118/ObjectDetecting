using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Detector.Core.Domain;
using Detector.Core.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Detector.Infrastructure.Repositories
{
    public class GeneralStatsRepository : IGeneralStatsRepository, IMongoRepository
    {
        private readonly IMongoDatabase _database;
        private IMongoCollection<GeneralStats> GeneralStats => _database.GetCollection<GeneralStats>("GeneralStats");

        public GeneralStatsRepository(IMongoDatabase database)
        {
            _database = database;
        }
        public async Task CreateAsync()
            => await GeneralStats.InsertOneAsync(new GeneralStats());

        public async Task<GeneralStats> GetAsync()
            => await GeneralStats.AsQueryable().FirstOrDefaultAsync(x=>x.Key == "GeneralStats");

        public async Task UpdateAsync(Feedback feedback, int numberOfObjects, List<Tuple<string, int>> foundObjects, long time)
        {
            var stats = await GetAsync();

            //from feedback
            stats.CorrectObjectsDetections += feedback.Correct;
            stats.NotFoundObjects += feedback.NotFound;
            stats.IncorrectObjectsDetections += feedback.Incorrect;
            stats.MultipleObjectsDetections += feedback.MultipleFound;
            stats.IncorrectBoxDetections += feedback.IncorrectBox;

            //from ml
            stats.ObjectsFoundByML += numberOfObjects;
            stats.Time += time;

            //rest
            stats.SmallMistakes += (feedback.MultipleFound + feedback.IncorrectBox);
            stats.CriticalMistakes += (feedback.NotFound + feedback.Incorrect);
            stats.AllMistakes += (feedback.MultipleFound + feedback.IncorrectBox + feedback.NotFound + feedback.Incorrect);
            stats.Detections++;
            stats.AverageTime = stats.Time/stats.Detections;

            await GeneralStats.ReplaceOneAsync(x=>x.Key == "GeneralStats", stats);
        }
    }
}