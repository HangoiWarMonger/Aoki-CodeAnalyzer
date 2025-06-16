using System.Data;
using System.Data.Common;

namespace Aoki.CodeAnalyzer.Samples;

public class CommentsSample
{
    private readonly DataAdapter _technologyEvaluator;

    public string A { get; set; }

    public string B { get; init; }

    public CommentsSample(DataAdapter technologyEvaluator)
    {
        _technologyEvaluator = technologyEvaluator;
    }

    public async Task Consume(List<string> context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var request = new MyClass
        {
        };

        _technologyEvaluator.Fill(new DataSet());
    }

    internal class MyClass : IMyInterface
    {
        public int Get()
        {
            throw new NotImplementedException();
        }
    }

    internal interface IMyInterface
    {
        /// <summary>
        /// Привет.
        /// </summary>
        /// <returns></returns>
        int Get();
    }
}