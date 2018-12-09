namespace FunApp.Web.ML.Contracts
{
    public interface IJokesCategorizer
    {
        string Categorize(string modelFile, string jokeContent);
    }
}