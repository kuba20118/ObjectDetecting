using System;
using System.Threading.Tasks;
using Detector.Infrastructure.Dtos;
using System.Collections.Generic;
using AutoMapper;
using Detector.Core.Repositories;
using Detector.Core.Domain;
using Detector.Infrastructure.Charts;
using System.Linq;

namespace Detector.Infrastructure.Services
{
    public class StatsService : IStatsService
    {
        private readonly IMapper _mapper;
        private readonly IStatsRepository _statsRepository;
        private readonly IImageRepository _imageRepository;
        private readonly IGeneralStatsRepository _generalStatsRepository;

        public StatsService(IMapper mapper, IStatsRepository statsRepo, IImageRepository imageRepository, IGeneralStatsRepository generalStatsRepository)
        {
            _generalStatsRepository = generalStatsRepository;
            _statsRepository = statsRepo;
            _imageRepository = imageRepository;
            _mapper = mapper;
        }
        public async Task AddStatsToImage(Guid id, Feedback stats)
        {
            var image = await _imageRepository.GetAsync(id);

            if (image == null)
                throw new Exception("Nie znaleziono odpowiedniego zdjęcia");

            var statistics = new Statistics(id, image.Description, image.ElapsedTime, stats);
            await _statsRepository.AddAsync(statistics);
        }

        public async Task<IEnumerable<Statistics>> GetAll()
        {
            var stats = await _statsRepository.GetAllAsync();
            return stats;
        }

        public async Task<Statistics> GetImageStats(Guid id)
        {
            var stats = await _statsRepository.GetAsync(id);
            var temp = _mapper.Map<StatsDto>(stats);
            return stats;
        }

        public async Task<SummaryStats> GetSummaryStats()
        {
            // var allStats = await _statsRepository.GetAllAsync();
            // if (allStats == null || allStats.Count() == 0)
            //     return null;

            var mistakes = new List<Tuple<string, int>>();
            // var averageTime = allStats.Average(x => x.Time);
            // var foundByMLSum = allStats.Sum(x => x.NumberOfObjectsFound);
            // var onlyCorrectSum = allStats.Sum(x => x.FeedbackFromUser.Correct);
            // var allMistakesSum = allStats.Sum(x => x.AllMistakes);
            // var critMistakesSum = allStats.Sum(x => x.CritMistakes);
            // var smallMistakesSum = allMistakesSum - critMistakesSum;
            var generalStats = await _generalStatsRepository.GetAsync();

            var sum1 = generalStats.IncorrectObjectsDetections;
            mistakes.Add(new Tuple<string, int>("Niepoprawnie wykryty obiekt", sum1));

            var sum2 = generalStats.NotFoundObjects;
            mistakes.Add(new Tuple<string, int>("Nieznaleziony obiekt", sum2));

            var sum3 = generalStats.MultipleObjectsDetections;
            mistakes.Add(new Tuple<string, int>("Wielokrotnie znaleziony obiekt", sum3));

            var sum4 = generalStats.IncorrectBoxDetections;
            mistakes.Add(new Tuple<string, int>("Niepoprawne zaznaczenie", sum4));

            mistakes.Sort((x, y) => y.Item2.CompareTo(x.Item2));


            var correctAndAllMistakesChart = new ChartData
            {
                Title = "Bezbłędne wykrycia",
                Key = "correctAndAllMistakesChart",
                ChartType = "doughnut",
                Data = new Tuple<List<string>, List<int>>
                (
                    new List<string> { "Poprawne", "Niepoprawne" },
                    new List<int> { generalStats.CorrectObjectsDetections, generalStats.AllMistakes }
                )
            };

            var correctAndSmallMistakesChart = new ChartData
            {
                Title = "Wykrycia z małoistotnymi błędami",
                Key = "correctAndSmallMistakesChart",
                ChartType = "doughnut",
                Data = new Tuple<List<string>, List<int>>
                (
                    new List<string> { "Poprawne", "Niepoprawne" },
                    new List<int> { generalStats.CorrectObjectsDetections, generalStats.SmallMistakes }
                )
            };

            var topMistakes = new ChartData
            {
                Title = "Najczęsciej występujące błędy",
                Key = "topMistakes",
                ChartType = "Bar",
                Data = Unpack(mistakes)
            };

            var topFoundObjects = new ChartData
            {
                Title = "Najczęsciej wykrywane obiekty",
                Key = "topFoundObjects",
                ChartType = "Bar",
                Data = Unpack(mistakes)
            };

            var chartsList = new List<ChartData> { correctAndAllMistakesChart, correctAndSmallMistakesChart, topMistakes, topFoundObjects };

            var summaryStats = new SummaryStats
            {
                AverageTime = generalStats.AverageTime,
                ChartsData = chartsList,
                Effectiveness = (double)(generalStats.CorrectObjectsDetections / generalStats.ObjectsFoundByML)
            };

            return summaryStats;
        }

        public async Task UpdateGeneralStats(Guid id, Feedback stats)
        {
            var generalStats = await _generalStatsRepository.GetAsync();
            if (generalStats == null || generalStats.Time == 0)
                await _generalStatsRepository.CreateAsync();

            var imageStats = await _statsRepository.GetAsync(id);

            await _generalStatsRepository.UpdateAsync(stats, imageStats.NumberOfObjectsFound, imageStats.FoundObjects, imageStats.Time);
        }

        Tuple<List<A>, List<B>> Unpack<A, B>(List<Tuple<A, B>> list)
        {
            return list.Aggregate(Tuple.Create(new List<A>(list.Count), new List<B>(list.Count)),
            (unpacked, tuple) =>
            {
                unpacked.Item1.Add(tuple.Item1);
                unpacked.Item2.Add(tuple.Item2);
                return unpacked;
            });
        }
    }
}