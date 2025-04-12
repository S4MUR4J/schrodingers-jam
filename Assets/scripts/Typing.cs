using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using Logger = Utils.Logger;

public class Typing : MonoBehaviour
{
    [SerializeField]
    private string _finishPattern = ":wqa";

    [SerializeField] 
    private string _typePattern;
    
    public string TypeSentence
    {
        get
        {
            var word = new string(_typedLetters.ToArray());
            return word;
        }
    }
    
    private List<char> _typedLetters;


    private void Awake()
    {
        _typedLetters = new List<char>();
    }

    private void Update()
    {
        HandlePlayerInput();
        MatchInputWithPattern();
    }

    private void MatchInputWithPattern()
    {
        var pattern = (_typePattern + _finishPattern).ToUpper();
        var typedSentence = TypeSentence;
        
        var partRegex = new Regex("^" + Regex.Escape(pattern.Substring(startIndex: 0, length: typedSentence.Length)) + "$");
        if (!partRegex.IsMatch(typedSentence)) 
            _typedLetters.Clear();
        Logger.Log("Currently typed sentence is: " + TypeSentence);
        
        var fullRegex = new Regex("^" + Regex.Escape(pattern) + "$");
        if (!fullRegex.IsMatch(typedSentence))
            return;

        _typedLetters.Clear();
        
        // TODO wysłanie eventu?!
        throw new NotImplementedException();
    }

    private void HandlePlayerInput()
    {
        var lastPressedChar = GetPressedChar();
        if (lastPressedChar == null)
            return;
       
        _typedLetters.Add(lastPressedChar.Value);
        Logger.Log("Last pressed key stoke is: " + lastPressedChar);
    }

    private static char? GetPressedChar()
    {
        var codeValues = Enum.GetValues(typeof(KeyCode));
        foreach (KeyCode keyCode in codeValues)
        {
            if (!Input.GetKeyDown(keyCode))
                continue;
            
            return keyCode.ToString()[0];
        }

        return null;
    }
}
