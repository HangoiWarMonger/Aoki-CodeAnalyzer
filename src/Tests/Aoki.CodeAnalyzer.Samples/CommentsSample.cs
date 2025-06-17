using System.Data;
using System.Data.Common;

namespace Aoki.CodeAnalyzer.Samples;

/// <summary>
/// 
/// </summary>
public class CommentsSample
{
    private readonly DataAdapter _technologyEvaluator;

    /// <summary>
    /// 
    /// </summary>
    public string A { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string B { get; init; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="technologyEvaluator"></param>
    public CommentsSample(DataAdapter technologyEvaluator)
    {
        _technologyEvaluator = technologyEvaluator;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
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