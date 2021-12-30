using System.Collections;
using System.Collections.Generic;

/// <summary>
/// MiniLisp Interpreter
/// </summary>
public class Interpreter
{
    string code = "";

    public Interpreter(){  }
    public Interpreter(string lispCode)
    {
        LoadCode(lispCode);
    }

    /// <summary>
    /// Load code
    /// </summary>
    /// <param name="lispCode"></param>
    public void LoadCode(string lispCode)
    {
        this.code = lispCode;
    }

    /// <summary>
    /// Evaluate the code, output result
    /// </summary>
    /// <returns></returns>
    public string Evaluate()
    {
        // TODO evaluation
        return "";
    }
}