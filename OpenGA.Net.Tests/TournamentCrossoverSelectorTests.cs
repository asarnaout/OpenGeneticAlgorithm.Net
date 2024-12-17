using OpenGA.Net.CrossoverSelectors;

namespace OpenGA.Net.Tests;

public class TournamentCrossoverSelectorTests
{
    [Fact]
    public void WillFailIfThereThereIsLessThanTwoIndividuals()
    {
        var selector = new TournamentCrossoverSelector<int>();

        var random = new Random();

        var population = GenerateRandomPopulation(1, random);

        var config = new CrossoverConfiguration
        {
            TournamentSize = 5
        };

        var result = selector.SelectParents(population, config, random, 100).ToList();

        Assert.Empty(result);
    }

    [Fact]
    public void WillProduceUniformCouplesIfOnlyTwoMembersExistInThePopulation()
    {
        var selector = new TournamentCrossoverSelector<int>();

        var random = new Random();

        var population = GenerateRandomPopulation(2, random);

        var config = new CrossoverConfiguration
        {
            TournamentSize = 5
        };

        var minimumNumberOfCouples = 100;

        var result = selector.SelectParents(population, config, random, minimumNumberOfCouples).ToList();

        Assert.Equal(minimumNumberOfCouples, result.Count);

        foreach(var item in result)
        {
            Assert.Equal(population[0], item.IndividualA);
            Assert.Equal(population[1], item.IndividualB);
        }
    }

    [Fact]
    public void WillRunIfTheTournamentSizeIsLargerThanThePopulationSize()
    {
        var selector = new TournamentCrossoverSelector<int>();

        var random = new Random();

        var population = GenerateRandomPopulation(3, random);

        var config = new CrossoverConfiguration
        {
            TournamentSize = 5,
        };

        var minimumNumberOfCouples = 100;

        var result = selector.SelectParents(population, config, random, minimumNumberOfCouples).ToList();

        Assert.Equal(minimumNumberOfCouples, result.Count);
    }

    [Fact]
    public void WillRunOnNonStochasticTournaments()
    {
        var selector = new TournamentCrossoverSelector<int>();

        var random = new Random();

        var population = GenerateRandomPopulation(3, random);

        var config = new CrossoverConfiguration
        {
            TournamentSize = 3
        };

        var minimumNumberOfCouples = 100;

        var result = selector.SelectParents(population, config, random, minimumNumberOfCouples).ToList();

        Assert.Equal(minimumNumberOfCouples, result.Count);

        var populationOrderedByFitness = population.OrderByDescending(x => x.CalculateFitness()).ToList();

        foreach(var item in result)
        {
            Assert.True(populationOrderedByFitness[0] == item.IndividualA || populationOrderedByFitness[1] == item.IndividualA);
            Assert.True(populationOrderedByFitness[0] == item.IndividualB || populationOrderedByFitness[1] == item.IndividualB);
        }
    }

    [Fact]
    public void WillRunWithStochasticTournaments()
    {
        var selector = new TournamentCrossoverSelector<int>();

        var random = new Random();

        var population = GenerateRandomPopulation(3, random);

        var config = new CrossoverConfiguration
        {
            TournamentSize = 5,
            StochasticTournament = true
        };

        var minimumNumberOfCouples = 100;

        var result = selector.SelectParents(population, config, random, minimumNumberOfCouples).ToList();

        Assert.Equal(minimumNumberOfCouples, result.Count);
    }

    private static DummyChromosome[] GenerateRandomPopulation(int size, Random random) =>
        Enumerable.Range(0, size)
            .Select(x => new DummyChromosome(Enumerable.Range(0, 10).Select(y => random.Next()).ToArray()))
            .ToArray();
}