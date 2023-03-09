/*ACH Find Invalid Characters
ACH files are used to transfer between banks. It is a fairly old specification so it cannot handle modern unicode characters. In fact, it can only handle alphameric (a-z A-Z0-9_-:.@$=/ ) characters.In this exercise you will create a program that will search for and point of invalid characters in an ACH file.

Requirements
-Create a console application or script that takes a single parameter that is the path to the file to search for invalid characters.
-If you find a character that is not alphameric, print the character and the position in the file on screen.
*/

//issue reading from ACH file; copied data onto a text file instead
using System.Reflection.PortableExecutable;
using System.Text.RegularExpressions;

string fileName = @"C:\Users\berge\OneDrive\Documents\MagicWrighter\ACH_Invalid_Characters\Invalid_Characters.txt";
string testFileName1 = @"C:\Users\berge\OneDrive\Documents\Test.txt";


FindInvalidCharacters1(fileName);

//This function finds invalid characters but splits strings by empty characters - not by ach file; therefore it doesn't properly track invalid characters by each ach file (though this is never explicity asked in the prompt) 
void FindInvalidCharacters1(string filepath)
{
    //opting for File.ReadAllLines instead of streamreader because it's easier to convert text into an array of lines

    if (File.Exists(filepath))
    {
        string[] lines = File.ReadAllLines(filepath);
        int lineNumber = 1;
        int overallCharacterPosition = 1;

        //I'm not experienced with Regex, so opting for "Character" class functions instead
        foreach (string line in lines)
        {
            foreach (char c in line)
            {
                if (!isAlphameric(c))
                {
                    Console.WriteLine($"character: {c} \t line: {lineNumber} position: {overallCharacterPosition} is not alphameric!");
                    overallCharacterPosition++;
                }

                else
                {
                    overallCharacterPosition++;
                }
            }

            lineNumber++;
        }
    }
}

//This function splits the file into 94 character length strings and finds invalid characters; however, there are problems with how the statitistics are displayed on console
void FindInvalidCharacters2(string filepath)
{
    if (File.Exists(filepath))
	{

		using (StreamReader sr = new StreamReader(filepath))
		{
            //string[] lines = File.ReadAllLines(filepath);
            string textDump = sr.ReadToEnd();
			int lineNumber = 1;
            int overallCharacterPosition = 1;

            //form a new line each ACH file (94 characters)
            List<string> lines = new List<string>();
            int textChunk = 94;
            for (int i = 0; i < textDump.Length; i+= textChunk)
            {
                if (textDump.Length - i >= textChunk)
                {
                    string achFile = textDump.Substring(i, textChunk);
                    lines.Add(achFile);
                }

                else
                {
                    string remainder = textDump.Substring(i);
                    lines.Add(remainder);
                }
            }
            
            //for testing purposes only
            /*Console.WriteLine("There are: " + lines.Count + "lines");

            foreach (var item in lines)
            {
                Console.WriteLine(item);
            }*/

			foreach (string line in lines)
			{
				foreach (char c in line)
				{
                    //commenting out regex code below
                    //if (!Regex.IsMatch(c.ToString(), "^[a-zA-z0-9]*$"))
                    if (!isAlphameric(c))
					{
						Console.WriteLine($"character: {c} \t line: {lineNumber} position: {overallCharacterPosition} is not alphameric!") ;
						overallCharacterPosition++;
					}

					else
					{
						overallCharacterPosition++;
					}
                }

				lineNumber++;
                //reset character position when starting a new line
                overallCharacterPosition = 1;
            }

			sr.Close();
		}
	}

	else
	{
		Console.WriteLine("File not found!");
	}
}

bool isAlphameric(char c)
{
    //based on the prompt, these are the only permissable characters
    if (char.IsLetterOrDigit(c) || c =='_' || c == '-' || c == ':' || c == '@' || c == '$' || c == '=' || c == '/' || c == ' ')
    {
        return true;
    }

    else 
    {
        return false;
    }
}