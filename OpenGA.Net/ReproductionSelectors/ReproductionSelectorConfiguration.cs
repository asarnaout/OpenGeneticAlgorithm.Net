namespace OpenGA.Net.ReproductionSelectors;

public struct ReproductionSelectorConfiguration
{
    #region Elitism

    public bool AllowMatingElitesWithNonElites { get; set; }

    public double ProportionOfNonElitesAllowedToMate { get; set; }

    public double ProportionOfElitesInPopulation { get; set; }

    #endregion

    #region Tournament Selection
    
    public int TournamentSize { get; set; }

    public bool StochasticTournament { get; set; }
    
    #endregion

    public  ReproductionSelectorConfiguration()
    {
    }
}