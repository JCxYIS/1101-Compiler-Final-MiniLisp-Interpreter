using JC.MiniLisp_Interpreter;

/// <summary>
/// For instance, '(' ')' '*' are all term
/// </summary>
public class Term : IGrammar
{
    private string term;

    public Term(string termStr)
    {
        term = termStr;
    }

    public object Evaluate()
    {
        throw new System.NotImplementedException();
    }
}