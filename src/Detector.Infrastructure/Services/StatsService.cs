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

        public StatsService(IMapper mapper, IStatsRepository statsRepo, IImageRepository imageRepository)
        {
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

        public async Task<StatsDto> GetImageStats(Guid id)
        {
            var stats = await _statsRepository.GetAsync(id);
            var temp = _mapper.Map<StatsDto>(stats);
            return temp;
        }

        public async Task<SummaryStats> GetSummaryStats()
        {
            var allStats = await _statsRepository.GetAllAsync();
            if (allStats == null || allStats.Count() == 0)
                return null;

            var mistakes = new List<Tuple<string, int>>();
            var averageTime = allStats.Average(x => x.Time);
            var foundByMLSum = allStats.Sum(x => x.NumberOfObjectsFound);
            var onlyCorrectSum = allStats.Sum(x => x.FeedbackFromUser.Correct);
            var allMistakesSum = allStats.Sum(x => x.AllMistakes);
            var critMistakesSum = allStats.Sum(x => x.CritMistakes);
            var smallMistakesSum = allMistakesSum - critMistakesSum;

            var sum1 = allStats.Sum(x => x.FeedbackFromUser.Incorrect);
            mistakes.Add(new Tuple<string, int>("Niepoprawnie wykryty obiekt", sum1));

            var sum2 = allStats.Sum(x => x.FeedbackFromUser.NotFound);
            mistakes.Add(new Tuple<string, int>("Nieznaleziony obiekt", sum2));

            var sum3 = allStats.Sum(x => x.FeedbackFromUser.MultipleFound);
            mistakes.Add(new Tuple<string, int>("Wielokrotnie znaleziony obiekt", sum3));

            var sum4 = allStats.Sum(x => x.FeedbackFromUser.IncorrectBox);
            mistakes.Add(new Tuple<string, int>("Niepoprawne zaznaczenie", sum4));

            mistakes.OrderByDescending(x => x.Item2).ToList();

            var correctAndAllMistakesChart = new ChartData
            {
                Key = "Bezbłędne wykrycia",
                ChartType = "doughnut",
                Data = new Tuple<List<string>,List<int>>
                (
                    new List<string> { "Poprawne", "Niepoprawne" },
                    new List<int> { onlyCorrectSum, allMistakesSum }
                )
            };

            var correctAndSmallMistakesChart = new ChartData
            {
                Key = "Wykrycia z małoistotnymi błędami",
                ChartType = "doughnut",
                Data = new Tuple<List<string>,List<int>>
                (
                    new List<string> { "Poprawne", "Niepoprawne" },
                    new List<int> { onlyCorrectSum, smallMistakesSum }
                )             
            };

            var topMistakes = new ChartData
            {
                Key = "Najczęsciej występujące błędy",
                ChartType = "Bar",
                Data = Unpack(mistakes)
            };

            var topFoundObjects = new ChartData
            {
                Key = "Najczęsciej wykrywane obiekty",
                ChartType = "Bar",
                Data = Unpack(mistakes)

            };

            var chartsList = new List<ChartData> { correctAndAllMistakesChart, correctAndSmallMistakesChart };

            var summaryStats = new SummaryStats
            {
                AverageTime = averageTime,
                ChartsData = chartsList,
                Effectiveness = (foundByMLSum / onlyCorrectSum)
            };

            return summaryStats;
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